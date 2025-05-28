// denne klasse skal ind i stien /BarberAkji.API/Data/Repositories/BookingRepository.cs (bookingRepository.cs skal laves den er ikke oprettet endnu)

using System.Data;
using System.Data.SqlClient;
using Dapper;
using BarberAkji.API.Data;
using BarberAkji.Models.Entities;

namespace BarberAkji.API.Repositories
{
    public class BookingRepository
    {
        private readonly DapperContext _context;

        public BookingRepository(DapperContext context)
        {
            _context = context;
        }

        // Denne metode forsøger at oprette en ny booking
        public async Task<(bool isSuccess, string message)> TryCreateBookingAsync(Booking booking)
        {
            using var connection = _context.CreateConnection();

            // Starter en transaktion for at kunne rulle tilbage hvis noget går galt
            using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                var sql = @"INSERT INTO Bookings (CustomerName, BookingDate, Note, EmployeeId, ServiceId)
                            VALUES (@CustomerName, @BookingDate, @Note, @EmployeeId, @ServiceId)";

                await connection.ExecuteAsync(sql, booking, transaction);

                transaction.Commit();
                return (true, "Booking oprettet");
            }
            catch (SqlException ex)
            {
                transaction.Rollback();

                // 2627/2601 betyder at unik constraint er blevet brudt (dobbeltbooking)
                if (ex.Number == 2627 || ex.Number == 2601)
                    return (false, "Der findes allerede en booking for denne medarbejder på det tidspunkt.");

                return (false, $"Ukendt fejl: {ex.Message}");
            }
        }

        // Henter alle bookinger fra databasen
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            using var connection = _context.CreateConnection();
            var sql = @"SELECT * FROM Bookings";
            return await connection.QueryAsync<Booking>(sql);
        }

        // Sletter en booking ud fra bookingens ID
        public async Task<bool> DeleteBookingAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Bookings WHERE Id = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
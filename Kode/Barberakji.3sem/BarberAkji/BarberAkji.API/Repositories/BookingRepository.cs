using System.Data;
using Microsoft.Data.SqlClient;
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
            using var transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);

            


            try
            {
                // Tjek for overlap: Findes der allerede en booking for samme medarbejder og tidspunkt?
                var overlapSql = @"SELECT COUNT(1) FROM Bookings
                           WHERE EmployeeId = @EmployeeId
                             AND BookingDate = @BookingDate";


                System.Diagnostics.Debugger.Break();


                var overlap = await connection.ExecuteScalarAsync<int>(
                    overlapSql,
                    new { booking.EmployeeId, booking.BookingDate },
                    transaction
                );

                if (overlap > 0)
                {
                    transaction.Rollback();
                    return (false, "Tiden overlapper med en eksisterende booking.");
                }

                // Hvis ingen overlap, opret booking
                var sql = @"INSERT INTO Bookings (CustomerName, BookingDate, Note, EmployeeId, ServiceId)
                    VALUES (@CustomerName, @BookingDate, @Note, @EmployeeId, @ServiceId)";
                await connection.ExecuteAsync(sql, booking, transaction);

                transaction.Commit();
                return (true, "Booking oprettet");
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
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

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

        public async Task<(bool isSuccess, string message)> TryCreateBookingAsync(Booking booking)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
            await connection.OpenAsync();


            using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                var duration = await connection.QuerySingleOrDefaultAsync<int>(
                    "SELECT DurationInMinutes FROM Services WHERE Id = @Id",
                    new { Id = booking.ServiceId }, transaction
                );

                if (duration == 0)
                    return (false, "Service ikke fundet");

                var sql = @"INSERT INTO Bookings (CustomerName, BookingDate, Note, EmployeeId, ServiceId)
                            VALUES (@CustomerName, @BookingDate, @Note, @EmployeeId, @ServiceId)";

                await connection.ExecuteAsync(sql, booking, transaction);
                transaction.Commit();

                return (true, "Booking oprettet");
            }
            catch (SqlException ex)
            {
                transaction.Rollback();

                if (ex.Number == 2627 || ex.Number == 2601)
                    return (false, "Der findes allerede en booking for denne medarbejder på dette tidspunkt.");

                return (false, $"SQL-fejl: {ex.Message}");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return (false, $"Fejl: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Bookings";
            var bookings = await connection.QueryAsync<Booking>(sql);
            return bookings;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Bookings WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using BarberAkji.API.Data;
using BarberAkji.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace BarberAkji.API.Repositories
{
    public class BookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Denne metode forsøger at oprette en ny booking
        public async Task<(bool isSuccess, string message)> TryCreateBookingAsync(Booking booking)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();  // Åben kun én gang

                using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

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

                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        return (false, "Tiden overlapper med en eksisterende booking.");
                    }

                    return (false, $"Ukendt fejl: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                throw;  // Brug 'throw;' i stedet for 'throw ex;' for at bevare stack trace
            }


        }


        // Henter alle bookinger fra databasen
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
            SELECT 
                b.*, 
                e.*, 
                s.*
            FROM Bookings b
            JOIN Employees e ON b.EmployeeId = e.Id
            JOIN Services s ON b.ServiceId = s.Id";

            var bookings = await connection.QueryAsync<Booking, Employee, Service, Booking>(
            sql,
            (booking, employee, service) =>
            {
                booking.Employee = employee;
                booking.Service = service;
                return booking;
            },
            splitOn: "Id,Id" // Dapper bruger dette til at forstå hvor Employee og Service starter
        );
            return bookings;
        }

        // Sletter en booking ud fra bookingens ID
        public async Task<bool> DeleteBookingAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var sql = "DELETE FROM Bookings WHERE Id = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

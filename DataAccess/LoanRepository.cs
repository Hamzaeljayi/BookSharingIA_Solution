using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Domain;

namespace DataAccess
{
    public class LoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Loan> GetAll()
        {
            var loans = new List<Loan>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Loans", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                loans.Add(new Loan
                {
                    Id = (int)reader["Id"],
                    BookId = (int)reader["BookId"],
                    DateBorrowed = (DateTime)reader["DateBorrowed"],
                    DateReturned = reader["DateReturned"] == DBNull.Value ? null : (DateTime?)reader["DateReturned"]
                });

            }

            return loans;
        }

        public void Add(Loan loan)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Loans (BookId, DateBorrowed) VALUES (@b,@d)", conn);
            cmd.Parameters.AddWithValue("@b", loan.BookId);
            cmd.Parameters.AddWithValue("@d", loan.DateBorrowed);

            cmd.ExecuteNonQuery();
        }
    }
}
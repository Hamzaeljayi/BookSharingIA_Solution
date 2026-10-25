using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Domain;

namespace DataAccess
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = new List<Book>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM Books", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Author = reader["Author"].ToString(),
                    YearPublished = reader["YearPublished"] == DBNull.Value ? 0 : (int)reader["YearPublished"],
                    Genre = reader["Genre"].ToString(),
                    NumberOfPages = reader["NumberOfPages"] == DBNull.Value ? 0 : (int)reader["NumberOfPages"]
                });

            }

            return books;
        }

        public void Add(Book book)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Books (Title, Author, YearPublished, Genre, NumberOfPages) VALUES (@t,@a,@y,@g,@p)", conn);

            cmd.Parameters.AddWithValue("@t", book.Title);
            cmd.Parameters.AddWithValue("@a", book.Author);
            cmd.Parameters.AddWithValue("@y", book.YearPublished);
            cmd.Parameters.AddWithValue("@g", book.Genre);
            cmd.Parameters.AddWithValue("@p", book.NumberOfPages);

            cmd.ExecuteNonQuery();
        }
    }
}
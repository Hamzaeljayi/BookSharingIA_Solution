using DataAccess;
using Domain;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class BookService
    {
        private readonly BookRepository _repo;

        public BookService(BookRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Book> GetAllBooks() => _repo.GetAll();

        public void AddBook(Book b)
        {
            if (string.IsNullOrWhiteSpace(b.Title) || string.IsNullOrWhiteSpace(b.Author))
                throw new Exception("Titre et auteur obligatoires.");
            _repo.Add(b);
        }
    }
}
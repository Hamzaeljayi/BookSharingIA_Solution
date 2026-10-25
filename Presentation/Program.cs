using BusinessLogic;
using DataAccess;
using Domain;

class Program
{
    static void Main()
    {
        var connectionString = DataSettings.GetConnectionString("LibraryDB");
        var bookService = new BookService(new BookRepository(connectionString));
        var loanService = new LoanService(new LoanRepository(connectionString));

        while (true)
        {
            Console.WriteLine("\n=== Bibliothèque Multi-Tiers ===");
            Console.WriteLine("1. Voir les livres");
            Console.WriteLine("2. Ajouter un livre");
            Console.WriteLine("3. Enregistrer un emprunt");
            Console.WriteLine("4. Voir les emprunts");
            Console.WriteLine("0. Quitter");
            Console.Write("Choix : ");
            var c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    foreach (var b in bookService.GetAllBooks())
                        Console.WriteLine($"{b.Id}. {b.Title} - {b.Author} ({b.YearPublished}) - {b.Genre}");
                    break;

                case "2":
                    Console.Write("Titre : ");
                    var title = Console.ReadLine();
                    Console.Write("Auteur : ");
                    var author = Console.ReadLine();
                    Console.Write("Année : ");
                    var year = int.Parse(Console.ReadLine());
                    Console.Write("Genre : ");
                    var genre = Console.ReadLine();
                    Console.Write("Pages : ");
                    var pages = int.Parse(Console.ReadLine());

                    bookService.AddBook(new Book
                    {
                        Title = title,
                        Author = author,
                        YearPublished = year,
                        Genre = genre,
                        NumberOfPages = pages
                    });

                    Console.WriteLine("Livre ajouté avec succès !");
                    break;

                case "3":
                    Console.Write("ID du livre : ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("ID invalide.");
                        break;
                    }

                    var book = bookService.GetAllBooks().FirstOrDefault(b => b.Id == id);
                    if (book == null)
                    {
                        Console.WriteLine("Aucun livre trouvé avec cet ID.");
                        break;
                    }

                    loanService.AddLoan(new Loan
                    {
                        BookId = id,
                        DateBorrowed = DateTime.Now
                    });

                    Console.WriteLine("Emprunt enregistré !");
                    break;


                case "4":
                    foreach (var l in loanService.GetAllLoans())
                        Console.WriteLine($"Emprunt #{l.Id} - Livre #{l.BookId} le {l.DateBorrowed.ToShortDateString()}");
                    break;

                case "0": return;
            }
        }
    }
}

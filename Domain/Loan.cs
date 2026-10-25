namespace Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        // Suppression de BorrowerName 
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
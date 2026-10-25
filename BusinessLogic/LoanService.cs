using DataAccess;
using Domain;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class LoanService
    {
        private readonly LoanRepository _repo;

        public LoanService(LoanRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Loan> GetAllLoans() => _repo.GetAll();

        public void AddLoan(Loan l)
        {
            _repo.Add(l);
        }

    }
}
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Repository
{
    public class BookDateRepository : RepositoryBase<BookDate>, IBookDateRepository
    {
        public BookDateRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateBookDate(BookDate bookdate)
        {
            Create(bookdate);
        }

        public int FindNpofBooksForDate(int Bookid, DateTime date)
        {
            return (FindByCondition(b => b.BookId.Equals(Bookid) && b.date.Equals(date)).Count());
        }

        public async Task<IEnumerable<BookDate>> GetAllBookDatesAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<BookDate>> GetBookDatesAsync(int BookId)
        {
            return await FindByCondition(b => b.BookId.Equals(BookId)).ToListAsync();
        }
    }
}

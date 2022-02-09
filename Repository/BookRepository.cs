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
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateBook(Book book)
        {
            Create(book);
        }

        public async Task<string> DeleteBookAsync(int id)
        {
            Book book = await GetBookAsync(id);
            if (book == null)
            {
                return "The book record couldn't be found.";
            }
            Delete(book);
            return "Book Deleted Successfully";
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync() =>

            await FindAll().OrderBy(b => b.BookId).ToListAsync();

        public Book GetBook(int BookId)
        {
            return FindByCondition(b => b.BookId.Equals(BookId)).SingleOrDefault();
        }

        public async Task<Book> GetBookAsync(int bookId)=>
        
            await FindByCondition(b => b.BookId.Equals(bookId)).SingleOrDefaultAsync();
        

        public async Task<IEnumerable<Book>> GetBooksAsync(int categoryId)=>
        
            await FindByCondition(b => b.CategoryId.Equals(categoryId)).ToListAsync();


        void IBookRepository.UpdateBookAsync(Book book)
        {
            Book bookEntity = GetBook(book.BookId);
            if(bookEntity!=null)
            UpdateAsync(book);
        }
    }
}

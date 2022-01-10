﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string DeleteBook(int id)
        {
            Book book =GetBook(id, false);
            if (book == null)
            {
                return "The book record couldn't be found.";
            }
            Delete(book);
            return "Book Deleted Successfully";
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges) =>

            FindAll(trackChanges).OrderBy(b => b.BookId).ToList();


        public Book GetBook(int bookId, bool trackChanges)=>
        
            FindByCondition(b => b.BookId.Equals(bookId), trackChanges).SingleOrDefault();
        

        public IEnumerable<Book> GetBooks(int categoryId, bool trackChanges)=>
        
            FindByCondition(b => b.CategoryId.Equals(categoryId), trackChanges).ToList();


        void IBookRepository.UpdateBook(Book book)
        {
            Update(book);
        }
    }
}

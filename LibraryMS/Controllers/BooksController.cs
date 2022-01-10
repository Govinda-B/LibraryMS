﻿using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using LibraryMS.Interface;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IBookBL _bookbl;

        public BooksController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper, IBookBL bookbl)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _bookbl = bookbl;
        }

        [HttpGet] 
        public IActionResult GetBooks() 
        {
            var books = _repository.Book.GetAllBooks(trackChanges: false);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksDto);    
        }

        //[HttpGet("{id}")]
        //public IActionResult GetBook(int id) 
        //{
        //    var book = _repository.Book.GetBook(id, trackChanges: false);
        //    if (book == null) 
        //    {
        //        _logger.LogInfo($"Book with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }
        //    else 
        //    {
        //        return Ok(book); 
        //    } 
        //}

        [HttpGet("{categoryid}")]
        public async Task<IActionResult> GetBooksForCategory(int categoryid)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryid, trackChanges: false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {categoryid} doesn't exist in the database.");
                return NotFound();
            }
            var booksforcategory = _repository.Book.GetBooks(categoryid, trackChanges: false);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksforcategory);
            return Ok(booksDto);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateDto book)
        {
            var category = await _repository.Category.GetCategoryAsync(book.CategoryId,false);
            if (category == null)
            {
                _logger.LogError("Category sent from client is null.");
                return BadRequest("Category does not exist.");
            }
            var bookEntity = _mapper.Map<Book>(book);
            _repository.Book.CreateBook(bookEntity);
            await _repository.SaveAsync();
            return Ok("Successully Added");
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response= _repository.Book.DeleteBook(id);
            await _repository.SaveAsync();
            return Ok(response);
        }
        
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookCreateDto book)
        {
            return Ok( await _bookbl.UpdateBookbyId(id,book));
            //Book bookToUpdate = _repository.Book.GetBook(id,false);
            //if (bookToUpdate == null)
            //{
            //    return NotFound("The book record couldn't be found.");
            //}
            //var bookEntity = _mapper.Map<Book>(book);
            //bookEntity.BookId= id;
            //_repository.Book.UpdateBook(bookEntity);
            //await _repository.SaveAsync();
            //return Ok("Updated Successfully");
        }

        [HttpGet]
        [Route("AvailableBooks")]
        public IActionResult GetAvailableBooks(int bookid, string d)
        {
            var date = DateTime.Parse(d);
            var book = _repository.Book.GetBook(bookid, trackChanges: false);
            if (book == null)
            {
                _logger.LogInfo($"Book with id: {bookid} doesn't exist in the database.");
                return NotFound();
            }
            int availablebooks =book.NoOfBooks - _repository.BookDate.FindNpofBooksForDate(bookid, date, false);
            return Ok(availablebooks);
        }
    }
}

using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BookService :ServiceBase, IBookService
    {
        IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository, ILogger logger):base(logger)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            try
            {
                var newBook = await bookRepository.CreateBookAsync(book);
                return newBook;
            }
            catch (DataException e)
            {
                logger.Error(e, "Error Adding new book");
                throw new Exception("Error Adding book to db");
            }
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                var bookList = await bookRepository.GetBooksAsync();
                return bookList.ToList();
            }
            catch (DataException e)
            {
                logger.Error(e, "Error getting books");
                throw new Exception("Error getting books from db");
            }
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            try
            {
                var newBook = await bookRepository.EditBookAsync(book);
                return newBook;
            }
            catch (DataException e)
            {
                logger.Error(e, "Error Updating book");
                throw new Exception("Error Updating book");
            }
        }
    }
}

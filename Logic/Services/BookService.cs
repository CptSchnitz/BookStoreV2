﻿using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BookService : ServiceBase, IBookService
    {
        IBookRepository bookRepository;
        IDiscountService discountService;
        public BookService(IBookRepository bookRepository, IDiscountService discountService, ILogger logger) : base(logger)
        {
            this.bookRepository = bookRepository;
            this.discountService = discountService;
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
                logger?.Error(e, "Error Adding new book");
                throw new DataException("Error Adding book to db");
            }
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> bookList;
            try
            {
                bookList = (await bookRepository.GetBooksAsync()).ToList();
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error getting books");
                throw new DataException("Error getting books from db");
            }

            // settings the prices after the discount
            await discountService.SetItemsPricesAsync(bookList.Cast<AbstractItem>());

            return bookList.ToList();
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
                logger?.Error(e, "Error Updating book");
                throw new DataException("Error Updating book");
            }
        }
    }
}

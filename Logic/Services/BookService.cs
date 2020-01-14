using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class BookService :ServiceBase, IBookService
    {
        IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository, ILogger logger):base(logger)
        {
            this.bookRepository = bookRepository;
        }
    }
}

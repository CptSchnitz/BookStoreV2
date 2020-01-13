using DAL.BookStoreRepository;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    class BookService : IBookService
    {
        IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
    }
}

using DAL.BookStoreRepository;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
    }
}

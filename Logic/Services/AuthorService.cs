﻿using Common.Model;
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
    public class AuthorService : ServiceBase ,IAuthorService
    {
        IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository authorRepository, ILogger logger) : base(logger)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            try
            {
                var newAuthor = await authorRepository.CreateAuthorAsync(author);
                return newAuthor;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Adding new author");
                throw new DataException("Error Adding author to db");
            }
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            try
            {
                var authorList = (await authorRepository.GetAuthorsAsync()).ToList();
                return authorList;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error loading Authors");
                throw new DataException("Error getting data from db");
            }
        }
    }
}

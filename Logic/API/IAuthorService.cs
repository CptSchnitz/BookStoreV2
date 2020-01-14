﻿using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface IAuthorService
    {
        Task<Author> AddAuthorAsync(Author author);
        Task<List<Author>> GetAuthorsAsync();
    }
}

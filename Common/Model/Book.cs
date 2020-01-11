using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class Book : AbstractItem
    {
        public int Edition { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

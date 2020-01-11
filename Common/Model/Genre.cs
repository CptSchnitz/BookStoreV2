using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}

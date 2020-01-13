using System.Collections.Generic;

namespace Common.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}

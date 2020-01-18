using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}

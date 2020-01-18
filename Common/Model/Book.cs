using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public class Book : AbstractItem
    {
        [Required]
        [Range(1,100)]
        public int Edition { get; set; }
        [Required]
        [MaxLength(20)]
        public string Isbn { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [ForeignKey(nameof(Author))]
        [Required]

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}

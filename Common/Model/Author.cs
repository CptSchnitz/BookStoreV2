using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }
        [MaxLength(80)]
        public string PseuduName { get; set; }
    }
}

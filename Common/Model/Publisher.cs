using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string ContactEmail { get; set; }
    }
}

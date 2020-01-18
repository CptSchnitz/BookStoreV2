using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Journal : AbstractItem
    {
        [Required]
        [MaxLength(20)]
        public string Issn { get; set; }
        [Range(1,1000)]
        public int IssueNum { get; set; }
    }
}

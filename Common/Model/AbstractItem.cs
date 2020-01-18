using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public abstract class AbstractItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [Range(1.0,1000.0)]
        public decimal Price { get; set; }

        [NotMapped]
        public decimal DiscountedPrice { get; set; }

        [NotMapped]
        public string DiscountType { get; set; }
        public DateTime PublishDate { get; set; }

        [Required]
        [Range(0,1000)]
        public int AmountInStock { get; set; }

        [ForeignKey(nameof(Publisher))]
        [Required]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}

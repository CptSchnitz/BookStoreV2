using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public abstract class AbstractItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public decimal DiscountedPrice { get; set; }

        [NotMapped]
        public string DiscountType { get; set; }
        public DateTime PublishDate { get; set; }

        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public class GenreDiscount : BaseDiscount
    {
        public GenreDiscount(Genre genre ,int amount) : base(amount)
        {
            GenreId = genre.Id;
        }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.ItemGenres.Any(ig => ig.GenreId == GenreId);
        }
    }
}

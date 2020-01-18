using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Common.Model
{
    public class GenreDiscount : BaseDiscount
    {
        //for ef use
        protected GenreDiscount() { }
        public GenreDiscount(Genre genre, int amount) : base(amount)
        {
            GenreId = genre.Id;
        }

        [ForeignKey(nameof(Genre))]
        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.ItemGenres.Any(ig => ig.GenreId == GenreId);
        }

        public override bool IsSameDiscount(BaseDiscount other)
        {
            var otherGenreDiscount = other as GenreDiscount;
            if (otherGenreDiscount == null) return false;
            return otherGenreDiscount.GenreId == GenreId;
        }
    }
}

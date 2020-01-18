using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public class AuthorDiscount : BaseDiscount
    {
        //For Ef Use
        protected AuthorDiscount() { }
        public AuthorDiscount(Author author, int amount) : base(amount)
        {
            if (author is null)
            {
                throw new System.ArgumentNullException(nameof(author));
            }

            AuthorId = author.Id;
        }

        [ForeignKey(nameof(Author))]
        [Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }


        // checks if this discount is applicable for the item
        public override bool IsDiscountValid(AbstractItem item)
        {
            // only book got author
            var book = item as Book;
            if (book == null || book.AuthorId != AuthorId)
                return false;
            else
                return true;
        }

        public override bool IsSameDiscount(BaseDiscount other)
        {
            var otherAuthorDiscount = other as AuthorDiscount;
            if (otherAuthorDiscount == null) return false;
            return otherAuthorDiscount.AuthorId == AuthorId;
        }
    }
}

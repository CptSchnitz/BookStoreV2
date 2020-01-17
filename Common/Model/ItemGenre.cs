namespace Common.Model
{
    // The type for the connecting table between genre and items
    public class ItemGenre
    {
        public int AbstractItemId { get; set; }
        public virtual AbstractItem AbstractItem { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}

namespace Common.Model
{
    public class ItemGenre
    {
        public int AbstractItemId { get; set; }
        public virtual AbstractItem AbstractItem { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class ItemGenre
    {
        public int AbstractItemId { get; set; }
        public AbstractItem AbstractItem { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}

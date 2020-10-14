using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Games
    {
        public Games()
        {
            GameReviews = new HashSet<GameReviews>();
            WishLists = new HashSet<WishLists>();
        }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public decimal ListPrice { get; set; }
        public string ContentRating { get; set; }
        public string Genre { get; set; }
        public string AvailablePlatforms { get; set; }
        public string MaxPlayers { get; set; }

        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<WishLists> WishLists { get; set; }
    }
}

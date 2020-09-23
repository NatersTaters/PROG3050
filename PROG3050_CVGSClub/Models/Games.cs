using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Games
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public decimal ListPrice { get; set; }
        public string ContentRating { get; set; }
        public string Genre { get; set; }
        public string AvailablePlatforms { get; set; }
        public string MaxPlayers { get; set; }
    }
}

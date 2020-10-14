using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class WishLists
    {
        public int WishId { get; set; }
        public string MemberId { get; set; }
        public int GameId { get; set; }

        public Games Game { get; set; }
        public Members Member { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class FriendsFamily
    {
        public int FriendFamilyId { get; set; }
        public int MemberId { get; set; }
        public int FriendId { get; set; }

        public Members Member { get; set; }
    }
}

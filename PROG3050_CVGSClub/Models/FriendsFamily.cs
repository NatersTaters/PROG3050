using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class FriendsFamily
    {
        public int FriendFamilyId { get; set; }
        public string MemberId { get; set; }
        public string FriendId { get; set; }

        public Members Member { get; set; }
    }
}

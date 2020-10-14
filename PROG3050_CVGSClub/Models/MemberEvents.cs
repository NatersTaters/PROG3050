using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class MemberEvents
    {
        public int EventId { get; set; }
        public string MemberId { get; set; }

        public Events Event { get; set; }
        public Members Member { get; set; }
    }
}

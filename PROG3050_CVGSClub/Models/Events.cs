using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Events
    {
        public Events()
        {
            MemberEvents = new HashSet<MemberEvents>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }

        public ICollection<MemberEvents> MemberEvents { get; set; }
    }
}

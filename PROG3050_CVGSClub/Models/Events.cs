using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Events
    {
        public int EventId { get; set; }
        public int MemberId { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }

        public Members Member { get; set; }
    }
}

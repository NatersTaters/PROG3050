using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Addresses
    {
        public int AddressId { get; set; }
        public int MemberId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        public Members Member { get; set; }
    }
}

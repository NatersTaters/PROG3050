using System;
using System.Collections.Generic;

namespace PROG3050_CVGSClub.Models
{
    public partial class Members
    {
        public Members()
        {
            FriendsFamily = new HashSet<FriendsFamily>();
            GameReviews = new HashSet<GameReviews>();
            GamesLibrary = new HashSet<GamesLibrary>();
            MemberEvents = new HashSet<MemberEvents>();
            WishLists = new HashSet<WishLists>();
        }

        public string MemberId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? ReceiveEmails { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardExpires { get; set; }

        public ICollection<FriendsFamily> FriendsFamily { get; set; }
        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<GamesLibrary> GamesLibrary { get; set; }
        public ICollection<MemberEvents> MemberEvents { get; set; }
        public ICollection<WishLists> WishLists { get; set; }
    }
}

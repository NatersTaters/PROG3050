using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
{
    public class FamilyAndFriendService : IFamilyAndFriendService
    {
        public void AddFamily(CvgsClubContext context, FriendsFamily friends, string id, string memberId)
        {
            try
            {
                if (id == null || memberId == null || friends == null || context == null)
                    throw new Exception();

                friends.MemberId = memberId;
                friends.FriendId = id;
                context.Add(friends);

            }
            catch(Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw e;
            }
        }
    }
}

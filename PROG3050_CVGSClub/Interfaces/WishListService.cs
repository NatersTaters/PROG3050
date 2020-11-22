using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
{
    public class WishListService : IWishListService
    {
        public WishLists AddWishlist(int wishId, string memberId)
        {
            //New Wishlist created, supply wishid and memberid to it
            WishLists newWishList = new WishLists();
            newWishList.WishId = wishId;
            newWishList.MemberId = memberId;

            return (newWishList);
        }
    }
}

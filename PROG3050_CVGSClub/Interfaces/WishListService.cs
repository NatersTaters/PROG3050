using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROG3050_CVGSClub.Controllers;
using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
{
    public class WishListService : IWishListService
    {
        public WishLists AddWishlist(int gameId, string memberId)
        {
            //New Wishlist created, supply wishid and memberid to it
            WishLists newWishList = new WishLists();
            newWishList.GameId = gameId;
            newWishList.MemberId = memberId;

            return (newWishList);
        }
    }
}

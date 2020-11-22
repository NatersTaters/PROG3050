using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG3050_CVGSClub.Interfaces
{
    public interface IWishListService
    {
        WishLists AddWishlist(int wishId, string memberId);
    }
}

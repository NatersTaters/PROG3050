using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class WishListUnitTest
    {
        Random r = new Random();
        WishLists wishlist;
        Games game;
        CvgsClubContext context = new CvgsClubContext();

        private void Initialize()
        {
            try { context.Entry(game).State = EntityState.Detached; }
            catch (Exception) { }

            wishlist = new WishLists()
            {
                WishId = r.Next(),
                MemberId = "TestMemberId",
                GameId = r.Next(), 
                Game = game,
            };
        }

        [Fact]
        public void GoodWishlistCreation_AllowTheCreation()
        {
            //Arrange
            Initialize();

            //Act
            context.WishLists.Add(wishlist);

            //Assert
            context.EFValidation();
        }

        [Fact]
        public void NegativeWishList_ShouldStillWork()
        {
            //Arrange
            Initialize();
            wishlist.WishId = -5;

            //Act
            context.WishLists.Add(wishlist);

            //Assert
            context.EFValidation();
        }

        [Fact]
        public void LargeGameId_ShouldPass()
        {
            //Arrange
            Initialize();
            wishlist.GameId = 5000;

            //Act
            context.WishLists.Add(wishlist);

            //Assert
            context.EFValidation();
        }
    }
}

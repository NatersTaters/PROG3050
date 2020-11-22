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

            game = new Games()
            {
                GameId = r.Next(),
                GameName = "GameTest",
                ListPrice = 60,
                ContentRating = "M",
                Genre = "Adventure",
                AvailablePlatforms = "PC",
                MaxPlayers = "100"
            };

            wishlist = new WishLists()
            {
                WishId = r.Next(),
                MemberId = "40",
                GameId = r.Next(), 
                Game = game,
            };
        }

        [Fact]
        public void GoodWishlistAddition_AllowTheAddition()
        {
            //Arrange
            Initialize();

            //Act
            context.Games.Add(game);

            //Assert
            context.EFValidation();
        }
    }
}

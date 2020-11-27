using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Helpers;
using PROG3050_CVGSClub.Interfaces;
using PROG3050_CVGSClub.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
	public class CartUnitTest
	{
		CartDependency context = new CartDependency();

		public Games InitializeGame()
        {
			Games sampleGame = new Games
			{
				GameId = 99,
				GameName = "Trial Game",
				ListPrice = 50,
				ContentRating = "M",
				Genre = "Action",
				AvailablePlatforms = "PC",
				MaxPlayers = "5"
			};

			return sampleGame;
		}

		public List<Item> InitializeCart()
        {
			Games sampleGame = InitializeGame();

			Item game = new Item
			{
				Game = sampleGame,
				Quantity = 2
			};

			List<Item> sampleCart = new List<Item>();
			sampleCart.Add(game);

			return sampleCart;
        }

		// Call TotalSum()
		// Pass in the initialized cart
		// Game price is 50, quantity is 2. Return 100
		[Fact]
		public void TotalSum_SendCart_Return100()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			double expectedValue = 100;

			// Act
			double actualValue = context.TotalSum(cart);

			// Assert
			Assert.Equal(expectedValue, actualValue);
		}

		// Call TaxAmount()
		// Total is 100, tax is 0.13. Return 13
		[Fact]
		public void TaxAmount_Send100And13Percent_Return13()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			double expectedValue = 13;

			// Act
			double actualValue = context.TaxAmount(100);

			// Assert
			Assert.Equal(expectedValue, actualValue);
		}

		// Call FinalCost()
		// Total is 100, Tax is 13. Return 113
		[Fact]
		public void FinalCost_Send100And13_Return113()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			double expectedValue = 113;

			// Act
			double actualValue = context.FinalCost(100, 13);

			// Assert
			Assert.Equal(expectedValue, actualValue);
		}

		// Call TotalSum()
		// Send in a null value. Exception should be thrown
		[Fact]
		public void TotalSum_SendNull_ReturnError()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			Exception exception = null;

			// Act
			try
			{
				var actualValue = context.TotalSum(null);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.NotNull(exception);
		}

		// Call AddToCart()
		// Send in the initialized game and existing cart
		// Total games in cart should increase by 1
		[Fact]
		public void AddToCart_SendGameToAddWithExistingCart_CartCountIncreasesBy1()
        {
			// Arrange
			List<Item> cart = InitializeCart();
            Games game = InitializeGame();
			int cartCountBefore = cart.Count;
			int expectedValue = 1;

			// Act
			cart = context.AddToCart(game, cart);
            int cartCountAfter = cart.Count;
			int actualValue = cartCountAfter - cartCountBefore;

            // Assert
            Assert.Equal(actualValue, expectedValue);
		}
	
		// Call FindGameIndex()
		// Send id of game and the existing cart
		// Return the correct index to remove (1)
		[Fact]
		public void FindGameIndex_SendGameIdAndCart_ReturnIndex1()
        {
			// Arrange
			List<Item> cart = InitializeCart();
			Games game = InitializeGame();
			int gameId = game.GameId;
			int expectedValue = 0;

			// Act
			int actualValue = context.FindGameIndex(gameId, cart);

			// Assert
			Assert.Equal(actualValue, expectedValue);
		}

		// Call BuyGame()
		// Send game id, existing cart and the game of interest
		// Current quantity in cart is 2. Expected quantity to increase to 3
		[Fact]
		public void BuyGame_SendGameIdAndExistingCartAndGame_QuantityIncreaseTo3()
        {
			// Arrange
			List<Item> cart = InitializeCart();
			Games game = InitializeGame();
			int expectedValue = 3;

			// Act
			cart = context.BuyGame(game.GameId, cart, game);
			int actualValue = cart[0].Quantity;
			
			// Assert
			Assert.Equal(actualValue, expectedValue);
		}
	
		// Call RemoveGame()
		// Send game id and existing cart
		// No matter the quantity, remove should remove the entire item from cart
		// Expected to have a cart count of 0
		[Fact]
		public void RemoveGame_SendGameIdAndExistingCart_RemoveTheGame()
        {
			// Arrange
			List<Item> cart = InitializeCart();
			Games game = InitializeGame();
			int expectedValue = 0;

			// Act
			cart = context.RemoveGame(game.GameId, cart);
			int actualValue = cart.Count;

			// Assert
			Assert.Equal(actualValue, expectedValue);
		}
	
		// Call GameLibrary()
		// Send the existing cart, the logged in member id and the index of the cart
		// GameLibrary should successfully return a viable GameLibrary object
		[Fact]
		public void GameLibrary_SendExistingCartAndLoggedMemberId_ReturnGameLibrary()
        {
			// Arrange
			List<Item> cart = InitializeCart();
			GamesLibrary gamesLibrary;
			int index = 0;
			string memberId = "ABC";

			// Act
			gamesLibrary = context.GameLibrary(cart, memberId, index);

			// Assert
			Assert.NotNull(gamesLibrary);
		}
	}
}

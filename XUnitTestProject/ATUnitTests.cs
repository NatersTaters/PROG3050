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
	public class ATUnitTests
	{
		CartDependency context = new CartDependency();

		public List<Item> InitializeCart()
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

			Item game = new Item
			{
				Game = sampleGame,
				Quantity = 2
			};

			List<Item> sampleCart = new List<Item>();
			sampleCart.Add(game);

			return sampleCart;
        }

		// Call total sum function
		// Give them the list of games currently in the cart
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

		// Call tax amount function
		// Total is 100, tax is 0.13. Return 13
		[Fact]
		public void TaxAmount_Send100And13Percent_Return13()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			double expectedValue = 13;

			// Act
			double actualValue = context.TaxAmount(100, 0.13);

			// Assert
			Assert.Equal(expectedValue, actualValue);
		}

		// Call final amount function
		// Total is 100, Tax is 13. Return 113
		[Fact]
		public void FinalAmount_Send100And13_Return113()
		{
			// Arrange
			List<Item> cart = InitializeCart();
			double expectedValue = 113;

			// Act
			double actualValue = context.FinalCost(100, 13);

			// Assert
			Assert.Equal(expectedValue, actualValue);
		}

		// Call total sum function
		// Send in a null value. Exception should be thrown
		// NOT FINISHED!
		[Fact]
		public void TotalSum_SendNull_ReturnError()
		{
			// Arrange
			List<Item> cart = InitializeCart();

			// Act
			var actualValue = context.TotalSum(null);

			// Assert
			// Assert.Throws(Exc);
		}
	}
}

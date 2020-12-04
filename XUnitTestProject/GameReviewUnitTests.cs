using System;
using Xunit;
using PROG3050_CVGSClub.Controllers;
using PROG3050_CVGSClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject
{
	public class GameReviewUnitTests
	{
		Random r = new Random();
		GameReviews gameReviews;
		Games games;
		Members members;
		CvgsClubContext context = new CvgsClubContext();

		private void Initialize()
		{
			try { context.Entry(gameReviews).State = EntityState.Detached; }
			catch (Exception) { }

			games = new Games()
			{
				GameId = r.Next(),
				GameName = "TestGame",
				ListPrice = 10,
				ContentRating = "R",
				Genre = "TestGenre",
				AvailablePlatforms = "TestPlatform",
				MaxPlayers = "100"
			};

			members = new Members()
			{
				MemberId = "TestMemberId",
				DisplayName = "TestDisplayName",
				FirstName = "TestFirstName",
				LastName = "TestLastName",
				Email = "test@email.com",
				Password = "TestPassword",
				Gender = "M",
				BirthDate = DateTime.Now,
				ReceiveEmails = false,
				CardType = "TestCard",
				CardNumber = "1234567890987654",
				CardExpires = "05/24"
			};

			gameReviews = new GameReviews()
			{
				ReviewId = r.Next(),
				MemberId = members.MemberId,
				GameId = games.GameId,
				GameReview = "TestGameReview",
				Game = games,
				Member = members
			};
		}
		[Fact]
		public void GoodReviewCreation_ShouldAllowCreate()
		{
			// Arrange
			Initialize();

			// Act
			context.GameReviews.Add(gameReviews);

			// Assert
			context.EFValidation();
		}
		[Fact]
		public void NullMember_ShouldThrowError()
		{
			// Arrange
			Initialize();

			// Act
			gameReviews.Member = null;
			context.GameReviews.Add(gameReviews);

			// Assert
			context.EFValidation();
		}
		[Fact]
		public void NullGame_ShouldThrowError()
		{
			// Arrange
			Initialize();

			// Act
			gameReviews.Game = null;
			context.GameReviews.Add(gameReviews);

			// Assert
			context.EFValidation();
		}
	}
}

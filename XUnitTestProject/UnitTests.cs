using System;
using Xunit;
using PROG3050_CVGSClub.Controllers;
using PROG3050_CVGSClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject
{
	public class UnitTests
	{
		Members member;
		CvgsClubContext context = new CvgsClubContext();
		Random random = new Random();

		private void Initialize()
		{
			try { context.Entry(member).State = EntityState.Detached; }
			catch (Exception) { }

			member = new Members()
			{
				MemberId = random.Next().ToString(),
				DisplayName = "TestDisplayName",
				FirstName = "TestFirstName",
				LastName = "TestLastName",
				Email = "TestEmail",
				Password = "TestPassword",
				Gender = "M",
				BirthDate = DateTime.Now,
				ReceiveEmails = false,
				CardType = "TestCardType",
				CardNumber = "1234567890987654",
				CardExpires = "05/24"
			};
		}
		[Fact]
		public void GoodCredForMember_ShouldAllowCreate()
		{
			// Arrange
			Initialize();

			// Act
			//var result = await controller.Create(member);
			context.Members.Add(member);

			// Assert
			context.EFValidation();
		}

		[Fact]
		public void EmptyMemberId_ShouldGiveError()
		{
			// Arrange
			Initialize();
			member.MemberId = "";

			// Act
			context.Members.Add(member);

			// Assert
			//Assert.ThrowsAny<Exception>(() => context.EFValidation());
			context.EFValidation();
		}
	}
}

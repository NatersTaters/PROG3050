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
	public class FamilyAndFriendUnitTest
	{
		CvgsClubContext context = new CvgsClubContext();
		FamilyAndFriendService contextFriend = new FamilyAndFriendService();


		// Call AddFamily()
		// Send it CVGS context, the friend and family object, the id of the friend to be added and the logged in member
		// There should be no exception.
		[Fact]
		public void AddFamily_SendCvgsContextAndFriendAndIdAndLoginMemberId_ReturnNoException()
		{
			// Arrange
			FriendsFamily friendsFamily = new FriendsFamily();
			string id = "FRIEND";
			string memberId = "USER";
			Exception exception = null;

			// Act
			try
			{
				contextFriend.AddFamily(context, friendsFamily, id, memberId);
			}
			catch (Exception e)
			{
				exception = e;
			}

			// Assert
			Assert.Null(exception);
			context.EFValidation();
		}
	}
}

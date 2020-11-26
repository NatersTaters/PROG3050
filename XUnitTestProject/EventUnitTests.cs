using System;
using Xunit;
using PROG3050_CVGSClub.Controllers;
using PROG3050_CVGSClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject
{
	public class EventUnitTests
	{
		Random r = new Random();
		MemberEvents memberEvents;
		Events events;
		CvgsClubContext context = new CvgsClubContext();

		private void Initialize()
		{
			try { context.Entry(memberEvents).State = EntityState.Detached; }
			catch (Exception) { }

			events = new Events()
			{
				EventId = r.Next(),
				EventName = "TestEvent",
				EventDate = DateTime.Now,
				StartTime = TimeSpan.Zero,
				EndTime = TimeSpan.Zero,
				Capacity = 500
			};

			memberEvents = new MemberEvents()
			{
				MemberEventsId = r.Next(),
				EventId = events.EventId,
				MemberId = "TestMemberId",
				Event = events
			};
		}
		[Fact]
		public void GoodEventCreation_ShouldAllowCreate()
		{
			// Arrange
			Initialize();

			// Act
			context.MemberEvents.Add(memberEvents);

			// Assert
			context.EFValidation();
		}
		[Fact]
		public void NullMemberId_ShouldThrowError()
		{
			// Arrange
			Initialize();

			// Act
			memberEvents.MemberId = null;
			context.MemberEvents.Add(memberEvents);

			// Assert
			context.EFValidation();
		}
		[Fact]
		public void NullEvent_ShouldThrowError()
		{
			// Arrange
			Initialize();

			// Act
			memberEvents.Event = null;
			context.MemberEvents.Add(memberEvents);

			// Assert
			context.EFValidation();
		}
	}
}

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
	public class EventService : IEventService
	{
		public MemberEvents Subscribe(int eventId, string memberId)
		{
			// Create a new MemberEvent object and add the memberId string reference object and the id supplied to this method
			MemberEvents newMemberEvent = new MemberEvents();
			newMemberEvent.MemberId = memberId;
			newMemberEvent.EventId = eventId;

			return (newMemberEvent);
		}
    }
}

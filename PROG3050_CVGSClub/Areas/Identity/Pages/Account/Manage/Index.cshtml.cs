using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG3050_CVGSClub.Controllers;
using PROG3050_CVGSClub.Models;

namespace PROG3050_CVGSClub.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        // In order to retrieve the details of the member object that is currently signed in, we must work with
        // the context object of the database, here it's being declared for use within the class
        private CVGSClubContext context = new CVGSClubContext();

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool ReceiveEmails { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public string CardExpires { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Gender { get; set; }

            public DateTime BirthDate { get; set; }

            public bool ReceiveEmails { get; set; }

            public string CardType { get; set; }

            public string CardNumber { get; set; }

            public string CardExpires { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Retrieves the details for the member object that is signed inwith the GetUserId method of the Identity _userManager
            var userIdForMembers = await _userManager.GetUserIdAsync(user);
            var members = await context.Members.FindAsync(userIdForMembers);

            // Pre-Sets all of the values for the "Profile Informtaion" page with a combination of data from the user object of the
            // localDB database and data from the member object of the SQL database
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);

            var firstName = members.FirstName;
            if (firstName == null)
			{
                firstName = "";
			}

            var lastName = members.LastName;
            if (lastName == null)
			{
                lastName = "";
			}

            var gender = members.Gender;
            if (gender == null)
			{
                gender = "";
			}

            var birthDate = members.BirthDate;
            if (birthDate == null)
			{
                birthDate = DateTime.Now;
			}

            var receiveEmails = members.ReceiveEmails;
            
            var cardType = members.CardType;
            if (cardType == null)
			{
                cardType = "";
			}

            var cardNumber = members.CardNumber;
            if (cardNumber == null)
			{
                cardNumber = "";
			}

            var cardExpires = members.CardExpires;
            if (CardExpires == null)
			{
                cardExpires = "";
			}

            Input = new InputModel
            {
                Username = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDate = (DateTime)birthDate,
                ReceiveEmails = (bool)receiveEmails,
                CardType = cardType,
                CardNumber = cardNumber,
                CardExpires = cardExpires
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var username = await _userManager.GetUserNameAsync(user);
            if (Input.Username != username)
            {
                var setUsernameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUsernameResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting username for user with ID '{userId}'.");
                }
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            // Retrieves the details for the member object that is signed with the GetUserId method of the Identity _userManager
            var userIdForMembers = await _userManager.GetUserIdAsync(user);
            var members = await context.Members.FindAsync(userIdForMembers);

            // Sets all of the member data values with the user input for each item on the "Profile Information" page
            members.DisplayName = Input.Username;
            members.Email = Input.Email;
            members.FirstName = Input.FirstName;
            members.LastName = Input.LastName;
            members.Gender = Input.Gender;
            members.BirthDate = Input.BirthDate;
            members.ReceiveEmails = Input.ReceiveEmails;
            members.CardType = Input.CardType;
            members.CardNumber = Input.CardNumber;
            members.CardExpires = Input.CardExpires;

            // Calls upon the Edit POST method of the CreateMembersController and supplies it with the new data values for
            // the current member object
            var createMember = new CreateMembersController(context);
            await createMember.Edit(_userManager.GetUserId(User), members);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";

            return RedirectToPage();
        }
    }
}

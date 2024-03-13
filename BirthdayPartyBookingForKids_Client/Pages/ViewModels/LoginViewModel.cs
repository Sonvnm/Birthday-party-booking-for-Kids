using System.ComponentModel.DataAnnotations;

namespace BirthdayPartyBookingForKids_Client.Pages.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

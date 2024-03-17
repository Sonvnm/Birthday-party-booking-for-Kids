using System.ComponentModel.DataAnnotations;

namespace BirthdayPartyBookingForKids_Client.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string RoleId { get; set; }
    }
}

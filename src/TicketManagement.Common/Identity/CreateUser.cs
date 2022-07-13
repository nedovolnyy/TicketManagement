using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Common.Identity
{
    public class CreateUser : User
    {
        [Required]
        public override string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public override string Email { get; set; } = null!;

        [EmailAddress]
        [Display(Name = "UserName")]
        public override string UserName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        public override string PhoneNumber { get; set; }

        public override string FirstName { get; set; }

        public override string SurName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Common.Identity
{
    public class User : IdentityUser
    {
        public virtual string FirstName { get; set; }

        public virtual string SurName { get; set; }

        public virtual string Language { get; set; }

        public virtual string TimeZone { get; set; }

        public virtual int CartCount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:$#.##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Balance")]
        public virtual decimal Balance { get; set; }

        public virtual string PayHistory { get; set; }
    }
}
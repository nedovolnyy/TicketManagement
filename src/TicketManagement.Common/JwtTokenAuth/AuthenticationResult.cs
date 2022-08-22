using System.Collections.Generic;
using TicketManagement.Common.Identity;

namespace TicketManagement.Common.JwtTokenAuth
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public bool Result { get; set; }

        public User User { get; set; }

        public List<string> Errors { get; set; }
    }
}

﻿namespace TicketManagement.UserAPI.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public bool Result { get; set; }

        public List<string> Errors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public static class LoginSettings
    {
        public static string Email { get; set; }

        public static string Password { get; set; }

        public static Guid UserId { get; set; }

        public static string userId
        {
            get { return "userId"; }
        }

        public static string email
        {
            get { return "email"; }
        }

        public static string password
        {
            get { return "password"; }
        }

    }
}

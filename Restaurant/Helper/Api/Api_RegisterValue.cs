using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Helper.Api
{
    public class Api_RegisterValue
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}
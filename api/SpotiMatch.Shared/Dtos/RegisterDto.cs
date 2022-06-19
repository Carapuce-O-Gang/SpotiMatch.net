﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string AuthorizationToken { get; set; }
    }
}

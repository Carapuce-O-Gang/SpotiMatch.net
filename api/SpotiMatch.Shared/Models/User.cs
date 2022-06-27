﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string AuthorizationToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

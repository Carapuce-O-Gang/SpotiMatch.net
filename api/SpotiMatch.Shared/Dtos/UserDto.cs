using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

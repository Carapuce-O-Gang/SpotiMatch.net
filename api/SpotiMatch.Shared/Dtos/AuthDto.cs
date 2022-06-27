using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Dtos
{
    public class AuthDto
    {
        public string TokenType { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Dtos.Spotify
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}

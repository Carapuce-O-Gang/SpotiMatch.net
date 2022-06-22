using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiMatch.Shared.Dtos.Spotify
{
    public class ProfileDto
    {
        public string Country { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public ExplicitContentDto ExplicitContent { get; set; }
        public ExternalUrlsDto ExternalUrls { get; set; }
        public FollowersDto Followers { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public ImageDto[] Images { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}

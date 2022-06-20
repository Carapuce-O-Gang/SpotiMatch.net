using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotiMatch.Database.Entities
{
    public class User
    {
        public User()
        {
            Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }

        [MaxLength(200)]
        public string AuthorizationToken { get; set; }

        [MaxLength(200)]
        public string AccessToken { get; set; }

        [MaxLength(200)]
        public string RefreshToken { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Image> Images { get; set;}
    }
}

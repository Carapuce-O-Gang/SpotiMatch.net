using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiMatch.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

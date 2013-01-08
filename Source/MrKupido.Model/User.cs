using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace MrKupido.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [MaxLength(7)]
        [MinLength(2)]
        [StringLength(7, MinimumLength = 2)]
        public string CultureName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(110)]
        [MinLength(3)]
        [StringLength(110, MinimumLength = 3)]
        public string FullName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        public string NickName { get; set; }

        [Required]
        //public Gender Gender { get; set; }
        public int Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        [Required]
        public int NewsletterFlags { get; set; }
        
        public int? PrimaryAddressId { get; set; }
        //[ForeignKey("PrimaryAddressId")]
        //public Address PrimaryAddress { get; set; }

        public Address[] Addresses { get; set; }
        
        public Ingredient[] Likes { get; set; }
        public Ingredient[] Dislikes { get; set; }
        public Condition[] Conditions { get; set; }

        [MaxLength(110)]
        [MinLength(0)]
        [StringLength(110, MinimumLength = 0)]        
        public string AvatarUrl { get; set; }

        [Required]
        public DateTime FirstLoginUtc { get; set; }        
        [Required]
        public DateTime LastLoginUtc { get; set; }
    }

    public enum Gender { Unknown, Male, Female }
}

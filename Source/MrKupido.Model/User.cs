using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrKupido.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")]
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
        [RegularExpression(@"^[a-zA-Z''-'\sáéíóúöőüűÁÉÍÓÚÖŐÜŰ]{1,50}$")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z''-'\sáéíóúöőüűÁÉÍÓÚÖŐÜŰ]{1,50}$")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(110)]
        [MinLength(3)]
        [StringLength(110, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z''-'\sáéíóúöőüűÁÉÍÓÚÖŐÜŰ]{1,50}$")]
        public string FullName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z''-'\sáéíóúöőüűÁÉÍÓÚÖŐÜŰ]{1,50}$")]
        public string NickName { get; set; }

        [Required]
        //public Gender Gender { get; set; }
        [Range(0, 3)]
        public int Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Range(1.20, 2.20)]
        public float? Height { get; set; }

        [Range(35, 300)]
        public float? Weight { get; set; }

        [Required]
        public int NewsletterFlags { get; set; }

        public int? PrimaryAddressId { get; set; }
        //[ForeignKey("PrimaryAddressId")]
        //public Address PrimaryAddress { get; set; }

        public Address[] Addresses { get; set; }

        [NotMapped]
        public Uri AvatarUrl { get; set; }

        [Required]
        public DateTime FirstLoginUtc { get; set; }
        [Required]
        public DateTime LastLoginUtc { get; set; }

        [MaxLength(50)]
        [MinLength(0)]
        [StringLength(50, MinimumLength = 0)]
        public string GoogleId { get; set; }

        [MaxLength(50)]
        [MinLength(0)]
        [StringLength(50, MinimumLength = 0)]
        public string FacebookId { get; set; }

        [MaxLength(50)]
        [MinLength(0)]
        [StringLength(50, MinimumLength = 0)]
        public string WindowsLiveId { get; set; }
    }

    public enum Gender { Unknown, Male, Female, Other }
}

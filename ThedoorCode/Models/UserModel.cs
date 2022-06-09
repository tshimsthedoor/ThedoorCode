using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThedoorCode.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [Required]
        [Range(25, 55, ErrorMessage = "Currently, We Have no Positions Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Qualification { get; set; } = "";

        [Required]
        [Range(1, 25, ErrorMessage = "Currently, We Have no Positions Vacant for Your Experience")]
        public int TotalExperience { get; set; }

        public virtual List<Experience> Experiences { get; set; } = new List<Experience>();// details very important

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "Please choose the Profile Photo")]
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

     
    }

    public enum UserStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
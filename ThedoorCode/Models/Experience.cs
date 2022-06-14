using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThedoorCode.Models
{
    public class Experience
    {
        public Experience()
        {

        }

        [Key]
        public int ExperienceId { get; set; }

        [ForeignKey("UserModel")] // Very Important

       
        public int UserId { get; set; } 

        public string CompanyName { get; set; }

        public string Designation { get; set; }

        [Required]
        public int YearsWorked { get; set; }

        public bool SoftDeleted { get; set; }
    }
}
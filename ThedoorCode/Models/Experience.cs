using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //[DisplayName("Name")]
        //public virtual UserModel UserModel { get; private set; } // Very Important

        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        public string Designation { get; set; }

        [Required]
        [DisplayName("Years Worked")]
        public int YearsWorked { get; set; }
    }
}
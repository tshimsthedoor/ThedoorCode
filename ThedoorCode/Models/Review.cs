using System.ComponentModel.DataAnnotations;

namespace ThedoorCode.Models
{
    public class Review
    {

        public Review()
        {

        }

        [Key]
        public int ReviewId { get; set; }

        public virtual UserModel Author { get; set; }
    }
}
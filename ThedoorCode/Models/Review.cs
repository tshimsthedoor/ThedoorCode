using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThedoorCode.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("ImageModel")]
        public int ImageId { get; set; }

        public string COmment { get; set; }
    }
}
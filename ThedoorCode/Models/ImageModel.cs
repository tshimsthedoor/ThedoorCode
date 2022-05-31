﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThedoorCode.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("UserModel")] // Very Important
        public int UserId { get; set; }

        public string Description { get; set; }

        public string Images { get; set; }

        public string Location { get; set; }

        public int Latitude { get; set; }

        public int Longitude { get; set; }

        public virtual Review Review { get; set; }
    }
}

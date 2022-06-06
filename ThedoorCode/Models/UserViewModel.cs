using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThedoorCode.Models
{
    public class UserViewModel
    {
        public List<UserModel> UserModel { get; set; }

        public SelectList UserQualification { get; set; }

        public string TotalExperience { get; set; }

        public string SearchString { get; set; }
    }

}

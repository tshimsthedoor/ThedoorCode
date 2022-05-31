using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThedoorCode.Data;
using ThedoorCode.Models;

namespace ThedoorCode.Controllers
{
    public class UserController : Controller
    {

        private readonly UserDbContext _context;

        private readonly IWebHostEnvironment _webHost;

        public UserController(UserDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            List<UserModel> userModels;
            userModels = _context.UserModels.ToList();
            return View(userModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            UserModel userModel = new UserModel();
            userModel.Experiences.Add(new Experience() { ExperienceId = 1 });
            //userModel.Experiences.Add(new Experience() { ExperienceId = 2 });
            //userModel.Experiences.Add(new Experience() { ExperienceId = 3 });
            return View(userModel);
        }

        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            foreach (Experience experience in userModel.Experiences)
            {
                if (experience.CompanyName == null || experience.CompanyName.Length == 0)
                    userModel.Experiences.Remove(experience);
            }

            string uniqueFileName = GetUploadFileName(userModel);
            userModel.PhotoUrl = uniqueFileName;

            _context.Add(userModel);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        private string GetUploadFileName(UserModel userModel)
        {
            string uniqueFileName = null;
            if (userModel.ProfilePhoto != null)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + userModel.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    userModel.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ThedoorCode.Data;
using ThedoorCode.Models;

namespace ThedoorCode.Controllers
{
    public class UserController : Controller
    {

        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
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
            _context.Add(userModel);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

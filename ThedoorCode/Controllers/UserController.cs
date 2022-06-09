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

        //public ViewResult Index()
        //{
        //    //Create db context object here 
        //    AdventureWorksDbContext dbContext = new AdventureWorksDbContext();
        //    //Get the value from database and then set it to ViewBag to pass it View
        //    IEnumerable<SelectListItem> items = dbContext.Employees.Select(c => new SelectListItem
        //    {
        //        Value = c.JobTitle,
        //        Text = c.JobTitle

        //    });
        //    ViewBag.JobTitle = items;
        //    return View();
        //}
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.UserModels
                                            orderby m.Age
                                            select m.Name;

            var movies = from m in _context.UserModels
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Gender == movieGenre);
            }

            var movieGenreVM = new UserViewModel
            {
                UserQualification = new SelectList(await genreQuery.Distinct().ToListAsync()),
                UserModel = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }



        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        //public IActionResult Index()
        //{
        //    List<UserModel> userModels;
        //    userModels = _context.UserModels.ToList();
        //    return View(userModels);
        //}

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
            return RedirectToAction("Index");
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }
        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Gender,Age,Qualification,TotalExperience,PhotoUrl")] UserModel userModel)
        {
            if (id != userModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModels
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var userModel = await _context.UserModels.FindAsync(id);

            // Delete image from wwwroot/images
            var imagePath = Path.Combine(_webHost.WebRootPath, "images", userModel.PhotoUrl);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            // delete the record.

            _context.UserModels.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return _context.UserModels.Any(e => e.UserId == id);
        }
    }


}


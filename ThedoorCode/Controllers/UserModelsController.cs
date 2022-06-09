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
    public class UserModelsController : Controller
    {
        private readonly UserDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public UserModelsController(UserDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: UserModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserModels.ToListAsync());
            //List<UserModel> userModels;
            //userModels = _context.UserModels.ToList();
            //return View(userModels);
        }

        // GET: UserModels/Details/5
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

        // GET: UserModels/Create
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Create(UserModel userModel)
        //{
        //    foreach (Experience experience in userModel.Experiences)
        //    {
        //        if (experience.CompanyName == null || experience.CompanyName.Length == 0)
        //            userModel.Experiences.Remove(experience);
        //    }

        //    string uniqueFileName = GetUploadFileName(userModel);
        //    userModel.PhotoUrl = uniqueFileName;

        //    _context.Add(userModel);
        //    _context.SaveChanges();
        //    return RedirectToAction("index");
        //}


        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Gender,Age,Qualification,TotalExperience,PhotoUrl")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHost.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(userModel.ProfilePhoto.FileName);
                string extension = Path.GetExtension(userModel.ProfilePhoto.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRoothPath + "/Image", fileName);
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await userModel.ProfilePhoto.CopyToAsync(fileStream);
                }
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

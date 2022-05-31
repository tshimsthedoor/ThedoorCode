using ECartAppDemo.Models;
using ECartAppDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECartAppDemo.Controllers
{
    public class ItemController : Controller
    {

        private ECartDBEntities objECartDbEntities;

        public ItemController()
        {
            objECartDbEntities = new ECartDBEntities();
        }

        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objECartDbEntities.Categoriesses
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.CategoryId.ToString(),
                                                           Selected = true
                                                       });
            return View(objItemViewModel);
        } 

        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            return Json(data: "HHHH", JsonRequestBehavior.AllowGet);
        }
        
    }
}
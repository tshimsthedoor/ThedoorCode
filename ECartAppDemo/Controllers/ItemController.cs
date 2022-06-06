using ECartAppDemo.Models;
using ECartAppDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string newImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + newImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + newImage;
            objItem.CategoryId = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemNam = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objECartDbEntities.Items.Add(objItem);
            objECartDbEntities.SaveChanges();



            return Json(data: new { Success = true, Message = "Item is added successfully." }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
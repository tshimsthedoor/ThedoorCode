using ECartAppDemo.Models;
using ECartAppDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECartAppDemo.Controllers
{
    public class ShoppingController : Controller
    {
        private  ECartDBEntities objECartDbEntities;
        public ShoppingController()
        {
            objECartDbEntities = new ECartDBEntities();
        }
        // GET: Shopping
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listOfShoppingViewModel = (from objItem in objECartDbEntities.Items
                                                                      join
                                                                      objCategory in objECartDbEntities.Categoriesses
                                                                      on objItem.CategoryId equals objCategory.CategoryId
                                                                      select new ShoppingViewModel()
                                                                      {
                                                                          ImagePath = objItem.ImagePath,
                                                                          ItemName = objItem.ItemNam,
                                                                          Description = objItem.Description,
                                                                          ItemPrice = objItem.ItemPrice,
                                                                          ItemId = objItem.ItemId,
                                                                          ItemCode = objItem.ItemCode,
                                                                          Category = objCategory.CategoryName
                                                                      }
                                                                      ).ToList();
            return View(listOfShoppingViewModel);
        }
    }
}
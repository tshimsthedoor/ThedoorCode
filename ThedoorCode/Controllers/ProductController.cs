using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThedoorCode.Models;

namespace ThedoorCode.Controllers
{
    public class ProductController : Controller
    {
        private IStoreRepository repository;
        public ProductController(IStoreRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index() => View(repository.Products);
    }
}

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

        public int PageSize = 4;
        public ProductController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(int productPage = 1)
                => View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
            .Take(PageSize));
    }
}


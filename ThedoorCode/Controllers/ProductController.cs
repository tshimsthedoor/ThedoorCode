using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThedoorCode.Models;
using ThedoorCode.Models.ViewModels;

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
        public ViewResult Index(string category, int productPage = 1)
                => View(new ProductsListViewModel
                { 
                    Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                    CurrentCategory = category
                });
    }
}


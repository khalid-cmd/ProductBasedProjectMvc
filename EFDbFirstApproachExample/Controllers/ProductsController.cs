using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Filters;
using Company.DataLayer;
using Company.DomainModels;
using Company.ServiceContracts;
using Company.ServiceLayer;

namespace EFDbFirstApproachExample.Controllers
{
    public class ProductsController : Controller
    {
        CompanyDbContext db;
        IProductsService proService;
        public ProductsController(IProductsService pService)
        {
            this.db = new CompanyDbContext();
            this.proService = pService;
        }

        [MyAuthenticationFilter]
        [CustomerAuthorization]
        [MyActionFilter]
        //[MyExceptionFilter]   //moved as global filters
        public ActionResult Index()
        {
            //CompanyDbContext db = new CompanyDbContext();
            List<Product> products = proService.GetProducts();
            return View(products);
        }

        [ChildActionOnly]
        public ActionResult DisplaySingleProduct(Product p)
        {
            return PartialView("MyProduct",p);
        }
    }
}
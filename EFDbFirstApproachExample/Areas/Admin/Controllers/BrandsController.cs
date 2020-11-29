using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Filters;
using Company.DomainModels;
using Company.ServiceContracts;
using Company.ServiceLayer;
using Company.DataLayer;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class BrandsController : Controller
    {
        CompanyDbContext db;
        IBrandService brandService;
        public BrandsController()
        {
            this.db = new CompanyDbContext();
            this.brandService = new BrandsService();
        }
        // GET: admin/Brands/Index
        public ActionResult Index()
        {
            List<Brand> brands = brandService.GetBrands();
            return View(brands);
        }
        public ActionResult Edit(long id)
        {
            Brand existingBrand = brandService.GetBrandByBrandID(id);
            return View(existingBrand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand b)
        {
            Brand existingBrand = db.Brands.Where(t => t.BrandID == b.BrandID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                existingBrand.BrandName = b.BrandName;
                brandService.UpdateBrand(existingBrand);
            }            
            return RedirectToAction("Index", "Brands");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="BrandName")]Brand b)
        {
            if (ModelState.IsValid)
            {
                brandService.InsertBrand(b);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]        
        public ActionResult Delete(long id)
        {
            Brand existingBrand = db.Brands.Where(t => t.BrandID == id).FirstOrDefault();
            return View(existingBrand);
        }
        [HttpPost]
        public ActionResult Delete(long id, Brand b)
        {
            brandService.DeleteBrand(id);
            return RedirectToAction("Index", "Brands");
        }
    }
}
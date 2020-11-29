using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.DataLayer;
using Company.DomainModels;
using EFDbFirstApproachExample.Filters;
using Company.ServiceContracts;
using Company.ServiceLayer;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CategoriesController : Controller
    {
        CompanyDbContext db;
        ICategoryService categoryService;
        public CategoriesController()
        {
            this.db = new CompanyDbContext();
            this.categoryService = new CategoryService();
        }
        // GET: admin/Categories/Index
        public ActionResult Index()
        {
            List<Category> categories = categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            Category existiongCategory = categoryService.GetCategoryByCategoryID(id);
            return View(existiongCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category c)
        {
            Category existiongCategory = categoryService.GetCategoryByCategoryID(c.CategoryID);
            if (ModelState.IsValid)
            {
                existiongCategory.CategoryName = c.CategoryName;
                categoryService.UpdateCategory(existiongCategory);
            }            
            return RedirectToAction("Index", "Categories");
        }
        [HttpGet]
        public ActionResult Delete(long id)
        {
            Category existiongCategory = categoryService.GetCategoryByCategoryID(id);
            return View(existiongCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete (long id, Category c)
        {
            categoryService.DeleteCategory(id);
            return RedirectToAction("Index", "Categories");
        }
        [HttpGet]        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category c)
        {
            categoryService.InsertCategory(c);
            return RedirectToAction("Index");
        }

    }
}
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

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductsController : Controller
    {
        CompanyDbContext db;
        IProductsService prodService;
        public ProductsController(IProductsService pService)
        {
            this.db = new CompanyDbContext();
            this.prodService = pService;
        }

        //GET: admin/products/index
        public ActionResult Index(string search = "", string SortColumn = "ProductName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            ViewBag.search = search;
            List<Product> products = prodService.SearchProducts(search);

            //sorting
            /*Sorting*/
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductID).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductID).ToList();
            }
            else if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            else if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Price).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Price).ToList();
            }
            else if (ViewBag.SortColumn == "DateOfPurchase")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.DateOfPurchase).ToList();
                else
                    products = products.OrderByDescending(temp => temp.DateOfPurchase).ToList();
            }
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.AvailabilityStatus).ToList();
                else
                    products = products.OrderByDescending(temp => temp.AvailabilityStatus).ToList();
            }
            else if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Category.CategoryName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Category.CategoryName).ToList();
            }
            else if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Brand.BrandName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Brand.BrandName).ToList();
            }


            //paging
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;

            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(products);
        }
        public ActionResult Details(long id)
        {
            Product p = prodService.GetProductByProductID(id);
            return View(p);
        }
        public ActionResult Create()
        {
            ViewData["Categories"] = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID, ProductName, Price, DateOfPurchase, AvailabilityStatus, CategoryID, BrandID, Active, Photo")] Product p)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];                            //taking the first file
                    var imgBytes = new Byte[file.ContentLength];            //createing a img bite array based on the img file length
                    file.InputStream.Read(imgBytes, 0, file.ContentLength); //reading all bytes from the files into the img bytes
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);    //converting the bytes into base 64
                    p.Photo = base64String;                                                     // storing base64 into the photo propety of the model class
                }
                prodService.InsertProduct(p);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brands = db.Brands.ToList();
                return View();
            }

        }

        public ActionResult Edit(long id)
        {
            Product ExistingProduct = prodService.GetProductByProductID(id);

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();

            return View(ExistingProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            Product ExistingProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];                            
                    var imgBytes = new Byte[file.ContentLength];            
                    file.InputStream.Read(imgBytes, 0, file.ContentLength); 
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    //p.Photo = base64String;                                                     // storing base64 into the photo propety of the model class
                    ExistingProduct.Photo = base64String;                      // storing base64 into the photo propety of the existing product
                }
                
                ExistingProduct.ProductName = p.ProductName;
                ExistingProduct.Price = p.Price;
                ExistingProduct.DateOfPurchase = p.DateOfPurchase;
                ExistingProduct.AvailabilityStatus = p.AvailabilityStatus;
                ExistingProduct.CategoryID = p.CategoryID;
                ExistingProduct.BrandID = p.BrandID;
                ExistingProduct.Active = p.Active;

                //db.SaveChanges();
                prodService.UpdateProduct(ExistingProduct);
            }            
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Delete(long id)
        {
            Product ExistingProduct = prodService.GetProductByProductID(id);
            return View(ExistingProduct);
        }
        [HttpPost]
        public ActionResult Delete(long id, Product p)
        {
            prodService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
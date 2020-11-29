using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DomainModels;
using Company.RepositoryContracts;
using Company.DataLayer;

namespace Company.RepositoryLayer
{
    public class ProductsRepository : IProductsRepository
    {
        CompanyDbContext db;
        public ProductsRepository()
        {
            this.db = new CompanyDbContext();
        }
        public void DeleteProduct(long ProductID)
        {
            Product p = db.Products.Where(t => t.ProductID == ProductID).FirstOrDefault();
            db.Products.Remove(p);
            db.SaveChanges();
        }

        public Product GetProductByProductID(long ProductID)
        {
            Product p = db.Products.Where(t => t.ProductID == ProductID).FirstOrDefault();
            return p;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = db.Products.ToList();
            return products;
        }

        public void InsertProduct(Product p)
        {
            db.Products.Add(p);
            db.SaveChanges();
        }

        public List<Product> SearchProducts(string ProductName)
        {
            List<Product> products = db.Products.Where(t => t.ProductName.Contains(ProductName)).ToList();
            return products;
        }

        public void UpdateProduct(Product p)
        {
            Product exisingProduct = db.Products.Where(t => t.ProductID == p.ProductID).FirstOrDefault();
            exisingProduct.ProductName = p.ProductName;
            exisingProduct.Price = p.Price;
            exisingProduct.DateOfPurchase = p.DateOfPurchase;
            exisingProduct.AvailabilityStatus = p.AvailabilityStatus;
            exisingProduct.CategoryID = p.CategoryID;
            exisingProduct.BrandID = p.BrandID;
            exisingProduct.Active = p.Active;
            exisingProduct.Photo = p.Photo;
            db.SaveChanges();
        }
    }
}

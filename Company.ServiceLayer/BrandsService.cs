using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DomainModels;
using Company.ServiceContracts;
using Company.DataLayer;


namespace Company.ServiceLayer
{
    public class BrandsService : IBrandService
    {
        CompanyDbContext db;
        public BrandsService()
        {
            this.db = new CompanyDbContext();
        }
        public void DeleteBrand(long id)
        {
            Brand b = db.Brands.Where(t => t.BrandID == id).FirstOrDefault();
            db.Brands.Remove(b);
            db.SaveChanges();
        }

        public Brand GetBrandByBrandID(long id)
        {
            Brand b = db.Brands.Where(t => t.BrandID == id).FirstOrDefault();
            return b;
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = db.Brands.ToList();
            return brands;
        }

        public void InsertBrand(Brand b)
        {
            db.Brands.Add(b);
            db.SaveChanges();
        }

        public void UpdateBrand(Brand b)
        {
            Brand existingBrand = db.Brands.Where(t => t.BrandID == b.BrandID).FirstOrDefault();
            existingBrand.BrandName = b.BrandName;
            db.SaveChanges();
        }
    }
}

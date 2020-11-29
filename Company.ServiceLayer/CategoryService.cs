using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.ServiceContracts;
using Company.DataLayer;
using Company.DomainModels;

namespace Company.ServiceLayer
{
    public class CategoryService : ICategoryService
    {
        CompanyDbContext db;
        public CategoryService()
        {
            this.db = new CompanyDbContext();
        }
        public void DeleteCategory(long id)
        {
            Category c = db.Categories.Where(t => t.CategoryID == id).FirstOrDefault();
            db.Categories.Remove(c);
            db.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public Category GetCategoryByCategoryID(long id)
        {
            Category c = db.Categories.Where(t => t.CategoryID == id).FirstOrDefault();
            return c;
        }

        public void InsertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            Category existCategory = db.Categories.Where(t => t.CategoryID == c.CategoryID).FirstOrDefault();
            existCategory.CategoryName = c.CategoryName;
            db.SaveChanges();
        }
    }
}

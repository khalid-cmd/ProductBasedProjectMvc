using Company.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.ServiceContracts
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryByCategoryID(long id);
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(long id);
    }
}

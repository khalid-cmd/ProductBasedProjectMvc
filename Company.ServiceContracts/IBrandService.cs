using Company.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.ServiceContracts
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandByBrandID(long id);
        void InsertBrand(Brand b);
        void UpdateBrand(Brand b);
        void DeleteBrand(long id);

    }
}

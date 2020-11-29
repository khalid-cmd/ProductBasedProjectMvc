using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DomainModels
{
    public class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [Display(Name = "Brand ID")]
        public long BrandID { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

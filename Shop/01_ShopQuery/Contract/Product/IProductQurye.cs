using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Contract.Product
{
    public interface IProductQurye
    {
        List<ProductQueryModel> GetLatestArrivals();
    }
}

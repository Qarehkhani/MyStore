using System.Collections.Generic;

namespace _01_ShopQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel>  GetProductCategories(); 
    }

}

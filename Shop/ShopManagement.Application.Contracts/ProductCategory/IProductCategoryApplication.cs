using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OpreatinResult Create(CreateProductCategory command);
        OpreatinResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}

using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OpreatinResult Create(CreateProductPicture command);
        OpreatinResult Edit(EditProductPicture command);
        OpreatinResult Remove(long id);
        OpreatinResult Restore(long id);
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Serach(ProductPictureSearchModel searchModel);

    }
}

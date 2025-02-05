using _01_ShopQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel product;
        private readonly IProductQurye _productQuery;

        public ProductModel(IProductQurye productQuery)
        {
            _productQuery=productQuery;
        }

        public void OnGet(string id)
        {
            product=_productQuery.GetDetails(id);
        }
    }
}

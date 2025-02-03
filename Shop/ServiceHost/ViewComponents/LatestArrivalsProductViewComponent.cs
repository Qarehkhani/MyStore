using _01_ShopQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsProductViewComponent : ViewComponent
    {
        private readonly IProductQurye _productQuery;

        public LatestArrivalsProductViewComponent(IProductQurye productQuery)
        {
            _productQuery=productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetLatestArrivals();
            return View(products);
        }
    }
}

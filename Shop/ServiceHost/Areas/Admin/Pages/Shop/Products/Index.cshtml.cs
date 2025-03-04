using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using System.Collections.Generic;

namespace ServiceHost.Areas.Admin.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData] public string message { get; set; }
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;
        public SelectList ProductCategories;

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication=productApplication;
            _productCategoryApplication=productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories=new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products= _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories =_productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            ProductCategories=new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var Product = _productApplication.GetDetails(id);
            Product.Categories=_productCategoryApplication.GetProductCategories();
            return Partial("Edit", Product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

        //public IActionResult OnGetNotInStock(long id)
        //{
        //    var result = _productApplication.NotInStock(id);
        //    if (result.IsSuccedded)
        //        return RedirectToPage("./Index");

        //    message= result.Message;
        //    return RedirectToPage("./Index");


        //}
        //public IActionResult OnGetIsInStock(long id)
        //{
        //    var result = _productApplication.IsStock(id);
        //    if (result.IsSuccedded)
        //        return RedirectToPage("./Index");

        //    message= result.Message;
        //    return RedirectToPage("./Index");

        //}

    }
}

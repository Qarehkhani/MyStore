using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Admin.Pages.Discounts.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        private readonly IProductApplication _ProductApplication;
        private readonly ICustomerDiscountApplication _CustomerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication CustomerDiscountApplication)
        {
            _ProductApplication=productApplication;
            _CustomerDiscountApplication=CustomerDiscountApplication;

        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products=new SelectList(_ProductApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts= _CustomerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products=_ProductApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _CustomerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var CustomerDiscount = _CustomerDiscountApplication.GetDetails(id);
            CustomerDiscount.Products=_ProductApplication.GetProducts();
            return Partial("Edit", CustomerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _CustomerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}

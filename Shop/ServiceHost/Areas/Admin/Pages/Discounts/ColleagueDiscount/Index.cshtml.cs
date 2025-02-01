using DiscountManagement.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Admin.Pages.Discounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string message { get; set; }
        public ColleagueDiscountSearchModel SearchModel;
        public List<ColleagueDiscountViewModel> colleagueDiscounts;
        public SelectList Products;

        private readonly IProductApplication _ProductApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _ProductApplication=productApplication;
            _colleagueDiscountApplication=colleagueDiscountApplication;

        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products=new SelectList(_ProductApplication.GetProducts(), "Id", "Name");
            colleagueDiscounts= _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products=_ProductApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var ColleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
            ColleagueDiscount.Products=_ProductApplication.GetProducts();
            return Partial("Edit", ColleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            message= result.Message;
            return RedirectToPage("./Index");


        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscountApplication.Restore(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            message= result.Message;
            return RedirectToPage("./Index");

        }

    }
}

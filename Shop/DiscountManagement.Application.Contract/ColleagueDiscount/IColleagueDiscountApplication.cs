using _0_Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OpreatinResult Define(DefineColleagueDiscount command);
        OpreatinResult Edit(EditColleagueDiscount command);
        OpreatinResult Remove(long id);
        OpreatinResult Restore(long id);
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);

    }
}

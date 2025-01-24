using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {

        OpreatinResult Create(CreateSlide command);
        OpreatinResult Edit(EditSlide command);
        OpreatinResult Remove(long id);
        OpreatinResult Restore(long id);
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();


    }
}

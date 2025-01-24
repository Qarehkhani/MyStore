using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository=slideRepository;
        }

        public OpreatinResult Create(CreateSlide command)
        {
            var opration = new OpreatinResult();
            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return opration.Succedded();
        }

        public OpreatinResult Edit(EditSlide command)
        {
            var opration = new OpreatinResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide==null)
                opration.Faild(ApplicationMessages.RecordNotFound);

            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.SaveChanges();
            return opration.Succedded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.getDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.getLists();
        }

        public OpreatinResult Remove(long id)
        {
            var opration = new OpreatinResult();
            var slide = _slideRepository.Get(id);
            if (slide==null)
                opration.Faild(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _slideRepository.SaveChanges();
            return opration.Succedded();
        }

        public OpreatinResult Restore(long id)
        {
            var opration = new OpreatinResult();
            var slide = _slideRepository.Get(id);
            if (slide==null)
                opration.Faild(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChanges();
            return opration.Succedded();
        }
    }
}

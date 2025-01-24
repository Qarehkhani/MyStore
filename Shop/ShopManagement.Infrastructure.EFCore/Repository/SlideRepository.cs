using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;
        public SlideRepository(ShopContext context) : base(context)
        {
            _context=context;
        }

        public EditSlide getDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = id,
                BtnText = x.BtnText,
                Heading=x.Heading,
                Picture = x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Text=x.Text,
                Title=x.Title
            }).FirstOrDefault(x => x.Id==id); 
        }

        public List<SlideViewModel> getLists()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id, 
                Title = x.Title,
                Heading = x.Heading,
                Picture=x.Picture,
                IsRemoved=x.IsRemoved,
                CreationDate=x.CreationDate.ToString()
            }).OrderBy(x => x.Id).ToList();
        }
    }
}

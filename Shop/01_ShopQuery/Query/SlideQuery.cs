using _01_ShopQuery.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_ShopQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext=shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(x=>x.IsRemoved==false).Select(x=> new SlideQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.Picture,
                PictureTitle = x.Picture,
                Heading=x.Heading,  
                Title=x.Title,
                Text=x.Text,
                Link=x.Link,
                BtnText = x.BtnText
            }).ToList();
        }
    }
}

using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context=context;
            _shopContext=shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                Reason = x.Reason
            }).FirstOrDefault(x => x.Id==id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id=x.Id,
                Reason = x.Reason,
                CreationDate=x.CreationDate.ToFarsi(),
                DiscountRate=x.DiscountRate,
                ProductId=x.ProductId,
                IsRemoved=x.IsRemoved
            });

            if (searchModel.ProductId>0)
                query=query.Where(x => x.ProductId==searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.Reason))
            {
                query=query.Where(x => x.Reason.Contains(searchModel.Reason));
            }


            var discount = query.OrderByDescending(x => x.Id).ToList();
            discount.ForEach(discount =>
            discount.Product= products.FirstOrDefault(x => x.Id==discount.ProductId)?.Name);


            return discount;

        }



    }
}

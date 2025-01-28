using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository=colleagueDiscountRepository;
        }

        public OpreatinResult Define(DefineColleagueDiscount command)
        {
            var opreatin = new OpreatinResult();
            if (_colleagueDiscountRepository.Exists(x => x.ProductId==command.ProductId && x.DiscountRate==command.DiscountRate))
                return opreatin.Faild(ApplicationMessages.DuplicatedRecord);
            var discount=new ColleagueDiscount(command.ProductId,command.DiscountRate,command.Reason);
            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.SaveChanges();
            return opreatin.Succedded();
        }

        public OpreatinResult Edit(EditColleagueDiscount command)
        {
            var opreatin = new OpreatinResult();
            var discoint=_colleagueDiscountRepository.Get(command.Id);
            if (discoint==null)
                return opreatin.Faild(ApplicationMessages.RecordNotFound);

            if (_colleagueDiscountRepository.Exists(x => x.ProductId==command.ProductId && x.DiscountRate==command.DiscountRate && x.Id!=command.Id))
                return opreatin.Faild(ApplicationMessages.DuplicatedRecord);

            discoint.Edit(command.ProductId, command.DiscountRate,command.Reason);
            _colleagueDiscountRepository.SaveChanges();
            return opreatin.Succedded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public OpreatinResult Remove(long id)
        {
            var opreatin = new OpreatinResult();
            var discoint = _colleagueDiscountRepository.Get(id);
            if (discoint==null)
                return opreatin.Faild(ApplicationMessages.RecordNotFound);

            discoint.Remove();
            return opreatin.Succedded();
        }

        public OpreatinResult Restore(long id)
        {
            var opreatin = new OpreatinResult();
            var discoint = _colleagueDiscountRepository.Get(id);
            if (discoint==null)
                return opreatin.Faild(ApplicationMessages.RecordNotFound);

            discoint.Restore();
            return opreatin.Succedded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}

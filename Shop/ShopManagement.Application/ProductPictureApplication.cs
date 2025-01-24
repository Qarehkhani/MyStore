using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository=productPictureRepository;
        }

        public OpreatinResult Create(CreateProductPicture command)
        {
            var opreation = new OpreatinResult();
            if (_productPictureRepository.Exists(x => x.Picture==command.Picture && x.ProductId==command.ProductId))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);
            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureAlt);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return opreation.Succedded();
        }

        public OpreatinResult Edit(EditProductPicture command)
        {
            var opreation = new OpreatinResult();
            var productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture == null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);
            if (_productPictureRepository.Exists(x => x.Picture==command.Picture && x.ProductId==command.ProductId && x.Id !=command.Id))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureAlt);
            _productPictureRepository.SaveChanges();
            return opreation.Succedded();

                
        }


        public EditProductPicture GetDetails(long id)
        {
           return _productPictureRepository.GetDetails(id);
        }

        public OpreatinResult Remove(long id)
        {
            var opreation = new OpreatinResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return opreation.Succedded();
        }

        public OpreatinResult Restore(long id)
        {
            var opreation = new OpreatinResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return opreation.Succedded();
        }

        public List<ProductPictureViewModel> Serach(ProductPictureSearchModel searchModel)
        {
          return  _productPictureRepository.Search(searchModel);
        }
    }
}

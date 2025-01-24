using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OpreatinResult Create(CreateProductCategory command)
        {
            var opreation = new OpreatinResult();
            if (_productCategoryRepository.Exists(x => x.Name==command.Name))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var productcategory = new ProductCategory(command.Name, command.Description,
                                                    command.Picture, command.PictureAlt, command.PictureAlt,
                                                    command.KeyWords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productcategory);
            _productCategoryRepository.SaveChanges();
            return opreation.Succedded();
        }

        public OpreatinResult Edit(EditProductCategory command)
        {
            var opreation = new OpreatinResult();
            var productcategory = _productCategoryRepository.Get(command.Id);

            if (productcategory==null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name==command.Name && x.Id!=command.Id))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description,
                                 command.Picture, command.PictureAlt, command.PictureAlt,
                                 command.KeyWords, command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();
            return opreation.Succedded();

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }


    }
}

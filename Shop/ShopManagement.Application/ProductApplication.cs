using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository=productRepository;
            _fileUploader=fileUploader;
            _productCategoryRepository=productCategoryRepository;
        }

        public OpreatinResult Create(CreateProduct command)
        {
            var opreation = new OpreatinResult();
            if (_productRepository.Exists(x => x.Name==command.Name))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);
            var categorySlog = _productCategoryRepository.GetSlogById(command.CategoryId);
            var picturePath = $"{categorySlog}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            var product = new Product(command.Name, command.Code,
                                    command.ShortDescription, command.Description,
                                    pictureName, command.PictureAlt, command.PictureTitle,
                                    slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return opreation.Succedded();

        }

        public OpreatinResult Edit(EditProduct command)
        {
            var opreation = new OpreatinResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product==null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name==command.Name && x.Id !=command.Id))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);

            var picturePath = $"{product.Category.Slug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            product.Edit(command.Name, command.Code,
                                    command.ShortDescription, command.Description,
                                    pictureName, command.PictureAlt, command.PictureTitle,
                                    slug, command.Keywords, command.MetaDescription, command.CategoryId);

            _productRepository.SaveChanges();
            return opreation.Succedded();

        }

        public EditProduct GetDetails(long Id)
        {
            return _productRepository.GetDetails(Id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        //public OpreatinResult IsStock(long Id)
        //{
        //    var opreation = new OpreatinResult();
        //    var product = _productRepository.Get(Id);
        //    if (product==null)
        //        return opreation.Faild(ApplicationMessages.RecordNotFound);

        //    product.InStock();
        //    _productRepository.SaveChanges();
        //    return opreation.Succedded();
        //}

        //public OpreatinResult NotInStock(long Id)
        //{
        //    var opreation = new OpreatinResult();
        //    var product = _productRepository.Get(Id);
        //    if (product==null)
        //        return opreation.Faild(ApplicationMessages.RecordNotFound);

        //    product.NotInStock();
        //    _productRepository.SaveChanges();
        //    return opreation.Succedded();
        //}

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}

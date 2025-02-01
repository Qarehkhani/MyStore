using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }

        public OpreatinResult Create(CreateProduct command)
        {
            var opreation  = new OpreatinResult();
            if (_productRepository.Exists(x=>x.Name==command.Name))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);

            var product = new Product(command.Name, command.Code,
                                    command.ShortDescription, command.Description,
                                    command.Picture, command.PictureAlt, command.PictureTitle,
                                    slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return opreation.Succedded();

        }

        public OpreatinResult Edit(EditProduct command)
        {
            var opreation = new OpreatinResult();
            var product = _productRepository.Get(command.Id);
            if (product==null)
                return opreation.Faild(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name==command.Name && x.Id !=command.Id))
                return opreation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);
            product.Edit(command.Name, command.Code,
                                    command.ShortDescription, command.Description,
                                    command.Picture, command.PictureAlt, command.PictureTitle,
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

﻿using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        //public double UnitPrice { get; set; }
        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxFileSize(3*1024*1024, ErrorMessage = ValidationMessages.MaxFileSixe)]
        [FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png", ".gif", ".tiff" }, ErrorMessage = ValidationMessages.InvaliFileFormat)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }
        
        [Range(1 , 100000, ErrorMessage=ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; }

    }
}

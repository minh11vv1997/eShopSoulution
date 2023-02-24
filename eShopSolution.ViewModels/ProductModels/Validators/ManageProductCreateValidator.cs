using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.ProductModels.Validators
{
    public class ManageProductCreateValidator : AbstractValidator<ProductCreateRequest>
    {
        public ManageProductCreateValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Number of trainings can't be empty");
            RuleFor(x => x.OriginalPrice).NotEmpty().WithMessage("Number of trainings can't be empty");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Number of trainings can't be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name of trainings can't be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description of trainings can't be empty");
            RuleFor(x => x.Details).NotEmpty().WithMessage("Details of trainings can't be empty");
            RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("SeoAlias of trainings can't be empty");
            RuleFor(x => x.LanguageId).NotEmpty().WithMessage("LanguageId of trainings can't be empty");
            //RuleFor(x => x.ThumbnailImage.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            //    .WithMessage("File type is larger than allowed");
            //RuleFor(x => x.ThumbnailImage.Length).NotNull().LessThanOrEqualTo(100)
            //   .WithMessage("File size is larger than allowed");
        }
    }
}
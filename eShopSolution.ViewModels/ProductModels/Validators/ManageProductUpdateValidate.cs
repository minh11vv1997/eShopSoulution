using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.ProductModels.Validators
{
    public class ManageProductUpdateValidate : AbstractValidator<ProductUpdateRequest>
    {
        public ManageProductUpdateValidate()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage("Name of trainings can't be empty");
            //RuleFor(x => x.Description).NotEmpty().WithMessage("Description of trainings can't be empty");
            //RuleFor(x => x.Details).NotEmpty().WithMessage("Details of trainings can't be empty");
            //RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("SeoAlias of trainings can't be empty");
        }
    }
}
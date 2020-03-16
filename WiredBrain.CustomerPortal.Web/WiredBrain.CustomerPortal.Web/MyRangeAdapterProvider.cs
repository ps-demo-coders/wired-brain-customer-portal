using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace WiredBrain.CustomerPortal.Web
{
    //public class MyRangeAdapterProvider : IValidationAttributeAdapterProvider
    //{
    //    private readonly IValidationAttributeAdapterProvider baseProvider =
    //        new ValidationAttributeAdapterProvider();

    //    public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
    //        IStringLocalizer stringLocalizer)
    //    {
    //        if (attribute is MyRangeAttribute classicMovieAttribute)
    //        {
    //            return new RangeAttributeAdapter(classicMovieAttribute, stringLocalizer);
    //        }

    //        return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
    //    }
    //}
}

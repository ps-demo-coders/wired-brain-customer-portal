using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace WiredBrain.CustomerPortal.Web.Validations
{
    public class CustomAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer)
        {
            if (attribute is ZipAttribute zipAttribute)
            {
                return new ZipAttributeAdapter(zipAttribute, stringLocalizer);
            }

            return baseProvider
                .GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}

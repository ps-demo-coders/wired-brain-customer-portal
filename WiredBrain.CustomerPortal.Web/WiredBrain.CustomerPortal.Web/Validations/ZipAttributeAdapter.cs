using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace WiredBrain.CustomerPortal.Web.Validations
{
    public class ZipAttributeAdapter : AttributeAdapterBase<ZipAttribute>
    {
        private readonly ZipAttribute attribute;

        public ZipAttributeAdapter(
            ZipAttribute attribute,
            IStringLocalizer localizer)
            : base(attribute, localizer)
        {
            this.attribute = attribute;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-regex", GetErrorMessage(context));
            context.Attributes.Add("data-val-regex-pattern", attribute.Pattern);
        }

        public override string GetErrorMessage(
            ModelValidationContextBase validationContext)
        { 
            return GetErrorMessage(validationContext.ModelMetadata, 
                validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

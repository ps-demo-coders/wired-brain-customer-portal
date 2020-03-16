using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace WiredBrain.CustomerPortal.Web
{
    public class MyRangeAttributeAdapter : AttributeAdapterBase<MyRangeAttribute>
    {
        public MyRangeAttributeAdapter(MyRangeAttribute attribute, IStringLocalizer stringLocalizer)
            :base(attribute, stringLocalizer)
        {}

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-range", GetErrorMessage(context));
            context.Attributes.Add("data-val-range-min", $"{Attribute.Minimum}");
            context.Attributes.Add("data-val-range-max", $"{Attribute.Maximum}");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            Attribute.GetErrorMessage();
    }
}

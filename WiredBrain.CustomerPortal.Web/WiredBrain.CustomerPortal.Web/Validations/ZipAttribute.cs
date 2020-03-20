using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WiredBrain.CustomerPortal.Web.Resources;

namespace WiredBrain.CustomerPortal.Web.Validations
{
    public class ZipAttribute : RegularExpressionAttribute
    {
        public ZipAttribute() : base(@"^\d{5}$")
        {
            ErrorMessageResourceType = typeof(ValidationMessages);
            ErrorMessageResourceName = "Zip";
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WiredBrain.CustomerPortal.Web
{
    public class MyRangeAttribute: RangeAttribute//, IClientModelValidator
    {
        public static int _min = 0;
        public static int _max = 10;    

        public MyRangeAttribute(): base(_min, _max)
        {
            ErrorMessage = GetErrorMessage();
        }

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    context.Attributes.Add("data-val", "true");
        //    context.Attributes.Add("data-val-range", GetErrorMessage());
        //    context.Attributes.Add("data-val-range-min", $"{_min}");
        //    context.Attributes.Add("data-val-range-max", $"{_max}");
        //}

        public string GetErrorMessage() =>
            $"Must be between {_min} and {_max}.";
    }
}

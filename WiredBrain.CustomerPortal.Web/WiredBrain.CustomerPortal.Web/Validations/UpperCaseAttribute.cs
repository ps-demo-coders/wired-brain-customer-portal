using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WiredBrain.CustomerPortal.Web.Validations
{
    public class UpperCaseAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int minLength;

        public UpperCaseAttribute(int minLength)
        {
            ErrorMessage = "Must be upper case";
            this.minLength = minLength;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-uppercase", ErrorMessage);
            context.Attributes.Add("data-val-uppercase-minlength", $"{minLength}");
        }

        public override bool IsValid(object value)
        {
            if (!(value is string s))
                throw new ArgumentException("Not a string!");
            if (s.Length < minLength)
                return true;
            return s == s.ToUpper();
        }
    }
}

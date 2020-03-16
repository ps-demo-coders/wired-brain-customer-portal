using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WiredBrain.CustomerPortal.Web
{
    public class UpperCaseAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int minLength;

        public UpperCaseAttribute(int minLength)
        {
            this.minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            if (!(value is string s))
                throw new ArgumentException("Not a string!");
            if (s.Length < minLength)
                return true;
            return s == s.ToUpper();
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-uppercase", GetErrorMessage());
            context.Attributes.Add("data-val-uppercase-minlength", $"{minLength}");
        }

        private string GetErrorMessage()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
                return "Must be upper case!";
            return ErrorMessage;
        }
    }
}

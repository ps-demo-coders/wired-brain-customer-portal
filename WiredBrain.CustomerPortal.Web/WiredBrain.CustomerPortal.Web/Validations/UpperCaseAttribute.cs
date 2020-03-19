using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WiredBrain.CustomerPortal.Web.Validations
{
    public class UpperCaseAttribute: ValidationAttribute
    {
        public UpperCaseAttribute()
        {
            ErrorMessage = "Must be upper case";
        }

        public override bool IsValid(object value)
        {
            if (!(value is string s))
                throw new ArgumentException("Not a string!");
            return s == s.ToUpper();
        }
    }
}

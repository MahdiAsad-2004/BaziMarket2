using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace BaziMarket2.Validators
{
    public class MaxFileSize : ValidationAttribute
    {
        public int _maxFileSize;
        
        public string ErrorMessage;
        
        public void MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;
            if (file != null)
            {
                if (file.ContentLength > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            if (!ErrorMessage.IsEmpty())
            {
                return ErrorMessage;
            }
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }
}
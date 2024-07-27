using E_CommercePraticeModel.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace E_CommercePraticeModel.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";

        public InvoicePriceValidatorAttribute()
        {
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? OtherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (OtherProperty != null)
                {
                    List<Product> Products = (List<Product>)OtherProperty.GetValue(validationContext.ObjectInstance);
                    double totalPrice = 0;
                    foreach (Product product in Products)
                    {
                        totalPrice += product.Price * product.Quantity;
                    }
                    double actualPrice = (double)value;
                    if (totalPrice > 0)
                    {
                        //if the value of "InvoicePrice" property is not equal to the total cost of all products in the order
                        if (totalPrice != actualPrice)
                        {
                            //return model error
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        //return model error is no products found
                        return new ValidationResult("No products found to validate invoice price", new string[] { nameof(validationContext.MemberName) });
                    }

                    //No validation error
                    return ValidationResult.Success;
                }
                return null;
            }
            else
                return null;


        }
    }
}

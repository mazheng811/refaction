using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefactorMeNetCore.Models
{
    public partial class Product : IValidatableObject
    {
        public Guid Id
        {
            get; set;
        }
        [Required]
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        [Required]
        public decimal Price
        {
            get; set;
        }
        [Required]
        public decimal DeliveryPrice
        {
            get; set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0)
            {
                yield return new ValidationResult("Price must be greater than 0.", new[] { "Price" });
            }

            if (DeliveryPrice < 0)
            {
                yield return new ValidationResult("Delivery Price can not be smaller 0.", new[] { "DeliveryPrice" });
            }
        }
    }
}

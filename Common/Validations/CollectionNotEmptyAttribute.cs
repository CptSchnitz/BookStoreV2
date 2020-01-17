using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Common.Validations
{
    // Checks that any object that implements ICollection is not empty
    public class CollectionNotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection == null)
                return false;
            return collection.Count > 0;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return IsValid(value) ? ValidationResult.Success : new ValidationResult("Collection Empty");
        }
    }
}

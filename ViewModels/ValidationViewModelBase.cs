using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ViewModels
{
    // Base view model that enables validation on properties with validation attributes
    public abstract class ValidationViewModelBase : ViewModelBase, IDataErrorInfo
    {
        // list of all the properties that got validators
        private readonly List<string> PropertiesWithValidators;

        public ValidationViewModelBase()
        {
            PropertiesWithValidators = this.GetType()
                .GetProperties()
                .Where(PropertyGotValidations)
                .Select(prop => prop.Name).ToList();
        }

        //checks if a given property got a validation attribute
        private bool PropertyGotValidations(PropertyInfo property)
        {
            return property.GetCustomAttributes
            (typeof(ValidationAttribute), true).Length > 0;
        }

        // the property to bind to check if model is valid
        public bool IsModelValid => CheckIfModelValid();

        //the method loops over all the properties and checks if one fails validation.
        private bool CheckIfModelValid()
        {
            var results = new List<ValidationResult>(1);
            foreach (var prop in PropertiesWithValidators)
            {
                var value = GetValue(prop);
                if (!Validator.TryValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = prop
                }, results))
                {
                    return false;
                }
            }
            return true;
        }
        string IDataErrorInfo.Error
        {
            get
            {
                throw new NotSupportedException("IDataErrorInfo.Error is not supported, use IDataErrorInfo.this[propertyName] instead.");
            }
        }

        // Lets you test if a specific property is valid or not
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                // checks if the property exists
                if (string.IsNullOrEmpty(propertyName))
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }
                string error = string.Empty;
                var value = GetValue(propertyName);
                // list to keep the error.
                var results = new List<ValidationResult>(1);

                //Checks the validation attributes
                var result = Validator.TryValidateProperty(
                    value,
                    new ValidationContext(this, null, null)
                    {
                        MemberName = propertyName
                    },
                    results);

                // sets the error message if validation failed.
                if (!result)
                {
                    var validationResult = results.First();
                    error = validationResult.ErrorMessage;
                }

                RaisePropertyChanged(nameof(IsModelValid));
                return error;
            }
        }

        //returns the value of a property
        private object GetValue(string propertyName)
        {
            PropertyInfo propInfo = GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
        }
    }
}

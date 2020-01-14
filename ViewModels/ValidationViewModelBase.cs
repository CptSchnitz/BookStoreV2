using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ViewModels
{
    public abstract class ValidationViewModelBase : ViewModelBase, IDataErrorInfo
    {

        private readonly List<string> PropertiesWithValidators;

        public ValidationViewModelBase()
        {
            PropertiesWithValidators = this.GetType().GetProperties().Where(PropertyGotValidations).Select(prop => prop.Name).ToList();
        }

        private bool PropertyGotValidations(PropertyInfo property)
        {
            return property.GetCustomAttributes
            (typeof(ValidationAttribute), true).Length > 0;
        }

        public bool IsModelValid => CheckIfModelValid();

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
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }
                string error = string.Empty;
                var value = GetValue(propertyName);
                var results = new List<ValidationResult>(1);
                var result = Validator.TryValidateProperty(
                    value,
                    new ValidationContext(this, null, null)
                    {
                        MemberName = propertyName
                    },
                    results);
                if (!result)
                {
                    var validationResult = results.First();
                    error = validationResult.ErrorMessage;
                }
                RaisePropertyChanged(nameof(IsModelValid));
                return error;
            }
        }
        private object GetValue(string propertyName)
        {
            PropertyInfo propInfo = GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
        }
    }
}

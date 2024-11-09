using EmailValidation;
using IbanNet;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Web.Data
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(FirstName), nameof(LastName), nameof(DateOfBirth), IsUnique = true)]

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [ValidatePhoneNumber]
        public string PhoneNumber { get; set; }

        [Required]
        [ValidateEmailAddress]
        public string Email { get; set; }

        [Required]
        [ValidateBankAccountNumber]
        public string BankAccountNumber { get; set; }
    }

    public class ValidatePhoneNumber : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberUtil.Parse(value.ToString(), null);
                var regionCode = phoneNumberUtil.GetRegionCodeForNumber(phoneNumber);
                if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult("the Phone Number is NOT Valid!");
                }
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            catch
            {
                return new System.ComponentModel.DataAnnotations.ValidationResult("the Phone Number is NOT Valid!");
            }

        }
    }

    public class ValidateBankAccountNumber : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                IIbanValidator validator = new IbanValidator();
                IbanNet.ValidationResult validationResult = validator.Validate(value.ToString());
                if (!validationResult.IsValid)
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult("the Bank Account Number is NOT Valid!");
                }
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            catch
            {
                return new System.ComponentModel.DataAnnotations.ValidationResult("the Bank Account Number is NOT Valid!");
            }
        }
    }

    public class ValidateEmailAddress : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValidEmailAddress = EmailValidator.Validate(value.ToString().Trim());

                if (!isValidEmailAddress)
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult("the Email Address is NOT Valid!");
                }
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
            catch
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }
        }
    }
}

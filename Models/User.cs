using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JsonApiDotNetCore.Models;

namespace LibraryApi.Models
{
    public class User : Identifiable
    {
        [Attr("email"), UniqueEmail, Required(AllowEmptyStrings = false, ErrorMessage="email is required")] public string Email { get; set; }
        [Attr("username"), UniqueUserName, Required(AllowEmptyStrings = false, ErrorMessage="username is required")] public string Username { get; set; }
        [Attr("password"), NotMapped, Required(AllowEmptyStrings = false, ErrorMessage="password is required"), Compare("PasswordConfirmation")] public string Password { get; set; }
        [Attr("password-confirmation"), NotMapped, Required(AllowEmptyStrings = false, ErrorMessage="password confirmation is required")] public string PasswordConfirmation { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UniqueUserName: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (AppDbContext) validationContext.GetService(typeof(AppDbContext));

            if (context.Users.Where(u => u.Username.Equals((string) value, StringComparison.OrdinalIgnoreCase)).Count() >= 1)
            {
                return new ValidationResult("username is already taken", new [] { "Username" } );
            }

            return ValidationResult.Success;
        }
    }

    public class UniqueEmail: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (AppDbContext) validationContext.GetService(typeof(AppDbContext));

            if (context.Users.Where(u => u.Email.Equals((string) value, StringComparison.OrdinalIgnoreCase)).Count() >= 1)
            {
                return new ValidationResult("email is already taken", new [] { "Email" } );
            }

            return ValidationResult.Success;
        }
    }
}
using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class UserValidatorTests
    {
        private readonly UserValidator _registerUserValidator;

        public UserValidatorTests()
        {
            _registerUserValidator = new UserValidator();
        }

        [Fact]
        public void UserValidator_ValidUser_ReturnsTrue()
        {
            UserViewModel user = new UserViewModel
            {
                Username = "Gigel34",
                Password = "ertgysegtAERFwa4crWc4arw4"
            };

            var result = _registerUserValidator.Validate(user);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void UserValidator_UsernameEmpty_ReturnsFalse()
        {
            UserViewModel user = new UserViewModel
            {
                Username = "   ",
                Password = "ertgysegtAERFwa4crWc4arw4"
            };

            var result = _registerUserValidator.Validate(user);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserValidator_PasswordTooShort_ReturnsFalse()
        {
            UserViewModel user = new UserViewModel
            {
                Username = "Gigel34",
                Password = "a12"
            };

            var result = _registerUserValidator.Validate(user);

            Assert.False(result.IsValid);
        }
    }
}

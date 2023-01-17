using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator _userRoleValidator;

        public EmployeeValidatorTests()
        {
            _userRoleValidator = new EmployeeValidator();
        }

        [Fact]
        public void EmployeeValidator_ValidEmployee_ReturnsTrue()
        {
            EmployeeViewModel user = new EmployeeViewModel
            {
                Username = "Gigel",
                Role = 1
            };

            var result = _userRoleValidator.Validate(user);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void EmployeeValidator_UsernameEmpty_ReturnsFalse()
        {
            EmployeeViewModel user = new EmployeeViewModel
            {
                Username = "",
                Role = 1
            };

            var result = _userRoleValidator.Validate(user);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeValidator_RoleInvalid_ReturnsFalse()
        {
            EmployeeViewModel user = new EmployeeViewModel
            {
                Username = "Gigel",
                Role = 99
            };

            var result = _userRoleValidator.Validate(user);

            Assert.False(result.IsValid);
        }
    }
}

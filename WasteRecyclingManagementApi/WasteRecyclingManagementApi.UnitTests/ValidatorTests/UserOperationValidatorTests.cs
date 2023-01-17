using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class UserOperationValidatorTests
    {
        private readonly UserOperationValidator _userOperationValidator;

        public UserOperationValidatorTests()
        {
            _userOperationValidator = new UserOperationValidator();
        }

        [Fact]
        public void UserOperationValidator_ValidUserOperation_ReturnsTrue()
        {
            UserOperationViewModel operation = new UserOperationViewModel
            {
                ContainerWasteType = "glass",
                RecyclingPointName = "Recycling Point test",
                WasteAmount = 10
            };

            var result = _userOperationValidator.Validate(operation);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void UserOperationValidator_ContainerWasteTypeEmpty_ReturnsFalse()
        {
            UserOperationViewModel operation = new UserOperationViewModel
            {
                ContainerWasteType = "",
                RecyclingPointName = "Recycling Point test",
                WasteAmount = 10
            };

            var result = _userOperationValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserOperationValidator_RecyclingPointNameEmpty_ReturnsFalse()
        {
            UserOperationViewModel operation = new UserOperationViewModel
            {
                ContainerWasteType = "glass",
                RecyclingPointName = " ",
                WasteAmount = 10
            };

            var result = _userOperationValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void UserOperationValidator_WasteAmountTooBig_ReturnsFalse()
        {
            UserOperationViewModel operation = new UserOperationViewModel
            {
                ContainerWasteType = "glass",
                RecyclingPointName = "Recycling Point test",
                WasteAmount = 100000
            };

            var result = _userOperationValidator.Validate(operation);

            Assert.False(result.IsValid);
        }
    }
}

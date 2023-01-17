using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class EmployeeOperationValidatorTests
    {
        private readonly EmployeeOperationValidator _cleanContainerValidator;

        public EmployeeOperationValidatorTests()
        {
            _cleanContainerValidator = new EmployeeOperationValidator();
        }

        [Fact]
        public void EmployeeOperationValidator_ValidEmployeeOperation_ReturnsTrue()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "metals",
                RecyclingPointName = "Recycling Point test"
            };

            var result = _cleanContainerValidator.Validate(operation);
   
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_ContainerWasteTypeTooLong_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "metalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetalsmetals",
                RecyclingPointName = "Recycling Point test"
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_ContainerWasteTypeTooShort_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "a",
                RecyclingPointName = "Recycling Point test"
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_ContainerWasteTypeEmpty_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = " ",
                RecyclingPointName = "Recycling Point test"
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_RecyclingPointNameTooLong_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "metals",
                RecyclingPointName = "Recycling Point test aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_RecyclingPointNameTooShort_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "metals",
                RecyclingPointName = "aa"
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmployeeOperationValidator_RecyclingPointNameEmpty_ReturnsFalse()
        {
            EmployeeOperationViewModel operation = new EmployeeOperationViewModel
            {
                ContainerWasteType = "metals",
                RecyclingPointName = ""
            };

            var result = _cleanContainerValidator.Validate(operation);

            Assert.False(result.IsValid);
        }
    }
}

using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class ContainerValidatorTests
    {
        private readonly ContainerValidator _containerValidator;

        public ContainerValidatorTests()
        {
            _containerValidator = new Validators.ContainerValidator();
        }

        [Fact]
        public void ContainerValidator_ValidContainer_ReturnsTrue()
        {
            var container = new ContainerViewModel
            {
                WasteType = "wood",
                MeasureUnit = "kg",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_WasteTypeTooLong_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "woodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwoodwood",
                MeasureUnit = "kg",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_WasteTypeTooShort_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "a",
                MeasureUnit = "kg",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_WasteTypeTooEmpty_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "  ",
                MeasureUnit = "kg",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_MeasureUnitTooLong_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "paper",
                MeasureUnit = "kilograms and meters and aaaaaaaaaaaaaaaa",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_MeasureUnitEmpty_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "paper",
                MeasureUnit = " ",
                TotalCapacity = 50
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_TotalCapacityTooSmall_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "paper",
                MeasureUnit = "kg",
                TotalCapacity = (decimal)0.01
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ContainerValidator_TotalCapacityTooLarge_ReturnsFalse()
        {
            var container = new ContainerViewModel
            {
                WasteType = "paper",
                MeasureUnit = "kg",
                TotalCapacity = 999999
            };

            var result = _containerValidator.Validate(container);

            Assert.False(result.IsValid);
        }

    }
}
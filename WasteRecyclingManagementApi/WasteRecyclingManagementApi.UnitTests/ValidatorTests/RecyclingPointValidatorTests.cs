using WasteRecyclingManagementApi.Validators;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ValidatorTests
{
    public class RecyclingPointValidatorTests
    {
        private readonly RecyclingPointValidator recyclingPointValidator;

        public RecyclingPointValidatorTests()
        {
            recyclingPointValidator = new RecyclingPointValidator();
        }

        [Fact]
        public void RecyclingPointValidator_ValidRecyclingPoint_ReturnsTrue()
        {
            RecyclingPointViewModel recyclingPoint = new RecyclingPointViewModel
            {
                Name = "Recycling Point test",
                Latitude = 60.32456765,
                Longitude = 45.24565442
            };

            var result = recyclingPointValidator.Validate(recyclingPoint);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void RecyclingPointValidator_NameEmpty_ReturnsFalse()
        {
            RecyclingPointViewModel recyclingPoint = new RecyclingPointViewModel
            {
                Name = "",
                Latitude = 60.32456765,
                Longitude = 45.24565442
            };

            var result = recyclingPointValidator.Validate(recyclingPoint);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void RecyclingPointValidator_LatitudeTooBig_ReturnsFalse()
        {
            RecyclingPointViewModel recyclingPoint = new RecyclingPointViewModel
            {
                Name = "",
                Latitude = 34560.32456765,
                Longitude = 45.24565442
            };

            var result = recyclingPointValidator.Validate(recyclingPoint);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void RecyclingPointValidator_LongitudeTooSmall_ReturnsFalse()
        {
            RecyclingPointViewModel recyclingPoint = new RecyclingPointViewModel
            {
                Name = "",
                Latitude = 60.32456765,
                Longitude = -24545.24565442
            };

            var result = recyclingPointValidator.Validate(recyclingPoint);

            Assert.False(result.IsValid);
        }
    }
}

using KataBabysitter.Classes;
using KataBabysitter.Services;
using System;
using Xunit;

namespace KataBabysitter.Tests
{
    public class PaymentCalculationServiceTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(4, 3, 0)]
        [InlineData(6, 0, 2)]
        [InlineData(0, 1, 2)]
        [InlineData(0, 5, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 3)]
        [InlineData(4, 0, 0)]
        public void CalculateIncome_CalculatesCorrectIncome(int awakeHours, int asleepHours, int postMidnightHours)
        {
            // Arrange
            var service = new PaymentCalculationService();

            // Act
            var expectedCalculatedIncome = (awakeHours * 12) + (asleepHours * 8) + (postMidnightHours * 16);
            var calculatedIncome = service.CalculateIncome(awakeHours, asleepHours, postMidnightHours);

            // Assert
            Assert.Equal(expectedCalculatedIncome, calculatedIncome);
        }

        [Theory]
        [InlineData("2005-05-05 5:00 PM", "2005-05-05 7:59 PM")]
        [InlineData("2005-05-05 5:00 PM", "2005-05-05 8:01 PM")]
        [InlineData("2005-05-05 6:31 PM", "2005-05-05 9:30 PM")]
        [InlineData("2005-05-05 6:31 PM", "2005-05-05 9:31 PM")]
        public void CalculateAwakeHours_FractionalHours_OnlyCountsFullHours(string startTime, string bedTime)
        {
            // Arrange
            var shiftInformation = new ShiftInformation();
            shiftInformation.StartTime = DateTime.Parse(startTime);
            shiftInformation.BedTime = DateTime.Parse(bedTime);
            var service = new PaymentCalculationService();

            // Act 
            var actualAwakeHours = service.CalculateAwakeHours(shiftInformation);
            var expectedAwakeHours = (int)(shiftInformation.BedTime - shiftInformation.StartTime).TotalHours;
            if(expectedAwakeHours < 0)
            {
                expectedAwakeHours = 0;
            }

            Assert.Equal(expectedAwakeHours, actualAwakeHours);
        }

        [Theory]
        [InlineData("2005-05-05 5:00 PM", "2005-05-05 7:59 PM")]
        [InlineData("2005-05-05 5:00 PM", "2005-05-05 8:01 PM")]
        [InlineData("2005-05-05 6:31 PM", "2005-05-05 9:30 PM")]
        [InlineData("2005-05-05 6:31 PM", "2005-05-05 9:31 PM")]
        public void CalculateAsleepHours_FractionalHours_OnlyCountsFullHours(string startTime, string bedTime)
        {
            // Arrange
            var shiftInformation = new ShiftInformation();
            shiftInformation.StartTime = DateTime.Parse(startTime);
            shiftInformation.BedTime = DateTime.Parse(bedTime);
            var service = new PaymentCalculationService();

            // Act 
            var actualAsleepHours = service.CalculateAsleepHours(shiftInformation);
            var expectedAsleepHours = (int)(shiftInformation.StartTime.Date.AddDays(1) - shiftInformation.BedTime).TotalHours;
            if (expectedAsleepHours < 0)
            {
                expectedAsleepHours = 0;
            }

            Assert.Equal(expectedAsleepHours, actualAsleepHours);
        }

        [Theory]
        [InlineData("2005-05-05 9:31 PM", "2005-05-05 7:30 PM")]
        [InlineData("2005-05-05 9:31 PM", "2005-05-05 9:30 PM")]
        public void CalculateAwakeHours_BedTimeBeforeStartTime_DoesNotReturnNegativeHours(string startTime, string bedTime)
        {
            // Arrange
            var shiftInformation = new ShiftInformation();
            shiftInformation.StartTime = DateTime.Parse(startTime);
            shiftInformation.BedTime = DateTime.Parse(bedTime);
            var service = new PaymentCalculationService();

            // Act 
            var actualAwakeHours = service.CalculateAwakeHours(shiftInformation);
            var expectedAwakeHours = (int)(shiftInformation.BedTime - shiftInformation.StartTime).TotalHours;
            if (expectedAwakeHours < 0)
            {
                expectedAwakeHours = 0;
            }

            Assert.Equal(expectedAwakeHours, actualAwakeHours);
        }

        [Theory]
        [InlineData("2005-05-05 9:31 PM", "2005-05-06 12:01 AM")]
        [InlineData("2005-05-05 9:31 PM", "2005-05-06 2:30 AM")]
        public void CalculateAsleepHours_MidnightBeforeBedTime_DoesNotReturnNegativeHours(string startTime, string bedTime)
        {
            // Arrange
            var shiftInformation = new ShiftInformation();
            shiftInformation.StartTime = DateTime.Parse(startTime);
            shiftInformation.BedTime = DateTime.Parse(bedTime);
            var service = new PaymentCalculationService();

            // Act 
            var actualAsleepHours = service.CalculateAsleepHours(shiftInformation);
            var expectedAsleepHours = (int)(shiftInformation.StartTime.Date.AddDays(1) - shiftInformation.BedTime).TotalHours;
            if (expectedAsleepHours < 0)
            {
                expectedAsleepHours = 0;
            }

            Assert.Equal(expectedAsleepHours, actualAsleepHours);
        }

        [Theory]
        [InlineData("2005-05-05 2:12 PM")]
        [InlineData("2005-05-05 4:59 PM")]
        [InlineData("2005-05-05 12:12 AM")]
        public void FixStartTime_HourBeforeFive_MakeStartTimeFive(string startTime)
        {
            // Arrange
            var originalStartTime = DateTime.Parse(startTime);
            var service = new PaymentCalculationService();

            // Act
            var fixedStartTime = service.FixStartTime(originalStartTime);

            // Assert
            Assert.True(fixedStartTime.Hour == 17);
        }

        [Theory]
        [InlineData("2005-05-05 5:00 PM")]
        [InlineData("2005-05-05 5:01 PM")]
        [InlineData("2005-05-05 7:59 PM")]
        [InlineData("2005-05-05 11:12 PM")]
        public void FixStartTime_HourAfterFive_DoesNotChangeStartTime(string startTime)
        {
            // Arrange
            var originalStartTime = DateTime.Parse(startTime);
            var service = new PaymentCalculationService();
            

            // Act
            var fixedStartTime = service.FixStartTime(originalStartTime);

            // Assert
            Assert.True(fixedStartTime == originalStartTime);
        }


    }
}

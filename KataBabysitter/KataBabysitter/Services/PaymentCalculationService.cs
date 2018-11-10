using KataBabysitter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KataBabysitter.Services
{
    public class PaymentCalculationService
    {
        public async Task<int> CalculateAsync(ShiftInformation shiftInformation)
        {
            shiftInformation = FixPostMidnightDates(shiftInformation);
            var awakeHours = CalculateAwakeHours(shiftInformation);
            var asleepHours = CalculateAsleepHours(shiftInformation);
            var postMidnightHours = CalculatePostMidnightHours(shiftInformation);
            return CalculateIncome(awakeHours, asleepHours, postMidnightHours);
        }

        public int CalculateIncome(int awakeHours, int asleepHours, int postMidnightHours)
        {
            int totalIncome = 0;
            totalIncome += awakeHours * 12;
            totalIncome += asleepHours * 8;
            totalIncome += postMidnightHours * 16;
            return totalIncome;
        }

        public int CalculateAwakeHours(ShiftInformation shiftInformation)
        {
            return (int)(shiftInformation.BedTime - FixStartTime(shiftInformation.StartTime)).TotalHours;
        }

        public int CalculateAsleepHours(ShiftInformation shiftInformation)
        {
            return (int)(DateTime.Today.AddDays(1) - shiftInformation.BedTime).TotalHours;
        }

        public int CalculatePostMidnightHours(ShiftInformation shiftInformation)
        {
            return (int)(shiftInformation.EndTime - DateTime.Today.AddDays(1)).TotalHours;
        }

        public DateTime FixStartTime(DateTime startTime)
        {
            if (startTime.Hour < 17)
            {
                startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 17, 0, 0);
            }
            return startTime;
        }

        public ShiftInformation FixPostMidnightDates(ShiftInformation shiftInformation)
        {
            if (shiftInformation.StartTime.Hour < 5)
            {
                shiftInformation.StartTime = shiftInformation.StartTime.AddDays(1);
            }

            if (shiftInformation.EndTime.Hour < 5)
            {
                shiftInformation.EndTime = shiftInformation.EndTime.AddDays(1);
            }

            if (shiftInformation.BedTime.Hour < 5)
            {
                shiftInformation.BedTime = shiftInformation.BedTime.AddDays(1);
            }
            return shiftInformation;
        }
    }
}

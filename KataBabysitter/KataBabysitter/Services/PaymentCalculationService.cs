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
            var awakeHours = (int)(shiftInformation.BedTime - FixStartTime(shiftInformation.StartTime)).TotalHours;
            if(awakeHours < 0)
            {
                awakeHours = 0;
            }
            return awakeHours;
        }

        public int CalculateAsleepHours(ShiftInformation shiftInformation)
        {
            var asleepHours = (int)(FixStartTime(shiftInformation.StartTime).Date.AddDays(1) - shiftInformation.BedTime).TotalHours;
            if (asleepHours < 0)
            {
                asleepHours = 0;
            }
            return asleepHours;
        }

        public int CalculatePostMidnightHours(ShiftInformation shiftInformation)
        {
            var postMidnightHours = (int)(shiftInformation.EndTime - DateTime.Today.AddDays(1)).TotalHours;
            if (postMidnightHours < 0)
            {
                postMidnightHours = 0;
            }
            return postMidnightHours;
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

using KataBabysitter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KataBabysitter.Services
{
    public class PaymentCalculationService
    {
        public int Calculate(ShiftInformation shiftInformation)
        {
            shiftInformation = FixPostMidnightDates(shiftInformation);
            var awakeHours = CalculateAwakeHours(shiftInformation);
            var asleepHours = CalculateAsleepHours(shiftInformation);
            var postMidnightHours = CalculatePostMidnightHours(shiftInformation);
            return CalculateIncome(awakeHours, asleepHours, postMidnightHours);
        }

        private int CalculateIncome(int awakeHours, int asleepHours, int postMidnightHours)
        {
            int totalIncome = 0;
            totalIncome += awakeHours * 12;
            totalIncome += asleepHours * 8;
            totalIncome += postMidnightHours * 16;
            return totalIncome;
        }

        private int CalculateAwakeHours(ShiftInformation shiftInformation)
        {
            return (int)(shiftInformation.BedTime - FixStartTime(shiftInformation.StartTime)).TotalHours;
        }

        private int CalculateAsleepHours(ShiftInformation shiftInformation)
        {
            return (int)(DateTime.Today.AddDays(1) - shiftInformation.BedTime).TotalHours;
        }

        private int CalculatePostMidnightHours(ShiftInformation shiftInformation)
        {
            return (int)(shiftInformation.EndTime - DateTime.Today.AddDays(1)).TotalHours;
        }

        private DateTime FixStartTime(DateTime startTime)
        {
            if (startTime.Hour < 17)
            {
                startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 17, 0, 0);
            }
            return startTime;
        }

        private ShiftInformation FixPostMidnightDates(ShiftInformation shiftInformation)
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

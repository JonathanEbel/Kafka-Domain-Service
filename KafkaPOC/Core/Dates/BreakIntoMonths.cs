using System;
using System.Collections.Generic;

namespace Core.Dates
{
    public class BreakIntoMonths
    {
        public static IEnumerable<DateTime> GetEachMonthlyDate(DateTime startDate, DateTime endDate, int dayOfMonth)
        {
            var allMonthlyDates = new List<DateTime>();

            while (startDate.Date < endDate.Date)
            {
                // pull out month and year
                var nextDate = DateTime.Parse(startDate.Month + "/" + dayOfMonth + "/" + startDate.Year);
                allMonthlyDates.Add(nextDate);

                startDate = startDate.AddMonths(1);  //increment
            }

            return allMonthlyDates;
        }
    }
}

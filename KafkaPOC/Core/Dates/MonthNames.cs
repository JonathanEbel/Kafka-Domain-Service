using System.Collections.Generic;

namespace Core.Dates
{
    public class MonthNames
    {
        public static string GetMonthName(int month)
        {
            var months = new Dictionary<int, string>
            {
                { 1, "January" },
                { 2, "Feburary" },
                { 3, "March" },
                { 4, "April" },
                { 5, "May" },
                { 6, "June" },
                { 7, "July" },
                { 8, "August" },
                { 9, "September" },
                { 10, "October" },
                { 11, "November" },
                { 12, "December" }
            };

            return months[month];
        }
    }
}

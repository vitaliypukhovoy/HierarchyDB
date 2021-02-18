using System;

namespace PMS.Infrastructure.Binding
{
    public readonly struct Date
    {
        public readonly int Year { get; }
        public readonly int Month { get; }
        public readonly int Day { get; }
        public Date(int year, int month, int day)
        {
            if (year < 1 || year > 9999)
                throw new ArgumentOutOfRangeException(nameof(year));
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));
            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                throw new ArgumentOutOfRangeException(nameof(day));

            Year = year;
            Month = month;
            Day = day;
        }

    }
}

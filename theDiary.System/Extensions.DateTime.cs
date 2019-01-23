using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static partial class SystemExtensions
    {
        /// <summary>
        /// Determines the week number for the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> value.</param>
        /// <returns>The week number of the <paramref name="date"/> value.</returns>
        public static int WeekOfYear(this DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns the greater of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is greater.</returns>
        public static DateTime Max(this DateTime date1, DateTime date2)
        {
            return date1 >= date2 ? date1 : date2;
        }

        /// <summary>
        /// Returns the lesser of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is lesser.</returns>
        public static DateTime Min(this DateTime date1, DateTime date2)
        {
            return date1 <= date2 ? date1 : date2;
        }

        /// <summary>
        /// Returns the greater of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is greater.</returns>
        public static DateTime Max(this DateTime? date1, DateTime? date2)
        {
            return date1.GetValueOrDefault().Min(date2.GetValueOrDefault());
        }

        /// <summary>
        /// Returns the lesser of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is lesser.</returns>
        public static DateTime Min(this DateTime? date1, DateTime? date2)
        {
            return date1.GetValueOrDefault().Min(date2.GetValueOrDefault());
        }

        /// <summary>
        /// Returns the greater of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is greater.</returns>
        public static DateTime Max(this DateTime date1, DateTime? date2)
        {
            return date1.Max(date2.GetValueOrDefault());
        }

        /// <summary>
        /// Returns the lesser of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is lesser.</returns>
        public static DateTime Min(this DateTime date1, DateTime? date2)
        {
            return date1.Min(date2.GetValueOrDefault());
        }

        /// <summary>
        /// Returns the greater of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is greater.</returns>
        public static DateTime Max(this DateTime? date1, DateTime date2)
        {
            return date1.GetValueOrDefault().Max(date2);
        }

        /// <summary>
        /// Returns the lesser of two <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="date1">The first of two <see cref="DateTime"/> value to compare.</param>
        /// <param name="date2">The second of two <see cref="DateTime"/> value to compare.</param>
        /// <returns>Parameter <paramref name="date1"/> or <paramref name="date2"/>, which ever is lesser.</returns>
        public static DateTime Min(this DateTime? date1, DateTime date2)
        {
            return date1.GetValueOrDefault().Min(date2);
        }
    }
}

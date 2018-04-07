using System;
using System.Collections.Generic;
using System.Linq;

namespace Simples.SouthAfrican
{
	public static class PublicHoliday
	{
		static readonly Dictionary<int, DateTime> DatesOfEaster = new Dictionary<int, DateTime>();
		static readonly (int Month, int Day)[] StaticHolidays =
			{(1, 1), (3, 21), (4, 27), (5, 1), (6, 16), (8, 9), (9, 24), (12, 16), (12, 25), (12, 26)};

		public static DateTime NextWorkDay(in this DateTime @this)
		{
			var date = @this;
			while (date.IsWeekend() || date.IsPublicHoliday()) date = date.AddDays(1);
			return date;
		}

		public static bool IsWeekend(this DateTime @this)
		{
			var date = @this.Date;
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}
		
		public static bool IsPublicHoliday(this DateTime @this)
		{
			var date = @this.Date;
			return IsStaticHoliday(date) || IsMondayHoliday(date) || IsEasterHoliday(date);
		}

		static bool IsStaticHoliday(DateTime date) => StaticHolidays.Any(d => date == Create(date.Year, d));

		static bool IsMondayHoliday(DateTime date) => date.DayOfWeek == DayOfWeek.Monday && IsStaticHoliday(date.AddDays(-1));

		static bool IsEasterHoliday(DateTime date)
		{
			if(!DatesOfEaster.TryGetValue(date.Year, out var easter))
				easter = DateOfEaster(date.Year);
			return date == easter.AddDays(-2) || date == easter.AddDays(1);
		}

		static DateTime DateOfEaster(int year)
		{
			var g = year % 19;
			var c = year / 100;
			var h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
			var i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));
			var day = i - ((year + year / 4 + i + 2 - c + c / 4) % 7) + 28;
			var month = 3;
			DateTime Create() => new DateTime(year, month, day);
			if (day <= 31) return Create();
			month++;
			day -= 31;
			return Create();
		}

		static DateTime Create(int year, (int Month, int Day) d) => new DateTime(year, d.Month, d.Day);
	}
}
using Simples.SouthAfrican;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimplesTests.SouthAfrican
{
	public class PublicHolidayTests
	{
		static readonly (int Year, DateTime GoodFriday, DateTime FamilyDay)[] Years =
		{
			(2013, new DateTime(2013, 03, 29), new DateTime(2013, 04, 01)),
			(2014, new DateTime(2014, 04, 18), new DateTime(2014, 04, 21)),
			(2015, new DateTime(2015, 04, 03), new DateTime(2015, 04, 06)),
			(2016, new DateTime(2016, 03, 25), new DateTime(2016, 03, 28)),
			(2017, new DateTime(2017, 04, 14), new DateTime(2017, 04, 17)),
			(2018, new DateTime(2018, 03, 30), new DateTime(2018, 04, 02)),
			(2019, new DateTime(2019, 04, 19), new DateTime(2019, 04, 22))
		};

		[Theory]
		[MemberData(nameof(GetHolidays))]
		public void IsPublicHoliday_TestHolidays(DateTime date)
		{
			var isHoliday = date.IsPublicHoliday();
			Assert.True(isHoliday, $"{date} is failing");
		}

		[Fact]
		public void IsPublicHoliday_TestNonHolidays()
		{
			var notHolidays = GetNotHolidays();
			foreach (var date in notHolidays)
			{
				var isHoliday = date.IsPublicHoliday();
				Assert.False(isHoliday, $"{date} is failing");
			}
		}

		[Fact]
		public void IsPublicHoliday_MondaysAfterASundayAreHolidays()
		{
			var holidays = GetHolidaysWithMondays();
			foreach (var date in holidays)
			{
				var isHoliday = date.IsPublicHoliday();
				Assert.True(isHoliday, $"{date} is failing");
			}
		}

		[Fact]
		public void IsPublicHoliday_AnyTimeOfTheDay()
		{
			var holidays = GetHolidaysWithMondays().ToArray();
			for (var i = 0; i < holidays.Length; i++)
			{
				var hour = i / (double) holidays.Length * 23;
				var date = holidays[i].AddHours(hour);
				var isHoliday = date.IsPublicHoliday();
				Assert.True(isHoliday, $"{date} is failing");
			}
		}

		[Fact]
		public void NextWorkDay_NotHolidaysAreReturned()
		{
			var notHolidays = GetNotHolidays().Where(d => !d.IsWeekend());
			foreach (var date in notHolidays)
			{
				var nextWorkDay = date.NextWorkDay();
				Assert.Equal(date, nextWorkDay);
			}
		}

		[Fact]
		public void NextWorkDay_WeekendsAndHolidaysReturnNextDay()
		{
			var weekends = GetNotHolidays().Where(d => d.DayOfWeek != DayOfWeek.Saturday & d.DayOfWeek != DayOfWeek.Sunday);
			var holidays = GetHolidaysWithMondays().ToArray();
			var holidaysAndWeekends = holidays.Union(weekends);
			foreach (var date in holidaysAndWeekends)
			{
				var nextDate = date;
				while (holidays.Contains(nextDate) || nextDate.IsWeekend())
					nextDate = nextDate.AddDays(1);
				var result = date.NextWorkDay();
				Assert.Equal(nextDate, result);
			}
		}

		public static IEnumerable<DateTime> GetNotHolidays()
		{
			var holidays = GetHolidaysWithMondays();
			var allDays = Enumerable
				.Range(0, 1 + holidays.Max().Subtract(holidays.Min()).Days)
				.Select(offset => holidays.Min().AddDays(offset));
			return allDays.Except(holidays);
		}

		public static TheoryData<DateTime> GetHolidays()
		{
			var testData = new TheoryData<DateTime>();
			foreach (var td in CreateHolidays())
				testData.Add(td);
			return testData;
		}

		static IEnumerable<DateTime> GetHolidaysWithMondays()
		{
			var allHolidays = CreateHolidays().ToList();
			var sundayHolidays = from d in allHolidays
				where d.DayOfWeek == DayOfWeek.Sunday
				select d.AddDays(1);
			allHolidays.AddRange(sundayHolidays);
			return allHolidays;
		}

		static IEnumerable<DateTime> CreateHolidays()
		{
			foreach (var year in Years)
			{
				yield return new DateTime(year.Year, 01, 01); //New Year
				yield return new DateTime(year.Year, 03, 21); //Human Rights Day
				yield return new DateTime(year.Year, 04, 27); //Freedom Day
				yield return new DateTime(year.Year, 05, 01); //Labour Day
				yield return new DateTime(year.Year, 06, 16); //Youth Day
				yield return new DateTime(year.Year, 08, 09); //National Women's Day
				yield return new DateTime(year.Year, 09, 24); //Heritage Day
				yield return new DateTime(year.Year, 12, 16); //Day of Reconciliation
				yield return new DateTime(year.Year, 12, 25); //Christmas Day
				yield return new DateTime(year.Year, 12, 26); //Day of Goodwill
				yield return year.GoodFriday;
				yield return year.FamilyDay;
			}
		}
	}
}
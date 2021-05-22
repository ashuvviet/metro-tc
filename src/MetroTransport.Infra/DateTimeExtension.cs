using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTransport.Infra
{
  public static class DateTimeExtension
  {
    private static TimeSpan WeekDayPeekHourStartMorning => new TimeSpan(7, 0, 0);
    private static TimeSpan WeekDayPeekHourEndMorning => new TimeSpan(10, 30, 0);

    private static TimeSpan WeekDayPeekHourStartEvening => new TimeSpan(17, 0, 0);
    private static TimeSpan WeekDayPeekHourEndEvening => new TimeSpan(20, 0, 0);

    private static TimeSpan WeekendDayPeekHourStartMorning => new TimeSpan(9, 0, 0);
    private static TimeSpan WeekendDayPeekHourEndMorning => new TimeSpan(11, 0, 0);

    private static TimeSpan WeekendDayPeekHourStartEvening => new TimeSpan(18, 0, 0);
    private static TimeSpan WeekendDayPeekHourEndEvening => new TimeSpan(22, 0, 0);

    public static bool IsWeekend(this DateTime time)
    {
      return time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday;
    }

    public static bool IsPeekHour(this DateTime time)
    {
      var timeOfDay = time.TimeOfDay;
      if (time.IsWeekend())
      {
        if ((timeOfDay >= WeekendDayPeekHourStartMorning && timeOfDay <= WeekendDayPeekHourEndMorning)
            || (timeOfDay >= WeekendDayPeekHourStartEvening && timeOfDay <= WeekendDayPeekHourEndEvening))
        {
          return true;
        }
      }

      if ((timeOfDay >= WeekDayPeekHourStartMorning && timeOfDay <= WeekDayPeekHourEndMorning)
          || (timeOfDay >= WeekDayPeekHourStartEvening && timeOfDay <= WeekDayPeekHourEndEvening))
      {
        return true;
      }

      return false;
    }

    public static bool IsOffPeekHour(this DateTime time)
    {
      var timeOfDay = time.TimeOfDay;
      if (time.IsWeekend())
      {
        if (timeOfDay >= WeekendDayPeekHourStartEvening && timeOfDay <= WeekendDayPeekHourEndEvening)
        {
          return true;
        }
      }

      if (timeOfDay >= WeekDayPeekHourStartEvening && timeOfDay <= WeekDayPeekHourEndEvening)
      {
        return true;
      }

      return false;
    }
  }
}

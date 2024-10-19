namespace DentalApplication.Helper
{
    public static class DateUtility
    {
        public static DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

        public static DateTime GetStartAndEndDate(DateOnly date, TimeOnly time)
        {
            DateTime startDateTime = date.ToDateTime(time);
            return startDateTime;
        }
    }
}

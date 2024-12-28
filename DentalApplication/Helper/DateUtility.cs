using DentalApplication.Errors;

namespace DentalApplication.Helper
{
    public static class DateUtility
    {
        public static DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

        public static DateTime GetStartAndEndDate(DateOnly date, string time)
        {
            if (!TimeOnly.TryParse(time, out TimeOnly value))
            {
                throw new BadRequestException("Time must have an valid format. Use HH:mm");
            }
            DateTime startDateTime = date.ToDateTime(value);
            return startDateTime;
        }
    }
}

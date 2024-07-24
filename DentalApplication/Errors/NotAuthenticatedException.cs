namespace DentalApplication.Errors
{
    public class NotAuthenticatedException : Exception
    {
        public List<string> Errors { get; } = new();

        public NotAuthenticatedException(string failures)
        {
            Errors.Add(failures);
        }
    }
}

namespace DentalApplication.Errors
{
    public class NotAuthorizedException : Exception
    {
        public List<string> Errors { get; } = new();

        public NotAuthorizedException(string failures)
        {
            Errors.Add(failures);
        }
    }
}

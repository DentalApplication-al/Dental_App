namespace DentalApplication.Errors
{
    public class EmailNotSentException : Exception
    {
        public List<string> Errors { get; set; } = new();
        public EmailNotSentException(string error)
        {
            Errors.Add(error);
        }
    }
}

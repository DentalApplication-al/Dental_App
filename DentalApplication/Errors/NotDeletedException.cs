namespace DentalApplication.Errors
{
    public class NotDeletedException : Exception
    {
        public List<string> Errors { get; } = new();

        public NotDeletedException(string error)
        {
            Errors.Add(error);
        }
    }
}

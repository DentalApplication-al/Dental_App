namespace DentalApplication.Errors
{
    public class NotDeletedException : Exception
    {
        public List<string> Errors { get; }

        public NotDeletedException(string error)
        {
            Errors.Add(error);
        }
    }
}

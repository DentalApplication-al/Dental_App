namespace DentalApplication.Errors
{
    public class NotFoundException : Exception
    {
        public List<string> Errors { get; } = new();

        public NotFoundException(string failures)
        {
            Errors.Add(failures);
        }
    }
}

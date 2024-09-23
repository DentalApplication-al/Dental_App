namespace DentalApplication.Errors
{
    public class ServerError : Exception
    {
        public List<string> Errors { get; set; } = new();
        public ServerError(string error)
        {
            Errors.Add(error);
        }
    }
}

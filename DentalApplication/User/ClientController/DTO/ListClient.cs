namespace DentalApplication.User.ClientController.DTO
{
    public class ListClient
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? last_appointment { get; set; }
        public string registered_date { get; set; }
    }
}

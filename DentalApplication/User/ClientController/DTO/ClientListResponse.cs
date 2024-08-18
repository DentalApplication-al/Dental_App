namespace DentalApplication.User.ClientController.DTO
{
    public class ClientListResponse
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public DateTime? last_appointment { get; set; }
        public DateTime registered_date { get; set; }
    }
}

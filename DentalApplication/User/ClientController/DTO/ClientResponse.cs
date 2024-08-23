using DentalDomain.Users.Clients;

namespace DentalApplication.User.ClientController.DTO
{
    public class ClientResponse
    {
        public DateOnly? birthday { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }


        private ClientResponse(Client client)
        {
            birthday = client.Birthday;
            first_name = client.FirstName;
            last_name = client.LastName;
            email = client.Email;
            phone = client.Phone;
        }
        public static ClientResponse Map(Client client)
        {
            client ??= Client.Create();
            return new ClientResponse(client);
        }
        public static List<ClientResponse> Map(List<Client> clients)
        {
            clients ??= [];
            return clients.Select(client => Map(client)).ToList();
        }
    }
}

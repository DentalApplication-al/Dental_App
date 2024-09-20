using DentalDomain.Users.Clients;
using Microsoft.IdentityModel.Tokens;

namespace DentalApplication.User.ClientController.DTO
{
    public class ClientResponse
    {
        public string? birthday { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public int? past { get; set; }
        public int? upcoming { get; set; }
        public string registered_date { get; set; }
        public List<FileResponse> documents { get; set; }



        //private ClientResponse(Client client)
        //{
        //    birthday = client.Birthday;
        //    first_name = client.FirstName;
        //    last_name = client.LastName;
        //    email = client.Email;
        //    phone = client.Phone;
        //}
        //public static ClientResponse Map(Client client)
        //{
        //    client ??= Client.Create();
        //    return new ClientResponse(client);
        //}
        //public static List<ClientResponse> Map(List<Client> clients)
        //{
        //    clients ??= [];
        //    return clients.Select(client => Map(client)).ToList();
        //}
        public ClientResponse()
        {
            
        }
    }
}

using DentalDomain.Users.Clients;
using DentalDomain.Users.Enums;
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
        public bool? heart_condition { get; set; }
        public bool? diabetes { get; set; }
        public bool? hypertension { get; set; }
        public bool? pregnancy_status { get; set; }
        public bool? bleeding_disorders { get; set; }
        public bool? immunocompromised { get; set; }
        public string? allergies { get; set; }
        public string? description { get; set; }
        public string? other_conditions { get; set; }
        public string? current_medications { get; set; }
        public string? special_notes { get; set; }
        public Gender gender { get; set; }
        public List<FileResponse> documents { get; set; }
        public Guid id { get; set; }


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

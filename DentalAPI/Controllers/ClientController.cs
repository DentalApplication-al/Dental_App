using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Errors;
using DentalApplication.User.ClientController.AddClientFile;
using DentalApplication.User.ClientController.ClientAppointments;
using DentalApplication.User.ClientController.Delete;
using DentalApplication.User.ClientController.DeleteClientFile;
using DentalApplication.User.ClientController.DTO;
using DentalApplication.User.ClientController.GetAll;
using DentalApplication.User.ClientController.GetById;
using DentalApplication.User.ClientController.Update;
using DentalContracts.UserContracts.ClientContracts;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("token")]
        public async Task<string> Tokens()
        {

            var client = new HttpClient();
            var clientId = "V0Isqm49jfSWCPdJ1z9ASQwiAvN3XFDqqTH6XdyjX7FkZbO6\r\n";
            var clientSecret = "cp1ZuopYOXAyXRqdki73kpqVN7F0RNb1DDXhpAQVGeSHTUOY37AVhCeRSQPeFI6r";
            var base64EncodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedCredentials);

            var requestBody = new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "code", "w4tngz6O" },
    { "redirect_uri", "https://sp.example.com" },
    { "grant_type", "authorization_code" }
});

            var response = await client.PostAsync("https://eu3.api.vodafone.com/openIDConnectCIBA/v1/token", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsStringAsync();
                var accessToken = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse).access_token;
                Console.WriteLine($"Access Token: {accessToken}");
            }
            else
            {
                Console.WriteLine("Failed to get access token.");
            }


            return "";
        }
        
        [HasPermission(Permission.CLIENT_ADD)]
        [HttpPost("add")]
        public async Task<Guid> AddClient(AddClientCommand command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_UPDATE)]
        [HttpPut("update/{id}")]
        public async Task<Guid> UpdateClient(UpdateClientCommand command, Guid id)
        {
            command.id = id;
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_GET_ALL)]
        [HttpGet("get-all")]
        public async Task<PaginatedResponse<ListClient>> AllClients([FromQuery] GetAllClientCommand? command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_GET_BY_ID)]
        [HttpGet("get-by-id/{id}")]
        public async Task<ClientResponse> GetClientById([FromQuery] GetClientByIdCommand? command)
        {
            return await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_GET_APPOINTMENTS)]
        [HttpGet("getClientAppointments/{id}")]
        public async Task<PaginatedResponse<ListAppointment>> GetClientAppointments([FromQuery] GetClientAppointmentsCommand? command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.CLIENT_DELETE)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteClient([FromQuery] DeleteClientCommand command)
        {
            await _mediator.Send(command);
        }

        [HasPermission(Permission.CLIENT_UPLOAD_FILE)]
        [HttpPost("uploadFile{id}")]
        public async Task UploadClientFile([FromRoute] Guid id, [FromForm] List<IFormFile> files, CancellationToken cancellationToken)
        {
            var command = Token.GetToken<AddClientFileCommand>(HttpContext);
            command.files = files;
            command.clientId = id;

            await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.CLIENT_UPLOAD_FILE)]
        [HttpDelete("deleteFile{id}")]
        public async Task DeleteClientFile([FromRoute] Guid id, [FromBody]List<Guid> files, CancellationToken cancellationToken)
        {
            var command = Token.GetToken<DeleteClientFileCommand>(HttpContext);
            command.files = files;
            command.clientId = id;
            await _mediator.Send(command, cancellationToken);
        }
    }



    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}

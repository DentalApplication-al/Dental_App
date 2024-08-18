using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.ServicesController;
using DentalApplication.ServicesController.Add;
using DentalApplication.ServicesController.Delete;
using DentalApplication.ServicesController.Get;
using DentalApplication.ServicesController.GetById;
using DentalApplication.ServicesController.Update;
using DentalInfrastructure.Authentication;
using DentalInfrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;

namespace DentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        public ServicesController(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [HasPermission(Permission.ADDSERVICE)]
        [HttpPost("add")]
        public async Task<ServiceResponse> AddService(AddServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.UPDATESRVICE)]
        [HttpPut("update")]
        public async Task<ServiceResponse> UpdateService(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.DELETESERVICE)]
        [HttpDelete("delete")]
        public async Task<bool> DeleteService(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETALLSERVICES)]
        [HttpGet("getall")]
        public async Task<List<ServiceResponse>> GetAllServices([FromQuery] GetAllServicesCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HasPermission(Permission.GETSERVICEBYID)]
        [HttpGet("getbyid")]
        public async Task<ServiceResponse> GetServiceById(GetServiceByIdCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("test")]
        public async Task<string> SendEmail()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkYmQ1YjM3OS1iMmI5LTRhMGYtYTI0Mi1lNjI2MzYxYTRmMmQiLCJyb2xpIjoiQURNSU4iLCJjbGluaWMiOiJkMzBiODNlOC05YjI5LTQ5OWYtZmViNy0wOGRjYmZhYTljNjMiLCJleHAiOjE3MjQwODgzNjEsImlzcyI6IkRlbnRhbEFwcGxpY2F0aW9uIiwiYXVkIjoiRGVudGFsQXBwbGljYXRpb24ifQ.lPO_NzbVgENS5xzMxCumkqZ_e-3IBtFGWacpU9zrbLA";
            HttpClient _httpClient = new();
            try
            {
                // Set the Authorization header with the Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Make the GET request
                var response = await _httpClient.GetAsync("https://amalka-001-site1.etempurl.com/client/get-all");

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read and return the response content
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                throw new Exception("An error occurred while calling the API.", ex);
            }
        }

    }
}

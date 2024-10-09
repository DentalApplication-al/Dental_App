using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using DentalContracts.UserContracts.ClientContracts;
using DentalDomain.Users.Clients;
using MediatR;

namespace DentalApplication.User.ClientController.Add
{
    public class AddClientCommmandHandler : IRequestHandler<AddClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;

        public AddClientCommmandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Guid> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var client = Client.Create(
                request.birthday.Value,
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.clinic_id.Value,
                request.description,
                request.gender.Value,
                request.heart_condition,
                request.diabetes,
                request.hypertension,
                request.bleeding_disorders,
                request.immunocompromised,
                request.allergies,
                request.other_conditions,
                request.current_medications,
                request.special_notes,
                request.pregnancy_status
            );

            await _clientRepository.AddAsync(client);

            await _clientRepository.SaveChangesAsync();
            return client.Id;
        }
    }
}

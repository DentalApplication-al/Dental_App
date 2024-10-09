using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using MediatR;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Guid> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.clinic_id.Value, request.clientId.Value) ??
                throw new NotFoundException("The client could not be found.");

            client.Update(
                request.first_name,
                request.last_name,
                request.phone,
                request.email,
                request.birthday.Value,
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

            await _clientRepository.UpdateAsync(client);
            await _clientRepository.SaveChangesAsync();

            return client.Id;
        }
    }
}

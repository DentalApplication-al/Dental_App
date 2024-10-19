using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Helper;
using DentalDomain.Appointments;
using DentalDomain.Users.Clients;
using MediatR;

namespace DentalApplication.AppointmentController.Add
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _repository;
        private IClientRepository _clientRepository;
        private readonly IMediator _mediator;
        private readonly IStaffRepository _staffRepository;
        private readonly IServiceRepository _serviceRepository;
        public AddAppointmentCommandHandler(
            IAppointmentRepository repository,
            IClientRepository clientRepository,
            IMediator mediator,
            IStaffRepository staffRepository,
            IServiceRepository serviceRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _mediator = mediator;
            _staffRepository = staffRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Guid> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            Client client;
            if (request.existingClient.HasValue)
            {
                client = await _clientRepository.GetByIdAsync(request.existingClient.Value, request.clinic_id.Value);
            }
            else
            {
                request.newClient.clinic_id = request.clinic_id.Value;
                request.newClient.loged_in_staff_id = request.clinic_id.Value;
                var clientId = await _mediator.Send(request.newClient);
                client = await _clientRepository.GetByIdAsync(clientId, request.clinic_id.Value);
            }

            var docotors = await _staffRepository.GetDoctorsById(request.doctors, request.clinic_id.Value);

            var service = await _serviceRepository.GetFullServiceById(request.clinic_id.Value, request.serviceId);



            var appointment = new Appointment
            {
                Client = client,
                Service = service,
                Doctor = docotors,
                ClinicId = request.clinic_id.Value,
                StartDate = DateUtility.GetStartAndEndDate(request.date, request.startTime),
                EndDate = DateUtility.GetStartAndEndDate(request.date, request.endTime),
                CreatedOn = DateUtility.GetDateTimeNow(),
                Price = request.price,
            };
            var app = await _repository.AddAppointmentAsync(appointment);
            await _repository.SaveChangesAsync();

            return app.Id;
        }
    }
}

using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Extensions;
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

            if (request.IsExistingClient())
            {
                client = await _clientRepository.GetByIdAsync(request.clinic_id.Value, request.existingClient.Value) ??
                    throw new NotFoundException("The selected client was not found.");
            }
            else
            {
                request.MapClinic();
                
                var clientId = await _mediator.Send(request.newClient);

                client = await _clientRepository.GetByIdAsync(clientId, request.clinic_id.Value) ??
                    throw new NotFoundException("The new client could not be added.");
            }

            var doctors = await _staffRepository.GetDoctorsById(request.doctors, request.clinic_id.Value);

            if (doctors == null || doctors.Count != request.doctors.Count)
            {
                throw new NotFoundException("Doctor could not be found.");
            }

            var service = await _serviceRepository.GetFullServiceById(request.clinic_id.Value, request.serviceId) ??
                throw new NotFoundException("The selected service could not be found.");

            var appointment = new Appointment
            {
                Client = client,
                Service = service,
                Doctor = doctors,
                ClinicId = request.clinic_id.Value,
                StartDate = DateUtility.GetStartAndEndDate(request.date, request.startTime),
                EndDate = DateUtility.GetStartAndEndDate(request.date, request.endTime),
                CreatedOn = DateUtility.GetDateTimeNow(),
                Price = request.price,
            };

            if (!appointment.IsDateValid())
            {
                throw new BadRequestException("End time should be greater than start time.");
            }

            foreach (var item in doctors)
            {
                if(!await _repository.IsDoctorAvailable(item.Id, request.clinic_id.Value, appointment.StartDate, appointment.EndDate))
                {
                    throw new BadRequestException($"Doctor {item.FirstName} is not available at {appointment.StartDate.ToString("dd-MM-yyyy HH:mm")} - {appointment.EndDate.ToString("dd-MM-yyyy HH:mm")}");
                }
            }

            var app = await _repository.AddAppointmentAsync(appointment);

            await _repository.SaveChangesAsync().EnsureSaved("The appointment could not be added.");

            return app.Id;
        }
    }
}

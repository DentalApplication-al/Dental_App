using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Clinics;
using DentalDomain.Users.Enums;
using DentalDomain.Users.Staffs;
using MediatR;

namespace DentalApplication.User.SuperAdmin.AddClinic
{
    public class AddClinicCommandHandler : IRequestHandler<AddClinicCommand, ClinicResponse>
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IStaffRepository _staffRepository;
        public AddClinicCommandHandler(IClinicRepository clinicRepository, IStaffRepository staffRepository)
        {
            _clinicRepository = clinicRepository;
            _staffRepository = staffRepository;
        }

        public async Task<ClinicResponse> Handle(AddClinicCommand request, CancellationToken cancellationToken)
        {
            var clinic = Clinic.Create(
                request.clinic_name,
                request.clinic_address,
                request.clinic_nipt,
                request.admin_phone,
                request.admin_email);

            await _clinicRepository.AddAsync(clinic);

            var staff = Staff.Create(
                request.admin_first_name,
                request.admin_last_name,
                request.admin_email,
                request.admin_phone,
                request.admin_birthday,
                request.admin_username,
                Staff.CreateAdminPassword(),
                Role.ADMIN,
                clinic.Id
                );

            await _staffRepository.AddAsync(staff);

            await _staffRepository.SaveChangesAsync();

            return ClinicResponse.Map(clinic);
        }
    }
}

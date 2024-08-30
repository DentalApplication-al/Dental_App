using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.User.StaffController.DTO;
using MediatR;

namespace DentalApplication.User.StaffController.GetById
{
    public class GetStaffByIdCommandHandler : IRequestHandler<GetStaffByIdCommand, StaffResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IBlobStorage _blobStorage;

        public GetStaffByIdCommandHandler(IStaffRepository staffRepository, IBlobStorage blobStorage)
        {
            _staffRepository = staffRepository;
            _blobStorage = blobStorage;
        }

        public async Task<StaffResponse> Handle(GetStaffByIdCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetstaffByIdAsync(request.staff_id.Value, request.clinic_id.Value) ??
                throw new NotFoundException("Staff could not be found");

            staff.picture = _blobStorage.GetLink(staff.picture);

            return staff;
        }
    }
}
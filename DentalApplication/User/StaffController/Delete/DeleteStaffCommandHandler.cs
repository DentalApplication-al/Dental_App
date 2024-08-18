using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Delete
{
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _staffRepository.DeleteAsync(request.staff_id);

            if (!isDeleted)
            {
                throw new NotDeletedException(_stringLocalizer.Get(Error.NOT_DELETED, _stringLocalizer["Staff"]));
            }
        }
    }
}

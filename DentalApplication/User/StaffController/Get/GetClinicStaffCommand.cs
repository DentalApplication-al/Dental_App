using DentalApplication.Common;
using MediatR;

namespace DentalApplication.User.StaffController.Get
{
    public class GetClinicStaffCommand : CommandBase, IRequest<PaginatedResponse<ListStaff>>
    {
        public int page { get; set; }
        public int take { get; set; }
    }
}

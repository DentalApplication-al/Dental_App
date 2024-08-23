using DentalApplication.Common;
using MediatR;

namespace DentalApplication.User.StaffController.GetAll
{
    public class GetClinicStaffCommand : CommandBase, IRequest<PaginatedResponse<ListStaff>>
    {
        public int page { get; set; }
        public int take { get; set; }
        public string? search { get; set; }
    }
}

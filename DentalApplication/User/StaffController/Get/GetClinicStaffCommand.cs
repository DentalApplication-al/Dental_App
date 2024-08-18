using DentalApplication.Common;
using MediatR;

namespace DentalApplication.User.StaffController.Get
{
    public class GetClinicStaffCommand : CommandBase, IRequest<List<StaffResponse>>
    {
    }
}

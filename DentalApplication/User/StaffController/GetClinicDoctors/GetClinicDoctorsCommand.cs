using DentalApplication.Common;
using DentalApplication.User.StaffController.DTO;
using MediatR;

namespace DentalApplication.User.StaffController.GetClinicDoctors
{
    public class GetClinicDoctorsCommand : CommandBase, IRequest<List<ListStaff>>
    {
    }
}

using DentalApplication.Common;
using MediatR;

namespace DentalApplication.User.StaffController.GetById
{
    public class GetStaffByIdCommand : CommandBase, IRequest<StaffResponse>
    {
        public Guid staff_id { get; set; }
    }
}

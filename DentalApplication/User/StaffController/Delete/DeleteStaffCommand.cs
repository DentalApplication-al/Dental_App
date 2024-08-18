using MediatR;

namespace DentalApplication.User.StaffController.Delete
{
    public class DeleteStaffCommand : IRequest
    {
        public Guid staff_id { get; set; }
    }
}

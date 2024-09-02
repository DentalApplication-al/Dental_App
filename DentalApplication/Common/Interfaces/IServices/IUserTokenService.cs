namespace DentalApplication.Common.Interfaces.IServices
{
    public interface IUserTokenService
    {
        Task AddTokenAsync(Guid userId, string token);
        Task<bool> MakeTokenInvalidAsync(Guid userId);
        bool ValidateTokenAsync(Guid userId, string token);
    }
}

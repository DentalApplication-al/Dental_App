using DentalApplication.Common.Interfaces.IServices;
using System.Collections.Concurrent;

namespace DentalInfrastructure.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly ConcurrentDictionary<Guid, string> _userTokens = new ConcurrentDictionary<Guid, string>();

        // Add or update the token for the specified user ID
        public async Task AddTokenAsync(Guid userId, string token)
        {
            _userTokens.AddOrUpdate(userId, token, (key, oldValue) => token);
            await Task.CompletedTask;
        }

        // Make the token invalid by removing the entry for the specified user ID
        public async Task<bool> MakeTokenInvalidAsync(Guid userId)
        {
            return await Task.FromResult(_userTokens.TryRemove(userId, out _));
        }

        // Validate if the provided token matches the one stored for the specified user ID
        public bool ValidateTokenAsync(Guid userId, string token)
        {
            if (_userTokens.TryGetValue(userId, out var storedToken))
            {
                return true;
            }
            return false;
        }
    }
}

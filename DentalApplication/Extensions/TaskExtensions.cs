using DentalApplication.Errors;

namespace DentalApplication.Extensions
{
    public static class TaskExtensions
    {
        public static async Task EnsureSaved(this Task<bool> saveTask, string errorMessage = "Something went wrong")
        {
            if (!await saveTask)
            {
                throw new ServerError(errorMessage);
            }
        }
    }
}

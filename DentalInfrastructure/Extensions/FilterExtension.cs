using DentalDomain;

namespace DentalInfrastructure.Extensions
{
    public static class FilterExtension
    {
        public static IQueryable<T> FilterByClinic<T>(this IQueryable<T> query, Guid? clinicId) where T : BaseEntity
        {
            return query.Where(a => a.ClinicId == clinicId);
        }
    }
}

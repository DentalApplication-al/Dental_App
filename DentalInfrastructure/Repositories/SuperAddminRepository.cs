using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Users.SuperAdmin;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalInfrastructure.Repositories
{
    public class SuperAddminRepository : GenericRepository<SuperAdmin>, ISuperAdminRepository
    {
        public SuperAddminRepository(DentalContext context) : base(context)
        {
        }

        public async Task<SuperAdmin> GetSuperAdminByUsername(string username)
        {
            var superadmin = await _context.SuperAdmin.FirstOrDefaultAsync(a => a.Username == username);
            return superadmin;
        }
    }
}

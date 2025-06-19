using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRolRepository : GenericRepository<UserRol>, IUserRolRepository
    {
        protected readonly AutoTallerDbContext _context;

        public UserRolRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
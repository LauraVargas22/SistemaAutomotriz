using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class UserSpessializationRepository : GenericRepository<UserSpecialization>, IUserSpecializationRepository
    {
        protected readonly AutoTallerDbContext _context;

        public UserSpessializationRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
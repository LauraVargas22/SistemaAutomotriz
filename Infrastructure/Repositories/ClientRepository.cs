using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly AutoTallerDbContext _context;

        public ClientRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Client
                .Include(c => c.TelephoneNumbers)
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? 
                throw new KeyNotFoundException($"Client with id {id} was not found");
        }

        public async Task<Client?> GetByIdentificationAsync(string identification)
        {
            return await _context.Client
                .FirstOrDefaultAsync(c => c.Identification == identification);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Client 
                .Include(c => c.TelephoneNumbers) // ⬅️ Esto es clave
                .ToListAsync();
        }


        public override async Task<(int totalRegisters, IEnumerable<Client> registers)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.Client as IQueryable<Client>;

            if (!String.IsNullOrEmpty(search))
            {
                consulta = consulta.Where(c => EF.Functions.Like(c.Name.ToLower(), $"%{search.ToLower()}%"));
            }

            var totalRegisters = await consulta.CountAsync();

            var registers = await consulta
                .Include(c => c.Vehicles)
                .Include(c => c.TelephoneNumbers)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return (totalRegisters, registers);
        }
    }
}
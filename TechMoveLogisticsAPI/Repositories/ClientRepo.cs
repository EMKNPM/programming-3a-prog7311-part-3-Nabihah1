using PROG7311TechMoveLogisticsAPI.Data;
using TechMoveLogisticsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TechMoveLogisticsAPI.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly DataContext _context;

        public ClientRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.Include(c => c.Contracts).ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.Include(c => c.Contracts).FirstOrDefaultAsync(c => c.ClientId == id);
        }

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
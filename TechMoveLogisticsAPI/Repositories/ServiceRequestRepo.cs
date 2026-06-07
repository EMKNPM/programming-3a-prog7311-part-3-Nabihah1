using PROG7311TechMoveLogisticsAPI.Data;
using TechMoveLogisticsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TechMoveLogisticsAPI.Repositories
{
    public class ServiceRequestRepo : IServiceRequestRepo
    {
        private readonly DataContext _context;

        public ServiceRequestRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllAsync()
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Contract)
                .ToListAsync();
        }

        public async Task<ServiceRequest?> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Contract)
                .FirstOrDefaultAsync(
                    sr => sr.ServiceRequestId == id);
        }

        public async Task AddAsync(ServiceRequest request)
        {
            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceRequest request)
        {
            _context.ServiceRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var request =
                await _context.ServiceRequests
                    .FindAsync(id);

            if (request != null)
            {
                _context.ServiceRequests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}

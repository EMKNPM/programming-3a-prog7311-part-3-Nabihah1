using Microsoft.EntityFrameworkCore;
using PROG7311TechMoveLogisticsAPI.Data;
using TechMoveLogisticsAPI.Models;


namespace TechMoveLogisticsAPI.Repositories
{
    public class ContractRepo : IContractRepo
    {
        private readonly DataContext _context;

        public ContractRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAllAsync(string? status, DateTime? startDate,DateTime? endDate)
        {
            var contracts = _context.Contracts.Include(c => c.Client).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                var parsedStatus = Enum.Parse<ContractStatus>(status);
                contracts = contracts.Where(c => c.ContractStatus == parsedStatus);
            }

            if (startDate.HasValue)
            {
                contracts = contracts.Where(c => c.ContractStartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                contracts = contracts.Where(c => c.ContractEndDate <= endDate.Value);
            }
            return await contracts.ToListAsync();
        }

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Documents)
                 .Include(c => c.ServiceRequests)
                .FirstOrDefaultAsync(c => c.ContractId == id);
        }

        public async Task AddAsync(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }
    }
}



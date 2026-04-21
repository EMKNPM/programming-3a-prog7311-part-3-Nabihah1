using Microsoft.EntityFrameworkCore;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Models;
using PROG7311TechMoveLogistics.States;
using PROG7311TechMoveLogistics.Observers;


namespace PROG7311TechMoveLogistics.Services
{
    public class ContractService : IContractService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContractService(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //private method to convert enum status into a state class. easy to use 
        private IContractState ResolveState(ContractStatus status)
        {
            return status switch
            {
                ContractStatus.Draft => new DraftState(),
                ContractStatus.Active => new ActiveState(),
                ContractStatus.Expired => new ExpiredState(),
                ContractStatus.OnHold => new OnHoldState(),
                _ => new DraftState()
            };
        }

        public async Task<List<Contract>> GetAllContractsAsync(string? status, DateTime? startDate, DateTime? endDate)
        {
            var contracts = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            // Filter by status
            if (!string.IsNullOrEmpty(status))
            {
                var parsedStatus = Enum.Parse<ContractStatus>(status);
                contracts = contracts.Where(c => c.ContractStatus == parsedStatus);
            }


            // Filter by start date
            if (startDate.HasValue)
            {
                contracts = contracts.Where(c => c.ContractStartDate >= startDate.Value);
            }


            // Filter by end date
            if (endDate.HasValue)
            {
                contracts = contracts.Where(c => c.ContractEndDate <= endDate.Value);
            }

            return await contracts.ToListAsync();
        }

        public async Task<Contract?> GetContractDetailsAsync(int id)
        {
            if (id == null)
            {
                throw new Exception("This ID cannot be found");
            }

            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.Documents)
                .FirstOrDefaultAsync(c => c.ContractId == id);

            return contracts;
        }


        public async Task CreateContractAsync(ContractFormViewModel viewModel)
        {
            //check data validation before creating a contract 
            //implements FACTORY desidn patterns
            if (viewModel.ContractEndDate <= viewModel.ContractStartDate)
            {
                throw new Exception("End date must be after start date.");
               

            }

            //create contract entity 
            var contract = ContractFactory.Create(viewModel);

            contract.Subscribe(new SmsNotifier());
            contract.Subscribe(new WhatsAppNotifier());
            // when the contract status changes both observers are notified 
            var state = ResolveState(viewModel.ContractStatus);
            contract.SetState(state);

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            // handle file upload
            if (viewModel.SignedDocument != null)
            {

                var validator = new FileValidations();
                validator.ValidatePdfFile(viewModel.SignedDocument.FileName);

                var folder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/contracts");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(viewModel.SignedDocument.FileName);
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.SignedDocument.CopyToAsync(stream);
                }

                var document = new UploadedDocument
                {
                    FileName = viewModel.SignedDocument.FileName,
                    FilePath = "/uploads/contracts/" + fileName,
                    FileSize = viewModel.SignedDocument.Length,
                    UploadedDate = DateTime.Now,
                    ContractId = contract.ContractId
                };

                _context.UploadedDocuments.Add(document);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Contract?> GetContractByIdAsync(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public async Task UpdateContractAsync(Contract updatedContract)
        {
            var existingContract = await _context.Contracts.FindAsync(updatedContract.ContractId);

            if (existingContract == null)
            {
                throw new Exception("Contract not found.");
            }

            if (updatedContract.ContractEndDate <= updatedContract.ContractStartDate)
            {
                throw new Exception("End date must be after start date.");
            }

            existingContract.ContractStartDate = updatedContract.ContractStartDate;
            existingContract.ContractEndDate = updatedContract.ContractEndDate;
            existingContract.ContractServiceLevel = updatedContract.ContractServiceLevel;
            existingContract.ClientId = updatedContract.ClientId;

            existingContract.Subscribe(new SmsNotifier());
            existingContract.Subscribe(new WhatsAppNotifier());

            var state = ResolveState(updatedContract.ContractStatus);
            existingContract.SetState(state);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContractAsync(int id)
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
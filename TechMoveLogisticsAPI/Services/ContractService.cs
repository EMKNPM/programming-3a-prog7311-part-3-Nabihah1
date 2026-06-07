using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Factories;
using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Observers;
using TechMoveLogisticsAPI.Repositories;
using TechMoveLogisticsAPI.States;
using TechMoveLogisticsAPI.Services;

namespace TechMoveLogisticsAPI.Services
{
    public class ContractService : IContractService
    {

        //constructor 
        private readonly IContractRepo _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ContractService(
            IContractRepo repository,
            IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }


        //private method to convert enum status into a state class -> easy to use 
        private IContractState ResolveState(ContractStatusDto status)
        {
            return status switch
            {
                ContractStatusDto.Draft => new DraftState(),
                ContractStatusDto.Active => new ActiveState(),
                ContractStatusDto.Expired => new ExpiredState(),
                ContractStatusDto.OnHold => new OnHoldState(),
                _ => new DraftState()
            };
        }

        public async Task<List<Contract>> GetAllContractsAsync(string? status, DateTime? startDate, DateTime? endDate)
        {
            return await _repository.GetAllAsync(status, startDate, endDate);
        }

        public async Task<Contract?> GetContractByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        //public async Task<int> CreateContractAsync(CreateContractDto dto)
        //{
        //    var contract = ContractFactory.Create(dto);

        //    contract.Subscribe(new SmsNotifier());
        //    contract.Subscribe(new WhatsAppNotifier());

        //    var state = ResolveState(dto.ContractStatus);
        //    contract.SetState(state);

        //    await _repository.AddAsync(contract);

        //    return contract.ContractId;
        //}

        public async Task<int> CreateContractAsync(CreateContractDto dto)
        {
            var contract = ContractFactory.Create(dto);

            contract.Subscribe(new SmsNotifier());
            contract.Subscribe(new WhatsAppNotifier());

            var state = ResolveState(dto.ContractStatus);
            contract.SetState(state);

            //if (dto.SignedDocument != null)
            //{
            //    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    var fileName = Guid.NewGuid() + Path.GetExtension(dto.SignedDocument.FileName);
            //    var filePath = Path.Combine(uploadsFolder, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await dto.SignedDocument.CopyToAsync(stream);
            //    }

            //    contract.Documents.Add(new UploadedDocument
            //    {
            //        FileName = dto.SignedDocument.FileName,
            //        FilePath = filePath,
            //        FileSize = dto.SignedDocument.Length,
            //        UploadedDate = DateTime.Now,
            //        IsEncrypted = false,
            //        Contract = contract
            //    });
            //}


            await _repository.AddAsync(contract);

            return contract.ContractId;
        }

        public async Task UpdateContractAsync(Contract updatedContract)
        {
            var existingContract =   await _repository.GetByIdAsync( updatedContract.ContractId);

            if (existingContract == null)
            {
                throw new Exception(  "Contract not found.");
            }


            existingContract.ContractStartDate = updatedContract.ContractStartDate;

            existingContract.ContractEndDate = updatedContract.ContractEndDate;

            existingContract.ContractServiceLevel =  updatedContract.ContractServiceLevel;

            existingContract.ClientId =  updatedContract.ClientId;


            existingContract.Subscribe(new SmsNotifier());

            existingContract.Subscribe(new WhatsAppNotifier());

            var state = ResolveState((ContractStatusDto)updatedContract.ContractStatus);

            existingContract.SetState(state);

            await _repository.UpdateAsync( existingContract);
        }

        public async Task DeleteContractAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


    }
}

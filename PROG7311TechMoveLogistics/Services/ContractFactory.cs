using PROG7311TechMoveLogistics.Models;
using TechMoveLogisticsAPI.DTOs;

namespace PROG7311TechMoveLogistics.Services
{
    public class ContractFactory
    {
        public static CreateContractDto Create(ContractFormViewModel model)
        {
            return new CreateContractDto
            {
                ClientId = model.ClientId,
                ContractStartDate = model.ContractStartDate ?? DateTime.Now,
                ContractEndDate = model.ContractEndDate ?? DateTime.Now,

                ContractStatus = model.ContractStatus switch
                {
                    ContractStatus.Draft => ContractStatusDto.Draft,
                    ContractStatus.Active => ContractStatusDto.Active,
                    ContractStatus.Expired => ContractStatusDto.Expired,
                    ContractStatus.OnHold => ContractStatusDto.OnHold,
                    _ => ContractStatusDto.Draft
                },

                ContractServiceLevel = model.ContractServiceLevel
            };
        }
    }
}
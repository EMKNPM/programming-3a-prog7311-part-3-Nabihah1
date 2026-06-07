using TechMoveLogisticsAPI.DTOs;
using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Factories
{
    public class ContractFactory
    {

        // created class to folloe factory design pattern rules 
        // linked to the contarct controller (post create)
        public static Contract Create(CreateContractDto model)
        {
            //factory creates new objects 
            return new Contract
            {
                ClientId = model.ClientId,
                ContractStartDate = model.ContractStartDate,
                ContractEndDate = model.ContractEndDate,
                ContractStatus = (ContractStatus)model.ContractStatus,
                ContractServiceLevel = model.ContractServiceLevel ?? "N/A"

            };
        }
    

    }
}

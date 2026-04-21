using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.Services
{
    public class ContractFactory
    {
        // created class to folloe factory design pattern rules 
        // linked to the contarct controller (post create)
        public static Contract Create(ContractFormViewModel model)
        {
            //factory creates new objects 
            return new Contract
            {
                ClientId = model.ClientId,
                ContractStartDate = model.ContractStartDate,
                ContractEndDate = model.ContractEndDate,
                ContractStatus = model.ContractStatus,
                ContractServiceLevel = model.ContractServiceLevel

            };
        }
    }
}
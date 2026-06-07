using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.States
{
    public class ExpiredState : IContractState
    {

        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.Expired;
        }

    }
}

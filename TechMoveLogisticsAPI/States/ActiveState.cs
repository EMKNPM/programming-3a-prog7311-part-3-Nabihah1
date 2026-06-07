using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.States
{
    public class ActiveState : IContractState
    {
        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.Active;
        }

    }
}

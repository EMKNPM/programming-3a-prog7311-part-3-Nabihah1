using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.States
{
    public class OnHoldState : IContractState
    {

        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.OnHold;
        }

    }
}

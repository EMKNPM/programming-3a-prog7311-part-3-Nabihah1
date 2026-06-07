using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.States
{
    public class DraftState : IContractState
    {

        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.Draft;
        }


    }
}

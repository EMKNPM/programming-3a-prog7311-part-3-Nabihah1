using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.States
{
    public class ActiveState : IContractState
    {
        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.Active;
        }

    }
}


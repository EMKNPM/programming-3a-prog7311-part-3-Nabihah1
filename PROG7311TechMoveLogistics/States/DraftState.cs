using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.States
{
    public class DraftState : IContractState
    {

        public void Handle(Contract contract)
        {
            contract.ContractStatus = ContractStatus.Draft;
        }


    }
}

using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.States
{
    public interface IContractState
    {
        void Handle(Contract contarct);

    }
}

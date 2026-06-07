using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.States
{
    public interface IContractState
    {
        void Handle(Contract contarct);

    }
}

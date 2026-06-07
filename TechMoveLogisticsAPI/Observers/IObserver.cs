using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Observers
{
    public interface IObserver
    {
        void Update(Contract contract);

    }
}

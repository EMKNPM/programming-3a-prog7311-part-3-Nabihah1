using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Observers
{
    public class SmsNotifier : IObserver
    {

        public void Update(Contract contract)
        {
            Console.WriteLine($"SMS Notification: Contract #{contract.ContractId} is now {contract.ContractStatus}");
        }

    }
}

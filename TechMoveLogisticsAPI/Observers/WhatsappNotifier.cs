using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Observers
{
    public class WhatsAppNotifier : IObserver
    {

        public void Update(Contract contract)
        {
            Console.WriteLine($"WhatsApp Notification: Contract #{contract.ContractId} is now {contract.ContractStatus}");
        }
    }
}

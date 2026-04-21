using PROG7311TechMoveLogistics.Models; 


namespace PROG7311TechMoveLogistics.Observers
{
    public class WhatsAppNotifier : IObserver
    {

        public void Update(Contract contract)
        {
            Console.WriteLine($"WhatsApp Notification: Contract #{contract.ContractId} is now {contract.ContractStatus}");
        }

    }
}

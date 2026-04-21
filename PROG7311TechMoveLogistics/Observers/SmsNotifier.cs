using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.Observers
{
    public class SmsNotifier : IObserver
    {

        public void Update(Contract contract)
        {
            Console.WriteLine($"SMS Notification: Contract #{contract.ContractId} is now {contract.ContractStatus}");
        }

    }
}

using System.ComponentModel.DataAnnotations;
using PROG7311TechMoveLogistics.Observers;
using PROG7311TechMoveLogistics.States;


namespace PROG7311TechMoveLogistics.Models
{
    public class Contract
    {
        public int ContractId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ContractStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ContractEndDate { get; set; }

        public ContractStatus ContractStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string ContractServiceLevel { get; set; } = string.Empty;


        // FOREIGN KEY
        public int ClientId { get; set; }


        //navigation properties 

        // 1 contract can only belong to 1 client 
        public Client? Client { get; set; }

        //1 contract can have many service requests 
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        // 1 contract can have manyyy documents
        public List<UploadedDocument> Documents { get; set; } = new List<UploadedDocument>();


        //implementing state design pattern 
        //does not need migration because its 'private
        private IContractState? _state;  //used for state and observer 


        //implementing observer design pattern 
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnSubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }

        }

        public void SetState(IContractState state)
        {
            _state = state;
            _state.Handle(this);
            //anytime the status changes, it automatically triggers a notification
            Notify();
        }



    }
}
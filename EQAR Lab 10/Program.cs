using System;
using System.Collections.Generic;

namespace FlowerDelivery
{

    public abstract class Component
    {
        protected IMediator mediator;

        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected void MediatorNotification(string StatusCode)
        {
            mediator?.Notify(this, StatusCode);
        }
    }

 
    public interface IMediator
    {
        void Notify(Component sender, string StatusCode);
    }

    public class DatePicker : Component
    {
        private string selectedDate;

        public void SelectDate(string date)
        {
            selectedDate = date;
            MediatorNotification("201 DateChanged");
        }

        public string GetSelectedDate()
        {
            return selectedDate;
        }
    }

    public class TimePicker : Component
    {
        private List<string> availableTime = new List<string>();

        public void AvailableTime(List<string> time)
        {
            availableTime = time;
            Console.WriteLine("Availible Time Gaps --> " + string.Join(" | ", availableTime));
        }

        public List<string> GetAvailableTime()
        {
            return availableTime;
        }
    }

    public class ReceiverInfo : Component
    {
        private bool isOtherPerson;
        private string name;
        private string phone;

        public void SetOtherPerson(bool otherPerson)
        {
            isOtherPerson = otherPerson;
            MediatorNotification("202 ReceiverChanged");
        }

        public bool IsOtherPerson()
        {
            return isOtherPerson;
        }

        public void SetName(string name)
        {
            if (isOtherPerson)
            {
                this.name = name;
            }
        }

        public void SetPhone(string phone)
        {
            if (isOtherPerson)
            {
                this.phone = phone;
            }
        }
    }


    public class PickupOption : Component
    {
        private bool isPickup;

        public void SetPickup(bool pickup)
        {
            isPickup = pickup;
            MediatorNotification("203 PickupChanged");
        }

        public bool IsPickup()
        {
            return isPickup;
        }
    }


    public class OrderMediator : IMediator
    {
        private DatePicker DatePicker;
        private TimePicker TimePicker;
        private ReceiverInfo ReceiverInfo;
        private PickupOption PickupOption;

        public OrderMediator(DatePicker datePicker, TimePicker timePicker, ReceiverInfo receiverInfo, PickupOption pickupOption)
        {
            this.DatePicker = datePicker;
            this.TimePicker = timePicker;
            this.ReceiverInfo = receiverInfo;
            this.PickupOption = pickupOption;

            this.DatePicker.SetMediator(this);
            this.TimePicker.SetMediator(this);
            this.ReceiverInfo.SetMediator(this);
            this.PickupOption.SetMediator(this);
        }

        public void Notify(Component sender, string StatusCode)
        {
            switch (StatusCode)
            {
                case "201 DateChanged":
                    UpdateAvailableTime();
                    break;
                case "202 ReceiverChanged":
                    CheckReceiverInfo();
                    break;
                case "203 PickupChanged":
                    ToggleDeliveryOptions();
                    break;
            }
        }

        private void UpdateAvailableTime()
        {
            string selectedDate = DatePicker.GetSelectedDate();
            List<string> time = new List<string>();

            if (selectedDate == "2024-11-11")
            {
                time.Add("12:30 - 14:45");
                time.Add("15:00 - 16:00");
                time.Add("19:00 - 20:40");
            }
            else if (selectedDate == "2024-11-12")
            {
                time.Add("13:30 - 15:45");
                time.Add("17:00 - 18:00");
                time.Add("20:00 - 21:40");
            }

            TimePicker.AvailableTime(time);
        }

        private void CheckReceiverInfo()
        {
            if (ReceiverInfo.IsOtherPerson())
            {
                Console.WriteLine("The recipient is <another person> --> The fields <Name> and <Phone> are required");
            }
            else
            {
                Console.WriteLine("The recipient is the <customer himself> --> No additional information required.");
            }
        }

        private void ToggleDeliveryOptions()
        {
            if (PickupOption.IsPickup())
            {
                Console.WriteLine("Pickup --> Delivery options are not required.");
            }
            else
            {
                Console.WriteLine("Delivery --> Delivery options are required.");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var DatePicker = new DatePicker();
            var TimePicker = new TimePicker();
            var ReceiverInfo = new ReceiverInfo();
            var PickupOption = new PickupOption();

            var mediator = new OrderMediator(DatePicker, TimePicker, ReceiverInfo, PickupOption);

            DatePicker.SelectDate("2024-11-12");
            ReceiverInfo.SetOtherPerson(false);
            PickupOption.SetPickup(false);
        }
    }
}

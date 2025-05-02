using System;

namespace CourierManagementEntityLibrary
{
    public class Courier
    {
        private static int trackingSeed = 1000; // Seed to generate tracking numbers

        public int CourierID { get; set; } // INT in SQL
        public string SenderName { get; set; } // VARCHAR(255)
        public string SenderAddress { get; set; } // VARCHAR(MAX)
        public string ReceiverName { get; set; } // VARCHAR(255)
        public string ReceiverAddress { get; set; } // VARCHAR(MAX)
        public decimal Weight { get; set; } // DECIMAL(5, 2)
        public string Status { get; set; } // VARCHAR(50)
        public string TrackingNumber { get; set; } // VARCHAR(20), Unique
        public DateTime DeliveryDate { get; set; } // DATE

        // Constructor
        public Courier()
        {
            CourierID = 0;
            SenderName = "";
            SenderAddress = "";
            ReceiverName = "";
            ReceiverAddress = "";
            Weight = 0.0m;
            Status = "YetToTransit";
            TrackingNumber = "TRK" + trackingSeed++;
            DeliveryDate = DateTime.Now.AddDays(5); // Example default: 5 days from now
        }

        // Overloaded Constructor
        public Courier(int courierID, string senderName, string senderAddress, string receiverName,
                       string receiverAddress, decimal weight, string status, DateTime deliveryDate)
        {
            CourierID = courierID;
            SenderName = senderName;
            SenderAddress = senderAddress;
            ReceiverName = receiverName;
            ReceiverAddress = receiverAddress;
            Weight = weight;
            Status = status;
            TrackingNumber = "TRK" + trackingSeed++;
            DeliveryDate = deliveryDate;
        }

        // Display Info
        public void PrintCourierInfo()
        {
            Console.WriteLine($"Courier ID: {CourierID}");
            Console.WriteLine($"Sender Name: {SenderName}");
            Console.WriteLine($"Sender Address: {SenderAddress}");
            Console.WriteLine($"Receiver Name: {ReceiverName}");
            Console.WriteLine($"Receiver Address: {ReceiverAddress}");
            Console.WriteLine($"Weight: {Weight}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Tracking Number: {TrackingNumber}");
            Console.WriteLine($"Delivery Date: {DeliveryDate.ToShortDateString()}");
        }
    }
}

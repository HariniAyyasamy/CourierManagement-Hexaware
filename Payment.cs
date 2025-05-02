using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementEntityLibrary
{
    public class Payment
    {
        public long PaymentID { get; set; }
        public long CourierID { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Default Constructor
        public Payment()
        {
            // Initialize default values
            PaymentID = 0;
            CourierID = 0;
            Amount = 0.0;
            PaymentDate = DateTime.MinValue;
        }

        // Overloaded Constructor
        public Payment(long paymentID, long courierID, double amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            CourierID = courierID;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public void PrintPaymentInfo()
        {
            Console.WriteLine($"Payment ID: {PaymentID}");
            Console.WriteLine($"Courier ID: {CourierID}");
            Console.WriteLine($"Amount: {Amount}");
            Console.WriteLine($"Payment Date: {PaymentDate}");
        }
    }
}
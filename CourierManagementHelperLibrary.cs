using System;
using System.Collections.Generic;
using CourierManagementEntityLibrary;
using CourierManagementDaoLibrary;

namespace CourierManagementHelperLibrary
{
    public class CourierServiceHelper
    {
        private readonly ICourierAdminService adminService;
        private readonly ICourierUserService userService;

        public CourierServiceHelper()
        {
            adminService = new CourierAdminService(); // Keep admin reference
            userService = new CourierUserService();   // Use only this for user methods
        }

        
        public Employees AddCourierEmployee(int employeeId, string name, string email, string contactNumber, string role, decimal salary)
        {
            try
            {
                return adminService.AddCourierEmployee(employeeId, name, email, contactNumber, role, salary);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding courier employee: " + ex.Message);
            }
        }

        public List<Employees> GetAllEmployees()
        {
            try
            {
                return adminService.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees: " + ex.Message);
            }
        }

        // -----------------------------------
        // ✅ USER METHODS
        // -----------------------------------
        public string PlaceOrder(Courier courierObj)
        {
            try
            {
                return userService.PlaceOrder(courierObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error placing order: " + ex.Message);
            }
        }

        public string GetOrderStatus(string trackingNumber)
        {
            try
            {
                return userService.GetOrderStatus(trackingNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order status: " + ex.Message);
            }
        }

        public bool CancelOrder(string trackingNumber)
        {
            try
            {
                return userService.CancelOrder(trackingNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Error canceling order: " + ex.Message);
            }
        }

        public List<Courier> GetAssignedOrders(string courierStaffId)
        {
            try
            {
                return userService.GetAssignedOrders(courierStaffId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving assigned orders: " + ex.Message);
            }
        }

        // -----------------------------------
        // ✅ USER INTERACTIVE METHODS
        // -----------------------------------
        public void PlaceOrderInteractive()
        {
            try
            {
                Console.Write("Enter Courier ID (unique): ");
                int courierId = int.Parse(Console.ReadLine());

                Console.Write("Enter Sender Name: ");
                string senderName = Console.ReadLine();

                Console.Write("Enter Sender Address: ");
                string senderAddress = Console.ReadLine();

                Console.Write("Enter Receiver Name: ");
                string receiverName = Console.ReadLine();

                Console.Write("Enter Receiver Address: ");
                string receiverAddress = Console.ReadLine();

                Console.Write("Enter Weight (in kg): ");
                decimal weight = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Tracking Number: ");
                string trackingNumber = Console.ReadLine();

                Console.Write("Enter Delivery Date (yyyy-MM-dd): ");
                DateTime deliveryDate = DateTime.Parse(Console.ReadLine());

                Courier newCourier = new Courier
                {
                    CourierID = courierId,
                    SenderName = senderName,
                    SenderAddress = senderAddress,
                    ReceiverName = receiverName,
                    ReceiverAddress = receiverAddress,
                    Weight = weight,
                    Status = "Pending",
                    TrackingNumber = trackingNumber,
                    DeliveryDate = deliveryDate
                };

                string result = userService.PlaceOrder(newCourier);

                if (!string.IsNullOrEmpty(result))
                    Console.WriteLine("✅ Order placed successfully! Tracking Number: " + result);
                else
                    Console.WriteLine("❌ Failed to place the order.");
            }
            catch (FormatException)
            {
                Console.WriteLine("⚠️ Invalid input format. Please enter data correctly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error placing order: " + ex.Message);
            }
        }

        public void TrackOrderInteractive()
        {
            try
            {
                Console.Write("Enter Tracking Number: ");
                string trackingNumber = Console.ReadLine();

                string status = userService.GetOrderStatus(trackingNumber);

                if (!string.IsNullOrEmpty(status))
                    Console.WriteLine("📦 Current Status: " + status);
                else
                    Console.WriteLine("❌ Order not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error tracking order: " + ex.Message);
            }
        }

        public void CancelOrderInteractive()
        {
            try
            {
                Console.Write("Enter Tracking Number to Cancel: ");
                string trackingNumber = Console.ReadLine();

                bool success = userService.CancelOrder(trackingNumber);

                if (success)
                    Console.WriteLine("✅ Order canceled successfully.");
                else
                    Console.WriteLine("❌ Cancellation failed or order not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error canceling order: " + ex.Message);
            }
        }

        public void ViewAssignedOrdersInteractive()
        {
            try
            {
                Console.Write("Enter Courier Staff ID: ");
                string staffId = Console.ReadLine();

                List<Courier> orders = userService.GetAssignedOrders(staffId);

                if (orders.Count > 0)
                {
                    Console.WriteLine($"📦 Assigned Orders for Staff ID {staffId}:");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Tracking: {order.TrackingNumber}, Sender: {order.SenderName}, Receiver: {order.ReceiverName}, Status: {order.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("❌ No assigned orders found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error retrieving assigned orders: " + ex.Message);
            }
        }
    }
}

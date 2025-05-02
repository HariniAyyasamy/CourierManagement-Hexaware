using System;
using CourierManagementHelperLibrary;
using CourierManagementEntityLibrary;
using System.Collections.Generic;

namespace CourierManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            CourierServiceHelper helper = new CourierServiceHelper();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Courier Management System ===");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        AdminMenu(helper);
                        break;

                    case "2":
                        CustomerMenu(helper);
                        break;

                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }
        }

        static void AdminMenu(CourierServiceHelper helper)
        {
            bool adminExit = false;
            while (!adminExit)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Courier Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Enter your choice: ");
                string adminChoice = Console.ReadLine();

                switch (adminChoice)
                {
                    case "1":
                        Console.Write("Enter Employee ID: ");
                        string input = Console.ReadLine();
                        int employeeId;
                        while (!int.TryParse(input, out employeeId) || employeeId <= 0)
                        {
                            Console.Write("Invalid ID. Please enter a positive number: ");
                            input = Console.ReadLine();
                        }

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter Contact Number: ");
                        string contactNumber = Console.ReadLine();

                        Console.Write("Enter Role: ");
                        string role = Console.ReadLine();

                        Console.Write("Enter Salary: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal salary))
                        {
                            try
                            {
                                Employees newEmployee = helper.AddCourierEmployee(employeeId, name, email, contactNumber, role, salary);
                                Console.WriteLine("\nEmployee added successfully!");
                                Console.WriteLine($"Employee ID: {newEmployee.EmployeeID}");
                                Console.WriteLine($"Name: {newEmployee.EmployeeName}");
                                Console.WriteLine($"Email: {newEmployee.Email}");
                                Console.WriteLine($"Contact Number: {newEmployee.ContactNumber}");
                                Console.WriteLine($"Role: {newEmployee.Role}");
                                Console.WriteLine($"Salary: {newEmployee.Salary:C}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An error occurred while adding the employee. Details: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid salary input.");
                        }
                        break;

                    case "2":
                        try
                        {
                            List<Employees> employees = helper.GetAllEmployees();
                            if (employees.Count == 0)
                            {
                                Console.WriteLine("No employees found.");
                            }
                            else
                            {
                                Console.WriteLine("\n============================== EMPLOYEE LIST ==============================\n");
                                Console.WriteLine("+------------+----------------------+----------------------------------+------------------+--------------+------------+");
                                Console.WriteLine("| EmployeeID | Name                 | Email                            | Contact Number   | Role         | Salary     |");
                                Console.WriteLine("+------------+----------------------+----------------------------------+------------------+--------------+------------+");

                                foreach (var emp in employees)
                                {
                                    Console.WriteLine($"| {emp.EmployeeID,-10} | {emp.EmployeeName,-20} | {emp.Email,-30} | {emp.ContactNumber,-16} | {emp.Role,-12} | {emp.Salary,10:F2} |");
                                }

                                Console.WriteLine("+------------+----------------------+----------------------------------+------------------+--------------+------------+");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred while fetching the employee list.");
                            Console.WriteLine("Details: " + ex.Message);
                        }
                        break;

                    case "3":
                        adminExit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }
        }

        static void CustomerMenu(CourierServiceHelper helper)
        {
            while (true)
            {
                Console.WriteLine("\n--- CUSTOMER MENU ---");
                Console.WriteLine("1. Place Order");
                Console.WriteLine("2. Get Order Status");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Get Assigned Orders");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        helper.PlaceOrderInteractive(); // Call helper method
                        break;

                    case 2:
                        Console.Write("Enter Tracking Number: ");
                        string trackingNo = Console.ReadLine();
                        string status = helper.GetOrderStatus(trackingNo);
                        Console.WriteLine($"Order Status: {status}");
                        break;

                    case 3:
                        Console.Write("Enter Tracking Number to Cancel: ");
                        string cancelTrackingNo = Console.ReadLine();
                        bool isCancelled = helper.CancelOrder(cancelTrackingNo);
                        Console.WriteLine(isCancelled
                            ? "Order cancelled successfully."
                            : "Cancellation failed. Check tracking number.");
                        break;

                    case 4:
                        Console.Write("Enter Courier Staff ID: ");
                        string staffId = Console.ReadLine();
                        List<Courier> assignedOrders = helper.GetAssignedOrders(staffId);

                        if (assignedOrders.Count == 0)
                        {
                            Console.WriteLine("No assigned orders found.");
                        }
                        else
                        {
                            Console.WriteLine("\n+---------------------+-------------------+--------------------+");
                            Console.WriteLine("|   Tracking Number   |      Status       |   Delivery Date    |");
                            Console.WriteLine("+---------------------+-------------------+--------------------+");

                            foreach (var o in assignedOrders)
                            {
                                Console.WriteLine($"| {o.TrackingNumber,-19} | {o.Status,-17} | {o.DeliveryDate:dd-MM-yyyy,-18} |");
                            }

                            Console.WriteLine("+---------------------+-------------------+--------------------+");
                        }
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}

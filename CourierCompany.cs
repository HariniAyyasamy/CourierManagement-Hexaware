using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementEntityLibrary
{
    public class CourierCompany
    {
        public string CompanyName { get; set; }
        public List<Courier> CourierDetails { get; set; }
        public List<Employees> EmployeeDetails { get; set; }
        public List<Location> LocationDetails { get; set; }

        // Default Constructor
        public CourierCompany()
        {
            CompanyName = "";
            CourierDetails = new List<Courier>();
            EmployeeDetails = new List<Employees>();
            LocationDetails = new List<Location>();
        }

        // Overloaded Constructor
        public CourierCompany(string companyName)
        {
            CompanyName = companyName;
            CourierDetails = new List<Courier>();
            EmployeeDetails = new List<Employees>();
            LocationDetails = new List<Location>();
        }

        public void PrintCompanyInfo()
        {
            Console.WriteLine($"Company Name: {CompanyName}");

            Console.WriteLine("\nCourier Details:");
            foreach (var courier in CourierDetails)
            {
                courier.PrintCourierInfo();
                Console.WriteLine();
            }

            Console.WriteLine("\nEmployee Details:");
            foreach (var employee in EmployeeDetails)
            {
                employee.PrintEmployeeInfo();
                Console.WriteLine();
            }

            Console.WriteLine("\nLocation Details:");
            foreach (var location in LocationDetails)
            {
                location.PrintLocationInfo();
                Console.WriteLine();
            }
        }
    }
}
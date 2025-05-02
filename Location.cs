using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementEntityLibrary
{
    public class Location
    {
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }

        // Default Constructor
        public Location()
        {
            // Initialize default values
            LocationID = "";
            LocationName = "";
            Address = "";
        }

        // Overloaded Constructor
        public Location(string locationID, string locationName, string address)
        {
            LocationID = locationID;
            LocationName = locationName;
            Address = address;
        }

        public void PrintLocationInfo()
        {
            Console.WriteLine($"Location ID: {LocationID}");
            Console.WriteLine($"Location Name: {LocationName}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
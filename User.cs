namespace CourierManagementEntityLibrary
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        // Default Constructor
        public User()
        {
            // Initialize default values
            UserID = "";
            UserName = "";
            Email = "";
            Password = "";
            ContactNumber = "";
            Address = "";
        }

        // Overloaded Constructor
        public User(string userID, string userName, string email, string password, string contactNumber, string address)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            Password = password;
            ContactNumber = contactNumber;
            Address = address;
        }

        public void PrintUserInfo()
        {
            Console.WriteLine($"User ID: {UserID}");
            Console.WriteLine($"Username: {UserName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Password: {Password}");
            Console.WriteLine($"Contact Number: {ContactNumber}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
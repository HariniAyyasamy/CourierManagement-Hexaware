using System;
using System.Collections.Generic;
using CourierManagementEntityLibrary;
using Microsoft.Data.SqlClient;

namespace CourierManagementDaoLibrary
{
    public class CourierAdminService : ICourierAdminService
    {
        private readonly string connectionString = "server=DESKTOP-0N7CP4U\\SQLEXPRESS;database=couriermanagement;integrated security=true;trust server certificate=true";

        public Employees AddCourierEmployee(int employeeId, string name, string email, string contactNumber, string role, decimal salary)
        {
            Console.WriteLine($"[DEBUG] employeeId={employeeId}, name={name}, email={email}, contactNumber={contactNumber}, role={role}, salary={salary}");

            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be greater than 0.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (string.IsNullOrWhiteSpace(contactNumber))
                throw new ArgumentException("Contact Number cannot be empty.");
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty.");


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if employee ID or email already exists
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID OR Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        Console.WriteLine("Employee with this ID or Email already exists.");
                        return null;
                    }

                    string query = @"INSERT INTO Employees (EmployeeID, Name, Email, ContactNumber, Role, Salary)
                                     VALUES (@EmployeeID, @Name, @Email, @ContactNumber, @Role, @Salary)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@Salary", salary);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new Employees
                        {
                            EmployeeID = employeeId.ToString(),
                            EmployeeName = name,
                            Email = email,
                            ContactNumber = contactNumber,
                            Role = role,
                            Salary = (double)salary
                        };
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert employee.");
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
                return null;
            }
        }

        public List<Employees> GetAllEmployees()
        {
            List<Employees> employees = new List<Employees>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Employees";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employees emp = new Employees
                        {
                            EmployeeID = reader["EmployeeID"].ToString(),
                            EmployeeName = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            ContactNumber = reader["ContactNumber"]?.ToString(),
                            Role = reader["Role"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"])
                        };

                        employees.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while retrieving employees: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while retrieving employees: " + ex.Message);
            }

            return employees;
        }
    }
}

using System;
using System.Collections.Generic;
using CourierManagementEntityLibrary;
using Microsoft.Data.SqlClient;

namespace CourierManagementDaoLibrary
{
    public class CourierUserService : ICourierUserService
    {
        private readonly string connectionString = "server=DESKTOP-0N7CP4U\\SQLEXPRESS;database=couriermanagement;integrated security=true;trust server certificate=true";

        // 1. PlaceOrder
        public string PlaceOrder(Courier courierObj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Couriers 
                                    (CourierID, SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, DeliveryDate)
                                    VALUES (@CourierID, @SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, @Weight, @Status, @TrackingNumber, @DeliveryDate)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CourierID", courierObj.CourierID);
                    cmd.Parameters.AddWithValue("@SenderName", courierObj.SenderName);
                    cmd.Parameters.AddWithValue("@SenderAddress", courierObj.SenderAddress);
                    cmd.Parameters.AddWithValue("@ReceiverName", courierObj.ReceiverName);
                    cmd.Parameters.AddWithValue("@ReceiverAddress", courierObj.ReceiverAddress);
                    cmd.Parameters.AddWithValue("@Weight", courierObj.Weight);
                    cmd.Parameters.AddWithValue("@Status", courierObj.Status);
                    cmd.Parameters.AddWithValue("@TrackingNumber", courierObj.TrackingNumber);
                    cmd.Parameters.AddWithValue("@DeliveryDate", courierObj.DeliveryDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return courierObj.TrackingNumber;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while placing order: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred while placing order: " + ex.Message);
                return null;
            }
        }

        // 2. GetOrderStatus
        public string GetOrderStatus(string trackingNumber)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Status FROM Couriers WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    conn.Open();
                    var result = cmd.ExecuteScalar();

                    return result != null ? result.ToString() : "Tracking number not found";
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while getting order status: " + ex.Message);
                return "Error retrieving status";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while getting order status: " + ex.Message);
                return "Error retrieving status";
            }
        }

        // 3. CancelOrder
        public bool CancelOrder(string trackingNumber)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Couriers WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while cancelling order: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while cancelling order: " + ex.Message);
                return false;
            }
        }

        // 4. GetAssignedOrders (dummy method - real relationship missing in schema)
        public List<Courier> GetAssignedOrders(string courierStaffId)
        {
            List<Courier> orders = new List<Courier>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Couriers WHERE Status = 'Assigned'"; // Assuming assigned orders by status
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Courier c = new Courier
                        {
                            CourierID = Convert.ToInt32(reader["CourierID"]),
                            SenderName = reader["SenderName"].ToString(),
                            SenderAddress = reader["SenderAddress"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            ReceiverAddress = reader["ReceiverAddress"].ToString(),
                            Weight = Convert.ToDecimal(reader["Weight"]),
                            Status = reader["Status"].ToString(),
                            TrackingNumber = reader["TrackingNumber"].ToString(),
                            DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"])
                        };

                        orders.Add(c);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while fetching assigned orders: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while fetching assigned orders: " + ex.Message);
            }

            return orders;
        }
    }
}

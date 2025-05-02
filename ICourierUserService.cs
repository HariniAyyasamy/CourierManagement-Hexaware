using System.Collections.Generic;
using CourierManagementEntityLibrary;

namespace CourierManagementDaoLibrary
{
    public interface ICourierUserService
    {
        string PlaceOrder(Courier courierObj);
        string GetOrderStatus(string trackingNumber);
        bool CancelOrder(string trackingNumber);
        List<Courier> GetAssignedOrders(string courierStaffId);
    }
}
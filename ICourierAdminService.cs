using System;
using System.Collections.Generic;
using CourierManagementEntityLibrary;

namespace CourierManagementDaoLibrary
{
    public interface ICourierAdminService
    {
        Employees AddCourierEmployee(int employeeId,string name, string email, string contactNumber, string role, decimal salary);

        List<Employees> GetAllEmployees();
    }

}
using CoreMongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoDB.IRepository
{
    public interface IEmployeeRepository
    {
        Employee Save(Employee employee);
        Employee Get(string employeeId);
        List<Employee> Gets();
        string Delete(string employeeId);
    }
}

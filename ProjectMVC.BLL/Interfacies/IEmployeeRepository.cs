using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.BLL.Interfacies
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        //Employee
        IQueryable<Employee> GetEmployeeByAddress(string address);

        IQueryable<Employee> GetEmployeeByName(string name);
        


    }
}

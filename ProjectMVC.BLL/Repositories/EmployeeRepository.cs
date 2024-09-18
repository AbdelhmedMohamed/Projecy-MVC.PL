using Microsoft.EntityFrameworkCore;
using ProjectMVC.BLL.Interfacies;
using ProjectMVC.DAL.Data;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        //private readonly AppDbContext _dbcontext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext) 
        {
            //_dbcontext = dbContext;
        }

        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbcontext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        }
    }
}

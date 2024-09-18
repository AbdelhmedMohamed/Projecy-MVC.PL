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
    public class DepartementRepository : GenericRepository<Department> , IDepartementRepository
    {
        //private readonly AppDbContext _dbcontext;

        public DepartementRepository(AppDbContext dbContext) :base(dbContext)   
        {
            //_dbcontext = dbContext;
        }

        
    }
}

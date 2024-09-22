using AutoMapper;
using ProjectMVC.DAL.Models;
using Projecy_MVC.PL.ViewModels;

namespace Projecy_MVC.PL.Helpers
{
    public class mappingProfiles :Profile
    {
        public mappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }

    }
}

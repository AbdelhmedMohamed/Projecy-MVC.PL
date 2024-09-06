using Microsoft.AspNetCore.Mvc;
using ProjectMVC.BLL.Interfacies;

namespace Projecy_MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartementRepository _departmentRepository;

        public DepartmentController(IDepartementRepository repository)
        {
            _departmentRepository = repository;
        }
        public IActionResult Index()
        {
            _departmentRepository.GetAll();

            return View();
        }
    }
}

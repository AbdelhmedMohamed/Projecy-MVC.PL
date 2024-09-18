using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfacies;
using ProjectMVC.DAL.Models;
using System;

namespace Projecy_MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            _EmployeeRepository = repository;
            _env = env;
        }

        public IActionResult Index()
        {
            var Employees = _EmployeeRepository.GetAll();

            ViewData["Message"] = "Hello in View Data";

            ViewBag.Message = "Hello in View Bag ";

            return View(Employees);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                var count = _EmployeeRepository.Add(Employee);
                if (count > 0)
                {
                    //TempData
                    TempData["Message"] = "Employee Create Succefully (TempData)";
                    
                }
                else
                {
                    TempData["Message"] = "An Error Occurred " ;
                }
                return RedirectToAction(nameof(Index));

            }
            return View(Employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _EmployeeRepository.GetById(id.Value);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(ViewName, Employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var Employee = _EmployeeRepository.GetById(id.Value);
            //if (Employee == null)
            //{
            //    return NotFound();
            //}
            //return View(Employee); 

            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(Employee);
            }

            try
            {
                _EmployeeRepository.Update(Employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr occured during update depatrment");
                }
                return View(Employee);
            }
        }

        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee Employee)
        {
            try
            {
                _EmployeeRepository.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Erorr occured during deleting depatrment");
                }
                return View(Employee);
            }
        }


    }
}

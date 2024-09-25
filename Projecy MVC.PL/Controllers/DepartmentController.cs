using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfacies;
using ProjectMVC.DAL.Models;
using System;

namespace Projecy_MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartementRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            //_departmentRepository = repository;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartementRepository.GetAll();      

            return View(departments);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                 _unitOfWork.DepartementRepository.Add(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartementRepository.GetById(id.Value);
            if (department == null) {
                return NotFound();
            }
            return View(ViewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var department = _departmentRepository.GetById(id.Value);
            //if (department == null)
            //{
            //    return NotFound();
            //}
            //return View(department); 

            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return View(department);
            }

            try
            {
                _unitOfWork.DepartementRepository.Update(department);
                _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else {
                    ModelState.AddModelError(string.Empty, "An Erorr occured during update depatrment");
                }
                return View(department);
            }
        }

        public IActionResult Delete(int? id) {

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartementRepository.Delete(department);
                _unitOfWork.Complete();
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
                return View(department);
            }
        }




    }
}
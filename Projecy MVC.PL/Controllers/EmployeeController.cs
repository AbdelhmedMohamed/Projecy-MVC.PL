using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfacies;
using ProjectMVC.DAL.Models;
using Projecy_MVC.PL.Helpers;
using Projecy_MVC.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Projecy_MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {

        //private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartementRepository _departementRepository;

        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env /*IDepartementRepository departementRepository*/ , IMapper mapper )
        {
           // _EmployeeRepository = repository;
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
            //_departementRepository = departementRepository;
        }

        public IActionResult Index(string searchInp )
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var Employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }
            else
            {
                var Employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(searchInp.ToLower());
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp); 
            }
           

           // ViewData["Message"] = "Hello in View Data";

           // ViewBag.Message = "Hello in View Bag ";

           
        }

        [HttpGet]

        public IActionResult Create()
        {
           // ViewData["Departments"] = _departementRepository.GetAll();
            //ViewBag.Departments = _departementRepository.GetAll(); 
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel EmployeeVM) 
        {
            if (ModelState.IsValid)
            {
                //mannual mapping
                //var mappedEmp = new Employee()
                //{
                //    Name = EmployeeVM.Name,
                //    Age = EmployeeVM.Age,
                //    Address = EmployeeVM.Address,
                //    Salary = EmployeeVM.Salary,
                //    Email = EmployeeVM.Email,
                //    PhoneNumber = EmployeeVM.PhoneNumber,
                //    IsActive = EmployeeVM.IsActive,
                //    HireDate = EmployeeVM.HireDate, //};

                EmployeeVM.ImageName = DocumentSettings.UploadFile(EmployeeVM.Image, "Images");

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWork.EmployeeRepository.Add(mappedEmp); //state added

                var count = _unitOfWork.Complete();

                //if (count > 0)
                //{
                //    //TempData
                //    TempData["Message"] = "Employee Create Succefully (TempData)";
                    
                //}
                //else
                //{
                //    TempData["Message"] = "An Error Occurred " ;
                //}
                return RedirectToAction(nameof(Index));

            }
            return View(EmployeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            //ViewBag.Departments = _departementRepository.GetAll();
            if (Employee == null)
            {
                return NotFound();
            }
            var EmpVM = _mapper.Map<Employee, EmployeeViewModel>(Employee); 
            return View(ViewName, EmpVM);
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(EmployeeVM);
            }

            try
            {
                EmployeeVM.ImageName = DocumentSettings.UploadFile(EmployeeVM.Image, "Images");

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWork.EmployeeRepository.Update(mappedEmp);
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
                    ModelState.AddModelError(string.Empty, "An Erorr occured during update depatrment");
                }
                return View(EmployeeVM);
            }
        }

        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel EmployeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                DocumentSettings.DeleteFile(EmployeeVM.ImageName, "Images"); 
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
                return View(EmployeeVM);
            }
        }


    }
}

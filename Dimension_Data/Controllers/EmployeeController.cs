using Dimension_Data.Data;
using Dimension_Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dimension_Data.Controllers
{
    public class EmployeeController : Controller

    {
        private readonly DatabaseDbContext _databaseDbContext;
        public EmployeeController(DatabaseDbContext databaseDbContext)
        {
            _databaseDbContext = databaseDbContext;
        }
        [HttpGet]
        public IActionResult Index()

        {
            var employee = _databaseDbContext.employee.ToList();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                _databaseDbContext.employee.Add(employeeModel);
                _databaseDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(employeeModel);
        }

        private IActionResult GetEmployeeById(int id)
        {
            var employee = _databaseDbContext.employee.FirstOrDefault(x=>x.id == id);
            return View(employee);
        }

        public IActionResult Edit(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                _databaseDbContext.employee.Update(employeeModel);
                _databaseDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        public IActionResult Delete(int id)
        {
            var employee = GetEmployeeById(id);
            _databaseDbContext.employee.Remove((EmployeeModel)employee);
            _databaseDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
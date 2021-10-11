using CoreMongoDB.IRepository;
using CoreMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoDB.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _empRepo = null;
        public EmployeesController(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = client.GetDatabase("OfficeDB");
            this.collection = db.GetCollection<Employee>("Employees");
        }

        private IMongoCollection<Employee> collection;

        public IActionResult Index()
        {
            var model = collection.Find(FilterDefinition<Employee>.Empty).ToList();
            return View(model);
        }
        public JsonResult GetEmployees()
        {
            var employee = _empRepo.Gets();
            return Json(employee);
        }
        public JsonResult SaveEmp(Employee employee)
        {
            var emp = _empRepo.Save(employee);
            return Json(emp);
        }
        public JsonResult DeleteEmp(string empId)
        {
            string message = _empRepo.Delete(empId);
            return Json(message);
        }
    }
}

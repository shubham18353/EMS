using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeDbContext db = new EmployeeDbContext();
            List<Employee> obj = db.GetEmployees();
            return View(obj);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    EmployeeDbContext db = new EmployeeDbContext();
                    bool check = db.AddEmployee(emp);
                    if (check)
                    {
                        TempData["InsertMessage"] = " Data has been inserted successully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }

            catch (Exception)
            {
                return View();
            }

        }
        public ActionResult Edit(int id)
        {
            EmployeeDbContext ctx = new EmployeeDbContext();
            var row = ctx.GetEmployees().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(int id, Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    EmployeeDbContext db = new EmployeeDbContext();
                    bool check = db.UpdateEmployee(emp);
                    if (check)
                    {
                        TempData["UpdateMessage"] = " Data has been Updated successully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }

            catch (Exception)
            {
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            EmployeeDbContext ctx = new EmployeeDbContext();
            var row = ctx.GetEmployees().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee emp)
        {
            try
            {

                EmployeeDbContext db = new EmployeeDbContext();
                bool check = db.DeleteEmployee(id);
                if (check)
                {
                    TempData["DeleteMessage"] = " Data has been Delete successully";

                    return RedirectToAction("Index");
                }

                return View();
            }

            catch (Exception)
            {
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            EmployeeDbContext ctx = new EmployeeDbContext();
            var row = ctx.GetEmployees().Find(model => model.Id == id);
            return View(row);
        }

    }
}
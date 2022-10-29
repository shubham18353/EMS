using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            try
            {
                DepartmentDbContext db = new DepartmentDbContext();
                List<Department> obj = db.GetDepartment();
                return View(obj);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department dep)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    DepartmentDbContext db = new DepartmentDbContext();
                    bool check = db.AddDepartment(dep);
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
            DepartmentDbContext ctx = new DepartmentDbContext();
            var row = ctx.GetDepartment().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(int id, Department emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    DepartmentDbContext db = new DepartmentDbContext();
                    bool check = db.UpdateDepartment(emp);
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
            DepartmentDbContext ctx = new DepartmentDbContext();
            var row = ctx.GetDepartment().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(int id, Department emp)
        {
            try
            {

                DepartmentDbContext db = new DepartmentDbContext();
                bool check = db.DeleteDepartment(id);
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
            DepartmentDbContext ctx = new DepartmentDbContext();
            var row = ctx.GetDepartment().Find(model => model.Id == id);
            return View(row);
        }
        [HandleError]
        public ActionResult Employees(Department dep)
        {
            try
            {
                EmployeeDbContext ctx = new EmployeeDbContext();
                List<Employee> row = ctx.GetEmployeeByDep(dep.Name);
                
                return View(row);
            }
            catch(Exception ex)
            {
                
                return View("Error", new HandleErrorInfo(ex, "Department", "Employees"));
            }
            
        }
    }
}
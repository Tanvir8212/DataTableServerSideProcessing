using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using PagedList.Mvc;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();

        // GET: Employees
        public ActionResult Index(int? i)
        {
            var emp = db.Employees.OrderBy(a=> a.ID1).ToPagedList(i ?? 1, 10);
            return View(emp);
        }

        public ActionResult Index2()
        {
            
            return View();
        }

        
        public ActionResult BlukUpdate()
        {
            string SearchText = Request["search[value]"];
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            return View();
        }

        public ActionResult GetAllEmployees()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string SearchText = Request["search[value]"];

            List<Employee> allEmp = db.Employees.ToList();


            if (!string.IsNullOrEmpty(Request["columns[0][search][value]"]))
                allEmp = allEmp.Where(x => x.Name.ToLower().Contains(Request["columns[0][search][value]"].ToLower())).ToList<Employee>();
            if (!string.IsNullOrEmpty(Request["columns[1][search][value]"]))
                allEmp = allEmp.ToList<Employee>();
            if (!string.IsNullOrEmpty(Request["columns[2][search][value]"]))
                allEmp = allEmp.ToList<Employee>();
            if (!string.IsNullOrEmpty(Request["columns[3][search][value]"]))
                allEmp = allEmp.ToList<Employee>();
            if (!string.IsNullOrEmpty(Request["columns[4][search][value]"]))
                allEmp = allEmp.ToList<Employee>();

          

            
           allEmp = allEmp.ToList().Skip(start).Take(length).ToList();




            return Json(new { data = allEmp }, JsonRequestBehavior.AllowGet);
            

           // return View();
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Salary,DepartmentID,ID1")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
     
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Salary,DepartmentID,ID1")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using MVCFIRST.Db_Connect;
using MVCFIRST.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFIRST.Controllers
{
    public class HomeController : Controller
    {
        EMPLOYEEINFOEntities dbobj = new EMPLOYEEINFOEntities();
        public ActionResult Index()
        {

            List<EMPMpdel> empobj = new List<EMPMpdel>();
            var res = dbobj.EMPLOYEEs.ToList();
            foreach (var item in res)
            {
                empobj.Add(new EMPMpdel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    City = item.City
                });
            }
            return View(empobj);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EMPMpdel Emp)
        {
            var EMP = new EMPLOYEE
            {
                Name = Emp.Name,
                Email = Emp.Email,
                Mobile = Emp.Mobile,
                City = Emp.City

            };
            dbobj.EMPLOYEEs.Add(EMP);
            dbobj.SaveChanges();

            return View();
        }
        public ActionResult Edit(int Id)
        {

            var emp = dbobj.EMPLOYEEs.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.Emp = emp;
            return View();
        }
        [HttpPost]
    public ActionResult Edit(EMPMpdel Emp)
        {
            var emp = dbobj.EMPLOYEEs.Where(x => x.Id == Emp.Id).FirstOrDefault();
            if (emp != null)
            {
                emp.Name = Emp.Name;
                emp.City = emp.City;
                emp.Email = emp.Email;
                emp.Mobile = emp.Mobile;
                dbobj.Entry(emp).State = EntityState.Modified;
                if (dbobj.SaveChanges() > 0)
                {
                    return RedirectToAction("Index", "Home");
                };
            }

            return View();
        }
    public ActionResult Delete(int Id)
        {
            var emp = dbobj.EMPLOYEEs.Where(x => x.Id == Id).FirstOrDefault();
                dbobj.Entry(emp).State = EntityState.Deleted;
                dbobj.SaveChanges();
            return RedirectToAction("Index");
        }
    public ActionResult About()
    {
        ViewBag.Message = "your application description page.";

        return View();
    }

    public ActionResult Contact()

        { 
             ViewBag.Message = "your contact page.";

            return View();
        }
    }
}
using Crud_Application.DbContext;
using Crud_Application.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud_Application.Controllers
{
    public class HomeController : Controller
    {
        DatabaseFirstApproachEntities Db = new DatabaseFirstApproachEntities();
        public ActionResult Index()
        {
            List<MainStudentModel> MainStudent = new List<MainStudentModel>();

            var data = Db.Students.ToList();
            foreach (var item in data)
            {
                MainStudent.Add(new MainStudentModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Gender = item.Gender,
                    Age = item.Age,
                    Address = item.Address,
                });
            }

            return View(MainStudent);
        }
        [HttpGet]
        public ActionResult AddDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDetails(MainStudentModel e)
        {
            //DatabaseFirstApproachEntities Db = new DatabaseFirstApproachEntities();
            Student objnew = new Student();
            objnew.Id = e.Id;
            objnew.Name = e.Name;
            objnew.Email = e.Email;
            objnew.Gender = e.Gender;
            objnew.Age = e.Age;
            objnew.Address = e.Address;

            if (e.Id == 0)
            {
                Db.Students.Add(objnew);
                Db.SaveChanges();
            }
            else
            {
                Db.Entry(objnew).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
            }
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult Delete(int id)
        {
            var deleteItem = Db.Students.Where(x => x.Id == id).First();
            Db.Students.Remove(deleteItem);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            MainStudentModel objstd = new MainStudentModel();
            var EditItem = Db.Students.Where(x => x.Id == id).First();
            objstd.Id = EditItem.Id;
            objstd.Name = EditItem.Name;
            objstd.Email = EditItem.Email;
            objstd.Gender = EditItem.Gender;
            objstd.Age = EditItem.Age;
            objstd.Address = EditItem.Address;


            return View("AddDetails", objstd);
        }
    }
}
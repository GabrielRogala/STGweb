using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STG.Models;
using Microsoft.AspNet.Identity;

namespace STG.Controllers
{
    [Authorize]
    public class SubjectTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: SubjectTypes
        public ActionResult Index()
        {
            List<SubjectTypes> list = new List<SubjectTypes>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.SubjectTypes
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var subjectTypes = db.SubjectTypes.Include(s => s.Schools);
            //return View(subjectTypes.ToList());
        }

        // GET: SubjectTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTypes subjectTypes = db.SubjectTypes.Find(id);
            if (subjectTypes == null)
            {
                return HttpNotFound();
            }
            return View(subjectTypes);
        }

        // GET: SubjectTypes/Create
        public ActionResult Create()
        {
            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()

            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            return View();
        }

        // POST: SubjectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Priority,SchoolsId")] SubjectTypes subjectTypes)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTypes.Add(subjectTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()


            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId", subjectTypes.SchoolsId);
            return View(subjectTypes);
        }

        // GET: SubjectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTypes subjectTypes = db.SubjectTypes.Find(id);
            if (subjectTypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", subjectTypes.SchoolsId);
            return View(subjectTypes);
        }

        // POST: SubjectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Priority,SchoolsId")] SubjectTypes subjectTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", subjectTypes.SchoolsId);
            return View(subjectTypes);
        }

        // GET: SubjectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTypes subjectTypes = db.SubjectTypes.Find(id);
            if (subjectTypes == null)
            {
                return HttpNotFound();
            }
            return View(subjectTypes);
        }

        // POST: SubjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectTypes subjectTypes = db.SubjectTypes.Find(id);
            db.SubjectTypes.Remove(subjectTypes);
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

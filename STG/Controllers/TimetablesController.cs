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
    public class TimetablesController : Controller
    {
        private Entities db = new Entities();

        // GET: Timetables
        public ActionResult Index()
        {

            List<Timetables> list = new List<Timetables>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.Timetables
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var timetables = db.Timetables.Include(t => t.Schools).Include(t => t.Lessons).Include(t => t.Rooms);
            //return View(timetables.ToList());
        }

        // GET: Timetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            return View(timetables);
        }

        // GET: Timetables/Create
        public ActionResult Create()
        {
            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()


            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            ViewBag.LessonsId = new SelectList(db.Lessons.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Schedule");
            ViewBag.RoomsId = new SelectList(db.Rooms.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            return View();
        }

        // POST: Timetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SchoolsId,LessonsId,Day,Hour,RoomsId,Size,Part")] Timetables timetables)
        {
            if (ModelState.IsValid)
            {
                db.Timetables.Add(timetables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()


            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId", timetables.SchoolsId);
            ViewBag.LessonsId = new SelectList(db.Lessons.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Schedule", timetables.LessonsId);
            ViewBag.RoomsId = new SelectList(db.Rooms.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", timetables.RoomsId);
            return View(timetables);
        }

        // GET: Timetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", timetables.SchoolsId);
            ViewBag.LessonsId = new SelectList(db.Lessons, "Id", "Schedule", timetables.LessonsId);
            ViewBag.RoomsId = new SelectList(db.Rooms, "Id", "Name", timetables.RoomsId);
            return View(timetables);
        }

        // POST: Timetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SchoolsId,LessonsId,Day,Hour,RoomsId,Size,Part")] Timetables timetables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", timetables.SchoolsId);
            ViewBag.LessonsId = new SelectList(db.Lessons, "Id", "Schedule", timetables.LessonsId);
            ViewBag.RoomsId = new SelectList(db.Rooms, "Id", "Name", timetables.RoomsId);
            return View(timetables);
        }

        // GET: Timetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetables timetables = db.Timetables.Find(id);
            if (timetables == null)
            {
                return HttpNotFound();
            }
            return View(timetables);
        }

        // POST: Timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timetables timetables = db.Timetables.Find(id);
            db.Timetables.Remove(timetables);
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

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
    public class LessonsController : Controller
    {
        private Entities db = new Entities();

        // GET: Lessons
        public ActionResult Index()
        {
            List<Lessons> list = new List<Lessons>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.Lessons
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var lessons = db.Lessons.Include(l => l.Subjects).Include(l => l.Teachers).Include(l => l.Groups).Include(l => l.Rooms).Include(l => l.RoomTypes).Include(l => l.Schools);
            //return View(lessons.ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            ViewBag.SubjectsId = new SelectList(db.Subjects.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.TeachersId = new SelectList(db.Teachers.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.GroupsId = new SelectList(db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.RoomsId = new SelectList(db.Rooms.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.RoomTypesId = new SelectList(db.RoomTypes.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubjectsId,TeachersId,GroupsId,RoomsId,RoomTypesId,Schedule,SchoolsId")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lessons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            ViewBag.SubjectsId = new SelectList(db.Subjects.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", lessons.SubjectsId);
            ViewBag.TeachersId = new SelectList(db.Teachers.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", lessons.TeachersId);
            ViewBag.GroupsId = new SelectList(db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", lessons.GroupsId);
            ViewBag.RoomsId = new SelectList(db.Rooms.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", lessons.RoomsId);
            ViewBag.RoomTypesId = new SelectList(db.RoomTypes.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", lessons.RoomTypesId);
            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId", lessons.SchoolsId);
            return View(lessons);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectsId = new SelectList(db.Subjects, "Id", "Name", lessons.SubjectsId);
            ViewBag.TeachersId = new SelectList(db.Teachers, "Id", "Name", lessons.TeachersId);
            ViewBag.GroupsId = new SelectList(db.Groups, "Id", "Name", lessons.GroupsId);
            ViewBag.RoomsId = new SelectList(db.Rooms, "Id", "Name", lessons.RoomsId);
            ViewBag.RoomTypesId = new SelectList(db.RoomTypes, "Id", "Name", lessons.RoomTypesId);
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", lessons.SchoolsId);
            return View(lessons);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubjectsId,TeachersId,GroupsId,RoomsId,RoomTypesId,Schedule,SchoolsId")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectsId = new SelectList(db.Subjects, "Id", "Name", lessons.SubjectsId);
            ViewBag.TeachersId = new SelectList(db.Teachers, "Id", "Name", lessons.TeachersId);
            ViewBag.GroupsId = new SelectList(db.Groups, "Id", "Name", lessons.GroupsId);
            ViewBag.RoomsId = new SelectList(db.Rooms, "Id", "Name", lessons.RoomsId);
            ViewBag.RoomTypesId = new SelectList(db.RoomTypes, "Id", "Name", lessons.RoomTypesId);
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", lessons.SchoolsId);
            return View(lessons);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lessons lessons = db.Lessons.Find(id);
            db.Lessons.Remove(lessons);
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

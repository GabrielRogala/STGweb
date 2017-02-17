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
    public class RoomsController : Controller
    {
        private Entities db = new Entities();

        // GET: Rooms
        public ActionResult Index()
        {
            List<Rooms> list = new List<Rooms>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.Rooms
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var rooms = db.Rooms.Include(r => r.RoomTypes).Include(r => r.Schools);
            //return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            var roomTypes = (from r in db.RoomTypes
                             where r.SchoolsId.Equals(list.First().Id)
                             select r).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()
            ViewBag.RoomTypesId = new SelectList(roomTypes, "Id", "Name");

            //ViewBag.RoomTypesId = new SelectList(db.RoomTypes.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RoomTypesId,SchoolsId,Amount,BlockedHours")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()

            ViewBag.RoomTypesId = new SelectList(db.RoomTypes.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            return View();
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            ViewBag.RoomTypesId = new SelectList(db.RoomTypes.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", rooms.RoomTypesId);
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", rooms.SchoolsId);
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RoomTypesId,SchoolsId,Amount,BlockedHours")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomTypesId = new SelectList(db.RoomTypes, "Id", "Name", rooms.RoomTypesId);
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", rooms.SchoolsId);
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rooms rooms = db.Rooms.Find(id);
            db.Rooms.Remove(rooms);
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

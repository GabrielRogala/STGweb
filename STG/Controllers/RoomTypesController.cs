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
    public class RoomTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: RoomTypes
        public ActionResult Index()
        {
            List<RoomTypes> list = new List<RoomTypes>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.RoomTypes
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var roomTypes = db.RoomTypes.Include(r => r.Schools);
            //return View(roomTypes.ToList());
        }

        // GET: RoomTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.RoomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            return View(roomTypes);
        }

        // GET: RoomTypes/Create
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

        // POST: RoomTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SchoolsId,Name")] RoomTypes roomTypes)
        {
            if (ModelState.IsValid)
            {
                db.RoomTypes.Add(roomTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()

            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId", roomTypes.SchoolsId);
            return View(roomTypes);
        }

        // GET: RoomTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.RoomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", roomTypes.SchoolsId);
            return View(roomTypes);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SchoolsId,Name")] RoomTypes roomTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", roomTypes.SchoolsId);
            return View(roomTypes);
        }

        // GET: RoomTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomTypes roomTypes = db.RoomTypes.Find(id);
            if (roomTypes == null)
            {
                return HttpNotFound();
            }
            return View(roomTypes);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomTypes roomTypes = db.RoomTypes.Find(id);
            db.RoomTypes.Remove(roomTypes);
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

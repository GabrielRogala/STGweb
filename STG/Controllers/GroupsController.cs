using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STG.Models;

namespace STG.Controllers
{
    public class GroupsController : Controller
    {
        private Entities db = new Entities();

        // GET: Groups
        public ActionResult Index()
        {
            var groups = db.Groups.Include(g => g.Schools).Include(g => g.Groups2).Include(g => g.SubGroupTypes1);
            return View(groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId");
            ViewBag.ParentGroup = new SelectList(db.Groups, "Id", "Name");
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes, "Id", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Amount,SchoolsId,ParentGroup,SubGroupTypesId,BlockedHours")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(groups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", groups.SchoolsId);
            ViewBag.ParentGroup = new SelectList(db.Groups, "Id", "Name", groups.ParentGroup);
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes, "Id", "Name", groups.SubGroupTypesId);
            return View(groups);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", groups.SchoolsId);
            ViewBag.ParentGroup = new SelectList(db.Groups, "Id", "Name", groups.ParentGroup);
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes, "Id", "Name", groups.SubGroupTypesId);
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Amount,SchoolsId,ParentGroup,SubGroupTypesId,BlockedHours")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolsId = new SelectList(db.Schools, "Id", "AspNetUsersId", groups.SchoolsId);
            ViewBag.ParentGroup = new SelectList(db.Groups, "Id", "Name", groups.ParentGroup);
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes, "Id", "Name", groups.SubGroupTypesId);
            return View(groups);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groups groups = db.Groups.Find(id);
            db.Groups.Remove(groups);
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

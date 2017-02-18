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
    public class GroupsController : Controller
    {
        private Entities db = new Entities();

        // GET: Groups
        public ActionResult Index()
        {
            List<Groups> list = new List<Groups>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.Groups
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var groups = db.Groups.Include(g => g.Schools).Include(g => g.Groups2).Include(g => g.SubGroupTypes1);
            //return View(groups.ToList());
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
            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()
            var gr = db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList();
            gr.Add(null);
            var sgr = db.SubGroupTypes.Where(s => s.Groups.SchoolsId == list.First().Id).ToList();
            sgr.Add(null);

            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId");
            ViewBag.ParentGroup = new SelectList(gr, "Id", "Name");
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes.Where(s => s.Groups.SchoolsId == list.First().Id).ToList(), "Id", "Name");
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

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()


            ViewBag.SchoolsId = new SelectList(list, "Id", "AspNetUsersId", groups.SchoolsId);
            ViewBag.ParentGroup = new SelectList(db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", groups.ParentGroup);
            ViewBag.SubGroupTypesId = new SelectList(db.SubGroupTypes.Where(s => s.Groups.SchoolsId == list.First().Id).ToList(), "Id", "Name", groups.SubGroupTypesId);
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

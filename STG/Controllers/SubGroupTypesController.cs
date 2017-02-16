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
    public class SubGroupTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: SubGroupTypes
        public ActionResult Index()
        {

            List<SubGroupTypes> list = new List<SubGroupTypes>();
            var user = User.Identity.GetUserId();
            Schools school = (from b in db.Schools
                              where b.AspNetUsersId.Equals(user)
                              select b).FirstOrDefault();
            if (school != null)
            {
                list = (from b in db.SubGroupTypes
                        where b.Groups.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }

            return View(list);

            //var subGroupTypes = db.SubGroupTypes.Include(s => s.Groups);
            //return View(subGroupTypes.ToList());
        }

        // GET: SubGroupTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGroupTypes subGroupTypes = db.SubGroupTypes.Find(id);
            if (subGroupTypes == null)
            {
                return HttpNotFound();
            }
            return View(subGroupTypes);
        }

        // GET: SubGroupTypes/Create
        public ActionResult Create()
        {
            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            //   .Where(s => s.SchoolsId == list.First().Id).ToList()


            ViewBag.GroupsId = new SelectList(db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name");
            return View();
        }

        // POST: SubGroupTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GroupsId")] SubGroupTypes subGroupTypes)
        {
            if (ModelState.IsValid)
            {
                db.SubGroupTypes.Add(subGroupTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();

            ViewBag.GroupsId = new SelectList(db.Groups.Where(s => s.SchoolsId == list.First().Id).ToList(), "Id", "Name", subGroupTypes.GroupsId);
            return View(subGroupTypes);
        }

        // GET: SubGroupTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGroupTypes subGroupTypes = db.SubGroupTypes.Find(id);
            if (subGroupTypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupsId = new SelectList(db.Groups, "Id", "Name", subGroupTypes.GroupsId);
            return View(subGroupTypes);
        }

        // POST: SubGroupTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GroupsId")] SubGroupTypes subGroupTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subGroupTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupsId = new SelectList(db.Groups, "Id", "Name", subGroupTypes.GroupsId);
            return View(subGroupTypes);
        }

        // GET: SubGroupTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubGroupTypes subGroupTypes = db.SubGroupTypes.Find(id);
            if (subGroupTypes == null)
            {
                return HttpNotFound();
            }
            return View(subGroupTypes);
        }

        // POST: SubGroupTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubGroupTypes subGroupTypes = db.SubGroupTypes.Find(id);
            db.SubGroupTypes.Remove(subGroupTypes);
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

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
    public class STGConfigsController : Controller
    {
        private Entities db = new Entities();

        // GET: STGConfigs
        public ActionResult Index()
        {

            //var user = User.Identity.GetUserId();
            //Schools school = (from b in db.Schools
            //                  where b.AspNetUsersId.Equals(user)
            //                  select b).FirstOrDefault();

            //List<STGConfig> list = new List<STGConfig>();
            //list.Add(school.STGConfig);

            //return View(list);
            return View(db.STGConfig.ToList());
        }

        // GET: STGConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STGConfig sTGConfig = db.STGConfig.Find(id);
            if (sTGConfig == null)
            {
                return HttpNotFound();
            }
            return View(sTGConfig);
        }

        // GET: STGConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: STGConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PopulationSize,EpocheSize,NumberOfLessonToPositioning,BottomBorderOfBestSlots,TopBorderOfBestSlots,ProbabilityOfMutation")] STGConfig sTGConfig)
        {
            if (ModelState.IsValid)
            {
                db.STGConfig.Add(sTGConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTGConfig);
        }

        // GET: STGConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STGConfig sTGConfig = db.STGConfig.Find(id);
            if (sTGConfig == null)
            {
                return HttpNotFound();
            }
            return View(sTGConfig);
        }

        // POST: STGConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PopulationSize,EpocheSize,NumberOfLessonToPositioning,BottomBorderOfBestSlots,TopBorderOfBestSlots,ProbabilityOfMutation")] STGConfig sTGConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTGConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTGConfig);
        }

        // GET: STGConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STGConfig sTGConfig = db.STGConfig.Find(id);
            if (sTGConfig == null)
            {
                return HttpNotFound();
            }
            return View(sTGConfig);
        }

        // POST: STGConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STGConfig sTGConfig = db.STGConfig.Find(id);
            db.STGConfig.Remove(sTGConfig);
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

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STG.Models;
using STG.Controllers.Engine;
using Microsoft.AspNet.Identity;

namespace STG.Controllers
{
    [Authorize]
    public class SchoolsController : Controller
    {
        private Entities db = new Entities();

        // GET: Schools
        public ActionResult Index()
        {

            List<Schools> list = new List<Schools>();
            var user = User.Identity.GetUserId();
            list = (from b in db.Schools
                    where b.AspNetUsersId.Equals(user)
                    select b).ToList();
            

            return View(list);

            //var schools = db.Schools.Include(s => s.AspNetUsers).Include(s => s.STGConfig);

            //return View(schools.ToList());
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schools schools = db.Schools.Find(id);
            if (schools == null)
            {
                return HttpNotFound();
            }
            return View(schools);
        }

        // GET: Schools/Generate/5
        public ActionResult Generate(int? id)
        {
            List<Timetables> list = new List<Timetables>();
            Schools school;
            if (id == null) {
                var user = User.Identity.GetUserId();
                school = (from b in db.Schools
                        where b.AspNetUsersId.Equals(user)
                        select b).FirstOrDefault();
            }else{
                school = db.Schools.Find(id);
            }

            if (school != null)
            {
                list = (from b in db.Timetables
                        where b.SchoolsId.Equals(school.Id)
                        select b).ToList();
            }
            db.Timetables.RemoveRange(list);
            db.SaveChanges();

            GenerateObjectWithDataBase(school);
           
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            List<AspNetUsers> userList = new List<AspNetUsers>();
            userList.Add(db.AspNetUsers.Find(user));

            ViewBag.AspNetUsersId = new SelectList(userList, "Id", "Email");
            ViewBag.STGConfigId = new SelectList(db.STGConfig, "Id", "Id");
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AspNetUsersId,NumberOfDays,NumberOfHours,STGConfigId")] Schools schools)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(schools);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", schools.AspNetUsersId);
            ViewBag.STGConfigId = new SelectList(db.STGConfig, "Id", "Id", schools.STGConfigId);
            return View(schools);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schools schools = db.Schools.Find(id);
            if (schools == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", schools.AspNetUsersId);
            ViewBag.STGConfigId = new SelectList(db.STGConfig, "Id", "Id", schools.STGConfigId);
            return View(schools);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AspNetUsersId,NumberOfDays,NumberOfHours,STGConfigId")] Schools schools)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schools).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", schools.AspNetUsersId);
            ViewBag.STGConfigId = new SelectList(db.STGConfig, "Id", "Id", schools.STGConfigId);
            return View(schools);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schools schools = db.Schools.Find(id);
            if (schools == null)
            {
                return HttpNotFound();
            }
            return View(schools);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schools schools = db.Schools.Find(id);
            db.Schools.Remove(schools);
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


        //-------------------------------------------
        public void GenerateObjectWithDataBase(Schools school)
        {
            STGCfg config = getSTGCfg(school);
            List<Teacher> teachers = getTeachers(school);

            List<Group> groups = getGroups(school);

            List<RoomType> roomTypes = getRoomTypes(school);
            List<Room> rooms = getRooms(school,roomTypes);

            List<SubjectType> subjectTypes = getSubjectTypes(school);
            List<Subject> subjects = getSubjects(school,subjectTypes);

            List<Lesson> lessons = getLessons(school,subjects,roomTypes,groups,teachers);
            Population population = new Population(lessons,teachers,groups,rooms,school.NumberOfDays,school.NumberOfHours, config);
            population.start();
            //population.getBestSchoolTimeTable().genWeb("ouuuuut");
            setSchoolTimetableToDAtaBaseTimetable(school,population.getBestSchoolTimeTable());
        }

        private void setSchoolTimetableToDAtaBaseTimetable(Schools school, SchoolTimetable stt) {
            foreach (Lesson l in stt.getLessons()) {

                //--------------------------------
                Groups gr = db.Groups.Find(l.getGroup().getName());
                //--------------------------------

                //--------------------------------
                Teachers te = db.Teachers.Find(l.getTeacher().getName());
                //--------------------------------

                //--------------------------------
                Rooms ro = db.Rooms.Find(l.getRoom().getName());
                //--------------------------------

                //--------------------------------
                Lessons le = null;
                List<Lessons> lessons = (from b in db.Lessons
                                     where b.SchoolsId.Equals(school.Id)
                                     select b).ToList();
                foreach (Lessons ls in lessons)
                {
                    if (ls.Groups.Equals(gr) && 
                        ls.Teachers.Equals(te) && 
                        ls.Subjects.Id.Equals(l.getSubject().getName())
                        )
                    {
                        le = ls;
                        break;
                    }
                }
                //--------------------------------

                for (int i = 0; i<l.getSize();i++) {
                    Timetables timetable = new Timetables();
                    timetable.Hour = l.getSlots()[i].hour;
                    timetable.Day = l.getSlots()[i].day;
                    timetable.LessonsId = le.Id;
                    timetable.Part = i+1;
                    timetable.SchoolsId = school.Id;
                    timetable.RoomsId = ro.Id;
                    timetable.Size = l.getSize();
               
                    db.Timetables.Add(timetable);
                    db.SaveChanges();
                }

            }
        }

        private STGCfg getSTGCfg(Schools school)
        {
            STGConfig c = school.STGConfig;
            return new STGCfg(c.PopulationSize,
                c.EpocheSize,
                c.NumberOfLessonToPositioning,
                c.BottomBorderOfBestSlots,
                c.TopBorderOfBestSlots,
                c.ProbabilityOfMutation);
        }

        class SubGroupIndex {
            public int id;
            public int index;

            public SubGroupIndex(int id, int index) {
                this.id = id;
                this.index = index;
            }

            public override bool Equals(object obj)
            {
                return id.Equals(((SubGroupIndex)obj).id);
            }
        }

        private List<Group> getGroups(Schools school)
        {
            List<Groups> groups = db.Groups.Where(g => g.SchoolsId == school.Id).ToList();

            List<Group> tmp = new List<Group>();

            foreach (Groups g in groups) {
                tmp.Add(new Group(g.Id,g.Amount));
            }

            List<SubGroupIndex> subGroupIndexs = new List<SubGroupIndex>();

            for (int i =0; i<groups.Count; i++)
            {
                if (groups[i].ParentGroup != null) {
                    Group parent = null;
                    foreach (Group g in tmp) {
                        Groups parentGroup = db.Groups.Find(groups[i].ParentGroup);

                        if (g.getName().Equals(parentGroup.Id)) {
                            parent = g;
                            break;
                        }
                    }
                    tmp[i].setParent(parent);

                    bool result = false;
                    SubGroupIndex subGroupIndex = null;

                    foreach (SubGroupIndex s in subGroupIndexs) {
                        if (s.id == groups[i].SubGroupTypesId.Value) {
                            subGroupIndex = s;
                            result = true;
                            break;
                        }
                    }

                    if (result) {
                        subGroupIndex.index++; 
                    }else { 
                        subGroupIndexs.Add(new SubGroupIndex(groups[i].SubGroupTypesId.Value, 1));
                        subGroupIndex = subGroupIndexs.Last();
                    }

                    tmp[i].setSubGroupId(subGroupIndex.id);
                    tmp[i].setSubGroupsIndex(subGroupIndex.index);

                    parent.getSubGroup().Add(tmp[i]);
                }
            }

            for (int i = 0; i < tmp.Count;) {
                if (tmp[i].getParent() != null)
                {
                    tmp.Remove(tmp[i]);
                } else {
                    i++;
                }
            }

            return tmp;
        }

        private List<Teacher> getTeachers(Schools school)
        {
            List<Teacher> tmp = new List<Teacher>();
            List<Teachers> teachers = db.Teachers.Where(g => g.SchoolsId == school.Id).ToList();

            foreach (Teachers t in teachers) {
                tmp.Add(new Teacher(t.Id));
            }

            return tmp;
        }

        private List<Room> getRooms(Schools school, List<RoomType> roomTypes)
        {
            List<Room> tmp = new List<Room>();
            List<Rooms> rooms = db.Rooms.Where(g => g.SchoolsId == school.Id).ToList();

            foreach (Rooms r in rooms) {
                RoomType type = null;
                foreach (RoomType rt in roomTypes) {
                    if (r.RoomTypes.Id.Equals(rt.getName())) {
                        type = rt;
                        break;
                    }
                }
                tmp.Add(new Room(r.Id,r.Amount,type));
            }

            return tmp;
        }

        private List<RoomType> getRoomTypes(Schools school)
        {
            List<RoomType> tmp = new List<RoomType>();
            List<RoomTypes> roomTypes = db.RoomTypes.Where(g => g.SchoolsId == school.Id).ToList();

            foreach (RoomTypes r in roomTypes)
            {
                tmp.Add(new RoomType(r.Id));
            }

            return tmp;
        }

        private List<SubjectType> getSubjectTypes(Schools school)
        {
            List<SubjectType> tmp = new List<SubjectType>();
            List<SubjectTypes> subjectTypes = db.SubjectTypes.Where(g => g.SchoolsId == school.Id).ToList();

            foreach (SubjectTypes s in subjectTypes)
            {
                tmp.Add(new SubjectType(s.Id,s.Priority));
            }

            return tmp;
        }

        private List<Subject> getSubjects(Schools school, List<SubjectType> subjectTypes)
        {
            List<Subject> tmp = new List<Subject>();
            List<Subjects> subjects = db.Subjects.Where(g => g.SchoolsId == school.Id).ToList();

            foreach (Subjects s in subjects)
            {

                SubjectType subjectType = null;
                foreach (SubjectType st in subjectTypes)
                {
                    if (s.SubjectTypes.Id.Equals(st.getName()))
                    {
                        subjectType = st;
                        break;
                    }
                }

                tmp.Add(new Subject(s.Id, subjectType));
            }


            return tmp;
        }

        private List<Lesson> getLessons(Schools school, List<Subject> subjects, List<RoomType> roomTypes, List<Group> groups, List<Teacher> teachers) {
            List<Lesson> tmp = new List<Lesson>();
            Subject subject = null;
            RoomType roomType = null;
            Group group = null;
            Teacher teacher = null;

            List<Lessons> lessons = db.Lessons.Where(g => g.SchoolsId == school.Id).ToList();
            
            foreach (Lessons l in lessons) {

                foreach (Subject s in subjects)
                {
                    if (s.getName().Equals(l.Subjects.Id))
                    {
                        subject = s;
                        break;
                    }
                }

                foreach (Teacher t in teachers) {
                    if (t.getName().Equals(l.Teachers.Id)) {
                        teacher = t;
                        break;
                    }
                }

                foreach (Group g in groups)
                {
                    if (g.getName().Equals(l.Groups.Id))
                    {
                        group = g;
                        break;
                    }
                    else if (g.getSubGroup().Count > 0)
                    {
                        foreach (Group sg in g.getSubGroup())
                        {
                            if (sg.getName().Equals(l.Groups.Id))
                            {
                                group = sg;
                                break;
                            }
                        }
                    }
                }

                foreach (RoomType rt in roomTypes)
                {
                    if (rt.getName().Equals(l.RoomTypes.Id))
                    {
                        roomType = rt;
                        break;
                    }
                }

                List<Int32> schedule = new List<Int32>();
                int amount = 0;
                for (int i = 0; i < l.Schedule.Count(); i++) {
                    if (l.Schedule[i] != '.') {
                        schedule.Add(Int32.Parse(l.Schedule[i].ToString()));
                        amount += schedule.Last();
                        //if (Int32.TryParse(l.Schedule[i].ToString(),out size)) {
                        //    size = Int32.Parse(l.Schedule[i].ToString());
                        //}   
                    }
                }
               
                foreach (Int32 s in schedule) {
                    tmp.Add(new Lesson(teacher,group,subject,roomType,amount,s));
                }
                
            }

            return tmp;
        }
    }
}

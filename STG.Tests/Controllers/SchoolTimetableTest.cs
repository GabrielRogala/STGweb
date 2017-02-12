using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG.Controllers.Engine;
using System.Collections.Generic;
using STG.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace STG.Tests.Controllers
{
    [TestClass]
    public class SchoolTimetableTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Lesson> lessons = new List<Lesson>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();
            List<Room> rooms = new List<Room>();
            List<RoomType> roomTypes = new List<RoomType>();
            List<SubjectType> subjectTypes = new List<SubjectType>();

            for (int j = 0; j < 7; j++)
            {
                teachers.Add(new Teacher("t" + j));
            }
            for (int j = 0; j < 6; j++)
            {
                List<Group> subGroups = new List<Group>();
                int randAmount = new Random().Next(15,20);
                subGroups.Add(new Group("g" + j + "WFm", randAmount));
                subGroups.Add(new Group("g" + j + "WFK", (25 + j) - randAmount));
                groups.Add(new Group("g" + j, 25 + j,subGroups));
                
            }

            roomTypes.Add(new RoomType("A"));
            roomTypes.Add(new RoomType("B"));
            roomTypes.Add(new RoomType("C"));

            int nr = 0;
            rooms.Add(new Room("nr" + nr++, 25, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[1]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[1]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[2]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[2]));

            subjectTypes.Add(new SubjectType("HUM", 7));
            subjectTypes.Add(new SubjectType("SCI", 8));
            subjectTypes.Add(new SubjectType("JEZ", 6));
            subjectTypes.Add(new SubjectType("SPE", 9));
            subjectTypes.Add(new SubjectType("SPO", 3));
            subjectTypes.Add(new SubjectType("INN", 3));

            int i = 0;
            subjects.Add(new Subject("pol", subjectTypes[0], roomTypes[0]));
            subjects.Add(new Subject("ang", subjectTypes[2], roomTypes[0]));
            subjects.Add(new Subject("mat", subjectTypes[1], roomTypes[0]));
            subjects.Add(new Subject("his", subjectTypes[0], roomTypes[0]));
            subjects.Add(new Subject("wos", subjectTypes[0], roomTypes[0]));
            subjects.Add(new Subject("fiz", subjectTypes[1], roomTypes[0]));
            //subjects.Add(new Subject("bio", subjectTypes[1], roomTypes[0]));
            subjects.Add(new Subject("geo", subjectTypes[1], roomTypes[0]));
            subjects.Add(new Subject("w-f", subjectTypes[4], roomTypes[2]));
            subjects.Add(new Subject("rel", subjectTypes[5], roomTypes[0]));
            subjects.Add(new Subject("inf", subjectTypes[3], roomTypes[1]));
            subjects.Add(new Subject("PRO", subjectTypes[3], roomTypes[1]));

            foreach (Group g in groups)
            {
                int tI = 0;
                int sI = 0;
                int amount = 0;
                //max = 45
                //----------pol----------
                tI = 0; sI = 0; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                //----------ang----------
                tI = 1; sI++ ; amount = 4;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------mat----------
                tI = 2; sI++; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------his----------
                tI = 3; sI++; amount = 1;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------wos----------
                tI = 3; sI++; amount = 2;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------fiz----------
                tI = 5; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------bio----------
                //tI = 3; sI++; amount = 1;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------geo----------
                tI = 3; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------w-f----------
                tI = 4; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[0], subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[0], subjects[sI], amount));
                
                tI = 3; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], amount));
                
                //----------rel----------
                tI = 5; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------inf----------
                tI = 4; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount));
                //----------pro----------
                tI = 6; sI++; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 2));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], amount, 3));

            }

            SchoolTimetable stt = new SchoolTimetable(teachers, groups, rooms,lessons,subjectTypes,5,9);
            stt.generateSchoolTimetable();
            stt.print();
            Console.WriteLine(stt.isCorrect());
            stt.genWeb("test1");
        }

        [TestMethod]
        public void TestSubGroup()
        {
            List<Lesson> lessons = new List<Lesson>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();
            List<Room> rooms = new List<Room>();
            List<SubjectType> subjectTypes = new List<SubjectType>();
            List<RoomType> roomTypes = new List<RoomType>();

            teachers.Add(new Teacher("t0"));
            teachers.Add(new Teacher("t1"));

            List<Group> subGroups = new List<Group>();
            subGroups.Add(new Group("g0WFm", 10 , 1,1));
            subGroups.Add(new Group("g0WFk", 15, 1,2));
            groups.Add(new Group("g0", 25, subGroups));

            roomTypes.Add(new RoomType("C"));

            rooms.Add(new Room("nr0", 40, roomTypes[0]));
            rooms.Add(new Room("nr1", 40, roomTypes[0]));

            subjectTypes.Add(new SubjectType("SPO", 2) );
            
            subjects.Add(new Subject("w-f", subjectTypes[0], roomTypes[0]));


            foreach (Group g in groups)
            {
                lessons.Add(new Lesson(teachers[0], g.getSubGroup()[0], subjects[0], 2));
                lessons.Add(new Lesson(teachers[0], g.getSubGroup()[0], subjects[0], 2));

                lessons.Add(new Lesson(teachers[1], g.getSubGroup()[1], subjects[0], 2));
                lessons.Add(new Lesson(teachers[1], g.getSubGroup()[1], subjects[0], 2));
            }

            SchoolTimetable stt = new SchoolTimetable(teachers, groups, rooms, lessons, subjectTypes, 3, 3);
            stt.generateSchoolTimetable();
            stt.print();

            stt.genWeb("subGroupTest");
            
        }


        private Entities db = new Entities();
        [TestMethod]
        public void GenerateObjectWithDataBase() {

            Schools schools = db.Schools.Find(1);
            Console.WriteLine(schools.AspNetUsersId);

            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();

            List<RoomType> roomTypes = new List<RoomType>();
            List<Room> rooms = new List<Room>();
            
            List<SubjectType> subjectTypes = new List<SubjectType>();
            List<Subject> subjects = new List<Subject>();
            
            

            List<Lesson> lessons = new List<Lesson>();
        }

        private List<Group> getGroups(Schools school) {
            List<Group> tmp = new List<Group>();

            return tmp;
        }

        private List<Teacher> getTeachers(Schools school)
        {
            List<Teacher> tmp = new List<Teacher>();

            return tmp;
        }

        private List<Room> getRooms(Schools school, List<String> roomTypes)
        {
            List<Room> tmp = new List<Room>();

            return tmp;
        }

        private List<String> getRoomTypes(Schools school)
        {
            List<String> tmp = new List<String>();

            return tmp;
        }

        private List<String> getSubjectTypes(Schools school)
        {
            List<String> tmp = new List<String>();

            return tmp;
        }

        private List<Subject> getSubjects(Schools school, List<String> subjectTypes)
        {
            List<Subject> tmp = new List<Subject>();

            return tmp;
        }


    }
}

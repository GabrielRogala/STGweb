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
using STG.Controllers;

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
            STGCfg config = new STGCfg(1,1,5,1,5,10);

            for (int j = 0; j < 7; j++)
            {
                teachers.Add(new Teacher("t" + j));
            }
            for (int j = 0; j < 6; j++)
            {
                List<Group> subGroups = new List<Group>();
                int randAmount = new Random().Next(15, 20);
                subGroups.Add(new Group("g" + j + "WFm", randAmount));
                subGroups.Add(new Group("g" + j + "WFK", (25 + j) - randAmount));
                groups.Add(new Group("g" + j, 25 + j, subGroups));

            }

            roomTypes.Add(new RoomType("A"));
            roomTypes.Add(new RoomType("B"));
            roomTypes.Add(new RoomType("C"));

            int nr = 0;
            rooms.Add(new Room("nr" + nr++, 25, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[0]));
            rooms.Add(new Room("nr" + nr++, 40, roomTypes[1]));
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
            subjects.Add(new Subject("pol", subjectTypes[0]));
            subjects.Add(new Subject("ang", subjectTypes[2]));
            subjects.Add(new Subject("mat", subjectTypes[1]));
            subjects.Add(new Subject("his", subjectTypes[0]));
            subjects.Add(new Subject("wos", subjectTypes[0]));
            subjects.Add(new Subject("fiz", subjectTypes[1]));
            subjects.Add(new Subject("bio", subjectTypes[1]));
            subjects.Add(new Subject("geo", subjectTypes[1]));
            subjects.Add(new Subject("w-f", subjectTypes[4]));
            subjects.Add(new Subject("rel", subjectTypes[5]));
            subjects.Add(new Subject("inf", subjectTypes[3]));
            subjects.Add(new Subject("PRO", subjectTypes[3]));

            foreach (Group g in groups)
            {
                int tI = 0;
                int sI = 0;
                int amount = 0;
                //max = 45
                //----------pol----------
                tI = 0; sI = 0; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount, 2));
                //----------ang----------
                tI = 1; sI++; amount = 4;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------mat----------
                tI = 2; sI++; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------his----------
                tI = 3; sI++; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------wos----------
                tI = 3; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------fiz----------
                tI = 5; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------bio----------
                tI = 3; sI++; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------geo----------
                tI = 3; sI++; amount = 2;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------w-f----------
                tI = 4; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[0], subjects[sI], roomTypes[2], amount));
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[0], subjects[sI], roomTypes[2], amount));

                tI = 3; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], roomTypes[2], amount));
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], roomTypes[2], amount));

                //----------rel----------
                tI = 5; sI++; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------inf----------
                tI = 4; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount));
                //----------pro----------
                tI = 6; sI++; amount = 5;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount, 2));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount, 3));

            }

            SchoolTimetable stt = new SchoolTimetable(teachers, groups, rooms, lessons, 5, 9,config);
            stt.generateSchoolTimetable();
            stt.print();

            SchoolTimetable stt2 = new SchoolTimetable(teachers, groups, rooms, lessons, 5, 9, config);
            stt2.generateSchoolTimetable();
            stt2.print();

            SchoolTimetable stt3 = new SchoolTimetable(stt);
            stt3.crossover(stt2);
            stt3.print();

            //foreach (Lesson l in stt.getLessons())
            //{
            //    Console.WriteLine(l.ToString());
            //}

            //SchoolTimetable stt2 = new SchoolTimetable(stt);
            //stt2.print();

            Console.WriteLine(stt.isCorrect() +" " + stt.fitness());
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
            STGCfg config = new STGCfg(1, 1, 2, 1, 5, 10);

            teachers.Add(new Teacher("t0"));
            teachers.Add(new Teacher("t1"));

            List<Group> subGroups = new List<Group>();
            subGroups.Add(new Group("g0WFm", 10, 1, 1));
            subGroups.Add(new Group("g0WFk", 15, 1, 2));
            groups.Add(new Group("g0", 25, subGroups));

            roomTypes.Add(new RoomType("C"));

            rooms.Add(new Room("nr0", 40, roomTypes[0]));
            rooms.Add(new Room("nr1", 40, roomTypes[0]));

            subjectTypes.Add(new SubjectType("SPO", 2));

            subjects.Add(new Subject("w-f", subjectTypes[0]));


            foreach (Group g in groups)
            {
                lessons.Add(new Lesson(teachers[0], g.getSubGroup()[0], subjects[0], roomTypes[0], 2));
                lessons.Add(new Lesson(teachers[0], g.getSubGroup()[0], subjects[0], roomTypes[0], 2));

                lessons.Add(new Lesson(teachers[1], g.getSubGroup()[1], subjects[0], roomTypes[0], 2));
                lessons.Add(new Lesson(teachers[1], g.getSubGroup()[1], subjects[0], roomTypes[0], 2));
            }

            SchoolTimetable stt = new SchoolTimetable(teachers, groups, rooms, lessons, 3, 3,config);
            stt.generateSchoolTimetable();
            stt.print();

            foreach (Lesson l in stt.getLessons()) {
                Console.WriteLine(l.ToString());
            }

            Console.WriteLine(stt.isCorrect() + " " + stt.fitness());

            stt.genWeb("subGroupTest");

        }

    }
}

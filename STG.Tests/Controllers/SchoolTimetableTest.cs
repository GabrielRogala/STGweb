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
                teachers.Add(new Teacher(j));
            }
            for (int j = 0; j < 6; j++)
            {
                List<Group> subGroups = new List<Group>();
                int randAmount = new Random().Next(15, 20);
                subGroups.Add(new Group(j+10, randAmount));
                subGroups.Add(new Group(j+10, (25 + j) - randAmount));
                groups.Add(new Group(j, 25 + j, subGroups));

            }

            roomTypes.Add(new RoomType(0));
            roomTypes.Add(new RoomType(1));
            roomTypes.Add(new RoomType(2));

            int nr = 0;
            rooms.Add(new Room(nr++, 25, roomTypes[0]));
            rooms.Add(new Room(nr++, 40, roomTypes[0]));
            rooms.Add(new Room(nr++, 40, roomTypes[0]));
            rooms.Add(new Room(nr++, 40, roomTypes[0]));
            rooms.Add(new Room(nr++, 40, roomTypes[0]));
            rooms.Add(new Room(nr++, 40, roomTypes[1]));
            rooms.Add(new Room(nr++, 40, roomTypes[1]));
            rooms.Add(new Room(nr++, 40, roomTypes[1]));
            rooms.Add(new Room(nr++, 40, roomTypes[2]));
            rooms.Add(new Room(nr++, 40, roomTypes[2]));

            subjectTypes.Add(new SubjectType(0, 7));
            subjectTypes.Add(new SubjectType(1, 8));
            subjectTypes.Add(new SubjectType(2, 6));
            subjectTypes.Add(new SubjectType(3, 9));
            subjectTypes.Add(new SubjectType(4, 3));
            subjectTypes.Add(new SubjectType(5, 3));

            int i = 0;
            subjects.Add(new Subject(0, subjectTypes[0]));
            subjects.Add(new Subject(1, subjectTypes[2]));
            subjects.Add(new Subject(2, subjectTypes[1]));
            subjects.Add(new Subject(3, subjectTypes[0]));
            subjects.Add(new Subject(4, subjectTypes[0]));
            subjects.Add(new Subject(5, subjectTypes[1]));
            subjects.Add(new Subject(6, subjectTypes[1]));
            subjects.Add(new Subject(7, subjectTypes[1]));
            subjects.Add(new Subject(8, subjectTypes[4]));
            subjects.Add(new Subject(9, subjectTypes[5]));
            subjects.Add(new Subject(10, subjectTypes[3]));
            subjects.Add(new Subject(11, subjectTypes[3]));

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
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
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
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
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
            Console.WriteLine(lessons.Count);

            SchoolTimetable stt = new SchoolTimetable(teachers, groups, rooms, lessons, 5, 9,config);
            stt.generateSchoolTimetable();
            stt.print();

            Console.WriteLine("###############################");

            SchoolTimetable stt2 = new SchoolTimetable(teachers, groups, rooms, lessons, 5, 9, config);
            stt2.generateSchoolTimetable();
            stt2.print();

            Console.WriteLine("###############################");

            SchoolTimetable stt3 = new SchoolTimetable(stt);
            //stt3.print();
            stt3.crossover(stt2);
            //stt3.print();

            Console.WriteLine("###############################");

            stt2.mutate();
            stt2.print();

            Console.WriteLine(stt2.isCorrect() +" " + stt2.fitness());
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

            teachers.Add(new Teacher(0));
            teachers.Add(new Teacher(1));

            List<Group> subGroups = new List<Group>();
            subGroups.Add(new Group(0, 10, 1, 1));
            subGroups.Add(new Group(1, 15, 1, 2));
            groups.Add(new Group(2, 25, subGroups));

            roomTypes.Add(new RoomType(1));

            rooms.Add(new Room(0, 40, roomTypes[0]));
            rooms.Add(new Room(1, 40, roomTypes[0]));

            subjectTypes.Add(new SubjectType(0, 2));

            subjects.Add(new Subject(0, subjectTypes[0]));


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
            Console.WriteLine("################");
            SchoolTimetable stt2 = new SchoolTimetable(stt);
            stt2.print();

            foreach (Lesson l in stt.getLessons()) {
                Console.WriteLine(l.ToString());
            }

            Console.WriteLine(stt.isCorrect() + " " + stt.fitness());

            stt.genWeb("subGroupTest");

        }

    }
}

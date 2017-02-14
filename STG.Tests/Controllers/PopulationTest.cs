using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG.Controllers.Engine;

namespace STG.Tests.Controllers
{
    [TestClass]
    public class PopulationTest
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
            STGCfg config = new STGCfg(10, 10, 5, 1, 5, 50);

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
                //max = 27
                //----------pol----------
                tI = 0; sI = 0; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount, 2));
                //----------ang----------
                tI = 1; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------mat----------
                tI = 2; sI++; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------his----------
                tI = 3; sI++; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------wos----------
                tI = 3; sI++; amount = 1;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------fiz----------
                tI = 5; sI++; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------bio----------
                tI = 3; sI++; amount = 1;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------geo----------
                tI = 3; sI++; amount = 2;
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------w-f----------
                tI = 4; sI++; amount = 1;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[0], subjects[sI], roomTypes[2], amount));

                tI = 3; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], roomTypes[2], amount));
                lessons.Add(new Lesson(teachers[tI], g.getSubGroup()[1], subjects[sI], roomTypes[2], amount));

                //----------rel----------
                tI = 5; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[0], amount));
                //----------inf----------
                tI = 4; sI++; amount = 2;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount));
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount));
                //----------pro----------
                tI = 6; sI++; amount = 3;
                lessons.Add(new Lesson(teachers[tI], g, subjects[sI], roomTypes[1], amount, 3));

            }

            Population population = new Population(lessons, teachers, groups, rooms, 3, 9, config);
            population.start();

            SchoolTimetable stt = population.getBestSchoolTimeTable();
            stt.print();

            Console.WriteLine(stt.isCorrect() + " " + stt.fitness());
            stt.genWeb("test1");
        }
    }
}

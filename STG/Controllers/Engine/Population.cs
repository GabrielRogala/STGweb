﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Population
    {
        private List<SchoolTimetable> schoolTimeTables;
        private List<Lesson> lessons;
        private List<Teacher> teachers;
        private List<Group> groups;
        private List<Room> rooms;
        private STGCfg config;
        private int numberOfDays;
        private int numberOfSlots;
        private Random rand = new Random();

        public Population()
        {
            schoolTimeTables = new List<SchoolTimetable>();
            lessons = new List<Lesson>();
            teachers = new List<Teacher>();
            groups = new List<Group>();
            rooms = new List<Room>();
            config = null;
            numberOfDays = 0;
            numberOfSlots = 0;
        }

        public Population(List<Lesson> lessons, List<Teacher> teachers, List<Group> groups, List<Room> rooms, int numberOfDays, int numberOfSlots, STGCfg config) : this()
        {
            this.lessons = lessons;
            this.teachers = teachers;
            this.groups = groups;
            this.rooms = rooms;
            this.config = config;
            this.numberOfDays = numberOfDays;
            this.numberOfSlots = numberOfSlots;
        }

        public void generatePopulation()
        {
            for (int i = 0; i < this.config.populationSize; ++i)
            {

                schoolTimeTables.Add(new SchoolTimetable(teachers, groups, rooms, lessons, numberOfDays,numberOfSlots,config));
                schoolTimeTables[i].generateSchoolTimetable();
                schoolTimeTables[i].fitness();
                while (schoolTimeTables[i].getErrorValue() > 0)
                {
                    schoolTimeTables.RemoveAt(i);
                    schoolTimeTables.Add(new SchoolTimetable(teachers, groups, rooms, lessons, numberOfDays, numberOfSlots, config));
                    schoolTimeTables[i].generateSchoolTimetable();
                    schoolTimeTables[i].fitness();
                }
            }
        }

        public void generateNewPopulation()
        {
            List<SchoolTimetable> schoolTimeTables_tmp = new List<SchoolTimetable>();

            schoolTimeTables.Sort(new Comparison<SchoolTimetable>(schoolTimeTableComparator));
            
            for (int i = 0; i < config.populationSize; ++i)
            {

                if (i < config.populationSize * 0.2)
                {
                    schoolTimeTables_tmp.Add(new SchoolTimetable(schoolTimeTables[i]));
                    if (rand.NextDouble() > config.probabilityOfMutation/100)
                    {
                        //Console.WriteLine("mutate");
                        schoolTimeTables_tmp.Add(new SchoolTimetable(schoolTimeTables[i]));
                        schoolTimeTables_tmp[i + 1].mutate();

                        if (schoolTimeTables_tmp[i].fitness() <= schoolTimeTables_tmp[i + 1].fitness())
                        {
                            schoolTimeTables_tmp.RemoveAt(i + 1);
                            //Console.WriteLine(" ");
                        }
                        else
                        {
                            schoolTimeTables_tmp.RemoveAt(i);
                            //Console.WriteLine(" create best solution ");
                        }
                    }
                }
                else if (i < config.populationSize * 0.8)
                {
                    schoolTimeTables_tmp.Add(schoolTimeTables[i]);
                    schoolTimeTables_tmp[i].crossover(schoolTimeTables[rand.Next(0 , (int)(config.populationSize * 0.2) - 1)]);
                    if (rand.NextDouble() > config.probabilityOfMutation / 100)
                    {
                       schoolTimeTables_tmp[i].mutate();
                    }
                }
                else
                {
                    schoolTimeTables_tmp.Add(new SchoolTimetable(teachers, groups, rooms, lessons, numberOfDays, numberOfSlots, config));
                    schoolTimeTables_tmp[i].generateSchoolTimetable();
                }

                schoolTimeTables_tmp[i].fitness();


                while (schoolTimeTables_tmp[i].getErrorValue() > 0)
                {
                    //Console.WriteLine(i + " must be removed : " + schoolTimeTables[i].getFitnessValue() + " EV: " + schoolTimeTables[i].getErrorValue() + " " + schoolTimeTables[i].isCorrect());
                    schoolTimeTables_tmp.RemoveAt(i);
                    schoolTimeTables_tmp.Add(new SchoolTimetable(teachers, groups, rooms, lessons, numberOfDays, numberOfSlots, config));
                    schoolTimeTables_tmp[i].generateSchoolTimetable();
                    schoolTimeTables_tmp[i].fitness();
                }

            }
            foreach(SchoolTimetable s in schoolTimeTables)
            {
                //s.Free();
            }
            schoolTimeTables.Clear();
            schoolTimeTables = schoolTimeTables_tmp;

            
        }

        public void start()
        {
            Console.WriteLine("School timetable size : " + numberOfSlots + "/" + numberOfDays);
            Console.WriteLine("-------CONFIG------");
            Console.WriteLine("Population size = " + config.populationSize);
            Console.WriteLine("Process time = " + config.epocheSize);
            Console.WriteLine("Probability of mutation = " + config.probabilityOfMutation);
            Console.WriteLine("Number of lesson to positioning = " + config.numberOfLessonToPositioning);
            Console.WriteLine("-------OBJECTS-----");
            Console.WriteLine("Teachers count = " + teachers.Count);
            Console.WriteLine("Groups count = " + groups.Count);
            Console.WriteLine("Rooms count = " + rooms.Count);
            Console.WriteLine("Lessons count = " + lessons.Count);
            Console.WriteLine("==============================================================");

            for (int i = 0; i < config.epocheSize; i++)
            {
                
                if (i == 0)
                {
                    this.generatePopulation();
                }
                else
                {
                    this.generateNewPopulation();
                }

                Console.Write(i);
                schoolTimeTables.Sort(new Comparison<SchoolTimetable>(schoolTimeTableComparator));
                for (int j = 0; j < config.populationSize; ++j)
                {
                    Console.Write(";" + schoolTimeTables[j].getFitnessValue());
                }
                Console.WriteLine("");
                //Console.WriteLine("Best value = " + getBestSchoolTimeTable().getFitnessValue());
                //Console.WriteLine("===========================================");
            }
        }

        public int schoolTimeTableComparator(SchoolTimetable l1, SchoolTimetable l2)
        {
            return l1.getFitnessValue().CompareTo(l2.getFitnessValue());
        }

        public SchoolTimetable getBestSchoolTimeTable()
        {
            schoolTimeTables.Sort(new Comparison<SchoolTimetable>(schoolTimeTableComparator));
            return schoolTimeTables[0];
        }
    }
}
using System;
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
                //schoolTimeTables[i].setId(i);
                //Console.WriteLine(i + ": " + "[" + schoolTimeTables[i].getId() + "] " + schoolTimeTables[i].fitness());
            }
        }

        public void generateNewPopulation()
        {
            List<SchoolTimetable> schoolTimeTables_tmp = new List<SchoolTimetable>();

            schoolTimeTables.Sort(new Comparison<SchoolTimetable>(schoolTimeTableComparator));
            //Console.WriteLine("gnp---------------------------v");
            for (int i = 0; i < config.populationSize; ++i)
            {
                //  Console.WriteLine(i + ": " + "[" + schoolTimeTables[i].getId() + "] " + schoolTimeTables[i].getValue());
            }
            //Console.WriteLine("------------------------------");
            for (int i = 0; i < config.populationSize; ++i)
            {

                if (i < config.populationSize * 0.2)
                {
                    //schoolTimeTables_tmp.Add(new SchoolTimetable(lessons, teachers, groups, schoolTimeTables[i]));
                    if (rand.NextDouble() > 0.1)
                    {

                        //schoolTimeTables_tmp.Add(new SchoolTimetable(lessons, teachers, groups, schoolTimeTables[i]));
                        //schoolTimeTables_tmp[i + 1].mutate();

                        if (schoolTimeTables_tmp[i].fitness() <= schoolTimeTables_tmp[i + 1].fitness())
                        {
                            schoolTimeTables_tmp.RemoveAt(i + 1);
                        }
                        else
                        {
                            schoolTimeTables_tmp.RemoveAt(i);
                        }
                    }
                }
                else if (i < config.populationSize * 0.8)
                {
                    //schoolTimeTables_tmp.Add(new SchoolTimetable(lessons, teachers, groups));
                    //schoolTimeTables_tmp[i].crossSchoolTimeTables(schoolTimeTables[i], schoolTimeTables[rand.Next((int)(size * 0.2))]);
                    if (rand.NextDouble() > 0.1)
                    {
                       // schoolTimeTables_tmp[i].mutate();
                    }
                }
                else
                {
                    //schoolTimeTables_tmp.Add(new SchoolTimeTable(lessons, teachers, groups));
                    //schoolTimeTables_tmp[i].generateSchoolTimeTable();
                }

                //schoolTimeTables_tmp[i].setId(i);
                schoolTimeTables_tmp[i].fitness();
            }

            for (int i = 0; i < config.populationSize; ++i)
            {
                //  Console.WriteLine(i + ": "+"["+ schoolTimeTables_tmp[i].getId()+ "] " + schoolTimeTables_tmp[i].fitness());
            }

            schoolTimeTables = schoolTimeTables_tmp;

            //Console.WriteLine("gnp---------------------------^");
        }

        public void start()
        {
            for (int i = 0; i < config.epocheSize; ++i)
            {
                Console.WriteLine("E:" + i + "-------------------------");
                this.generateNewPopulation();
                Console.WriteLine(getBestSchoolTimeTable().fitness());
            }
        }

        public int schoolTimeTableComparator(SchoolTimetable l1, SchoolTimetable l2)
        {
            return 0;
            //return l1.getValue().CompareTo(l2.getValue());
        }

        public SchoolTimetable getBestSchoolTimeTable()
        {
            schoolTimeTables.Sort(new Comparison<SchoolTimetable>(schoolTimeTableComparator));
            return schoolTimeTables[0];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Teacher
    {
        private int name;
        private Timetable timetable;

        public Teacher()
        {
            this.name = 0;
        }

        public Teacher(int name) : this()
        {
            this.name = name;
        }

        public int getName()
        {
            return name;
        }

        public void setTimetable(Timetable timetable)
        {
            this.timetable = timetable;
        }

        public Timetable getTimetable()
        {
            return timetable;
        }

        public override string ToString()
        {
            return "t"+name;
        }

        public override bool Equals(object obj)
        {
            return this.name.Equals(((Teacher)obj).name);
        }
    }
}
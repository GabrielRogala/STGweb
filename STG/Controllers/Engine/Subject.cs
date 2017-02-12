using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Subject
    {
        private String name;
        private SubjectType subjectType;
        //private RoomType roomType;

        public Subject()
        {
            this.name = "name";
            this.subjectType = null;
            //this.roomType = null;
        }

        //public Subject(string name, SubjectType subjectType, RoomType roomType) : this()
        //{
        //    this.name = name;
        //    this.subjectType = subjectType;
        //    this.roomType = roomType;
        //}

        public Subject(string name, SubjectType subjectType) : this()
        {
            this.name = name;
            this.subjectType = subjectType;
        }

        public String getName() {
            return name;
        }

        public SubjectType getSubjectType() {
            return subjectType;
        }

        //public RoomType getRoomType() {
        //    return roomType;
        //}

        public override string ToString()
        {
            return name + "(" +subjectType.getName()+ ")";
        }
    }
}
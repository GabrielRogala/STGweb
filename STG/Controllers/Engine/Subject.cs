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

        public Subject()
        {
            this.name = "name";
            this.subjectType = null;
        }

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

        public override string ToString()
        {
            return name + "(" +subjectType.getName()+ ")";
        }
    }
}
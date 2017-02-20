using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Subject
    {
        private int name;
        private SubjectType subjectType;

        public Subject()
        {
            this.name = 0;
            this.subjectType = null;
        }

        public Subject(int name, SubjectType subjectType) : this()
        {
            this.name = name;
            this.subjectType = subjectType;
        }

        public int getName() {
            return name;
        }

        public SubjectType getSubjectType() {
            return subjectType;
        }

        public override string ToString()
        {
            return "s"+name + "(" +"st"+subjectType.getName()+ ")";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class SubjectType
    {
        private int priority;
        private int name;

        public SubjectType(int name, int priority) {
            this.name = name;
            this.priority = priority;
        }

        public int getName() {
            return name;
        }

        public int getPriority() {
            return priority;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class SubjectType
    {
        private int priority;
        private string name;

        public SubjectType(string name, int priority) {
            this.name = name;
            this.priority = priority;
        }

        public string getName() {
            return name;
        }

        public int getPriority() {
            return priority;
        }
    }
}
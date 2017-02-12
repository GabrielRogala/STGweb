using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class RoomType
    {
        private string name;

        public RoomType(string name) {
            this.name = name;
        }

        public string getName() {
            return name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class RoomType
    {
        private int name;

        public RoomType(int name) {
            this.name = name;
        }

        public int getName() {
            return name;
        }
    }
}
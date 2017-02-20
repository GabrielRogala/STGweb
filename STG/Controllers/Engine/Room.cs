using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Room
    {
        private int name;
        private Timetable timetable;
        private int amount;
        private RoomType roomType;

        public Room()
        {
            this.name = 0;
            this.amount = 0;
            this.roomType = null;
        }

        public Room(int name, int amount, RoomType roomType) : this()
        {
            this.name = name;
            this.amount = amount;
            this.roomType = roomType;
        }

        public int getName()
        {
            return name;
        }

        public int getAmount()
        {
            return amount;
        }

        public RoomType getRoomType() {
            return roomType;
        }

        public void setTimetable(Timetable timetable)
        {
            this.timetable = timetable;
        }

        public Timetable getTimetable()
        {
            return timetable;
        }

        public override bool Equals(object obj)
        {
            return name.Equals(((Room)obj).getName());
        }

        public override string ToString()
        {
            return "r"+name;
            //return "r"+name+"("+amount+"/"+"rt"+roomType.getName()+")";
        }
    }
}
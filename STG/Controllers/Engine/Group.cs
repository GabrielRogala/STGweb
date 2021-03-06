﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Group
    {
        private int name;
        private Timetable timetable;
        private int amount;

        private List<Group> subGroup;
        private Group parent;
        private int subGroupsIndex;
        private int subGroupId;

        public Group()
        {
            this.name = 0;
            this.amount = 0;
            this.subGroup = new List<Group>();
            this.subGroupsIndex = 0;
            this.subGroupId = 0;
        }

        public Group(int name, int amount): this()
        {
            this.name = name;
            this.amount = amount;
        }

        public Group(int name, int amount, int subGroupsIndex, int subGroupId) : this(name, amount)
        {
            this.subGroupsIndex = subGroupsIndex;
            this.subGroupId = subGroupId;
        }

        public Group(int name, int amount, List<Group> subGroup): this(name,amount) {
            this.subGroup = subGroup;
            foreach (Group g in this.subGroup) {
                g.setParent(this);
            }
        }

        public Group(Group gr):this(gr.getName(),gr.getAmount(),gr.getSubGroupsIndex(),gr.getSubGroupId())
        {

        }

        public void setName(int name) {
            this.name = name;
        }

        public List<Group> getSubGroup() {
            return subGroup;
        }

        public void setParent(Group group) {
            this.parent = group;
        }

        public Group getParent()
        {
            return parent;
        }

        public TimeSlot getSubGroupFreeSlotToLesson(Lesson lesson) {
            if (parent != null)
            {
                foreach (Group g in parent.getSubGroup()) {

                    if (lesson.getGroup().getSubGroupsIndex() == g.getSubGroupsIndex() && 
                        !g.Equals(lesson.getGroup())) {
                        // podgrupy tejsamej kalegorii

                        if (g.getTimetable().getSlotsWithLesson().Count > 0) {
                            foreach(TimeSlot ts in g.getTimetable().getSlotsWithLesson() )
                            {
                                if (lesson.getGroup().getTimetable().getLessons(ts.day,ts.hour).Count == 0) {
                                    return ts;
                                }
                            }
                        }

                    }
                }
            }

            return null;
        }

        public int getSubGroupsIndex()
        {
            return subGroupsIndex;
        }

        public void setSubGroupsIndex(int subGroupsIndex)
        {
            this.subGroupsIndex = subGroupsIndex;
        }

        public int getSubGroupId()
        {
            return subGroupId;
        }

        public void setSubGroupId(int subGroupId)
        {
            this.subGroupId = subGroupId;
        }

        public int getName() {
            return name;
        }

        public int getAmount() {
            return amount;
        }

        public void setTimetable(Timetable timetable) {
            this.timetable = timetable;
        }

        public Timetable getTimetable() {
            return timetable;
        }

        public void addLesson(Lesson lesson,int day , int slot) {
            if (parent != null) {
                parent.getTimetable().addLesson(lesson, day, slot);
            } 
            this.timetable.addLesson(lesson,day,slot);
        }

        public void removeLesson(Lesson lesson, int day, int slot)
        {
            if (parent != null)
            {
                parent.getTimetable().removeLesson(lesson, day, slot);
            }
            else if (subGroup.Count > 0)
            {
                foreach (Group g in subGroup)
                {
                    g.getTimetable().removeLesson(lesson, day, slot);
                }
            }
            this.timetable.removeLesson(lesson, day, slot);
        }

        public override string ToString()
        {
            String tmp = "";
            if (parent != null) {
                tmp += subGroupsIndex + ":" + subGroupId;
            }
            return "g"+name + "(" + amount + ")" + tmp;
        }

        public override bool Equals(object obj)
        {
            return this.name.Equals(((Group)obj).name);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Lesson
    {
        private Subject subject;
        private int amount;
        private int size;
        private Group group;
        private Teacher teacher;
        private Room room;
        private RoomType roomType;
        private List<TimeSlot> slots;

        public Lesson()
        {
            this.subject = null;
            this.amount = 0;
            this.size = 0;
            this.group = null;
            this.teacher = null;
            slots = new List<TimeSlot>();
        }

        public Lesson(Teacher teacher, Group group, Subject subject, RoomType roomType, int amount) : this(teacher, group, subject, roomType, amount,1) {}

        public Lesson(Teacher teacher, Group group, Subject subject, RoomType roomType, int amount, int size): this()
        {
            this.subject = subject;
            this.amount = amount;
            this.size = size;
            this.group = group;
            this.teacher = teacher;
            this.roomType = roomType;
        }

        public RoomType getRoomType() {
            return roomType;
        }

        public Group getGroup() {
            return group;
        }

        public Teacher getTeacher() {
            return teacher;
        }

        public Room getRoom() {
            return room;
        }

        public void setRoom(Room room) {
            this.room = room;
        }

        public Subject getSubject() {
            return subject;
        }

        public int getSize() {
            return size;
        }

        public int getAmount() {
            return amount;
        }

        public void addSlot(TimeSlot slot) {
            slots.Add(slot);
        }

        public List<TimeSlot> getSlots() {
            return slots;
        }

        public void removeAllSlots() {
            slots.Clear();
        }

        public override string ToString()
        {
            String tmp = "";
            if (room != null)
            {
                tmp = ":" + room.ToString();
            }
            else {
                tmp = ":" + roomType.getName();
            }

            foreach (TimeSlot ts in slots) {
                tmp += ts.ToString();
            }

            return group.ToString() + "/" + teacher.ToString() + "/" + subject.ToString() + "(" + amount + "/" + size + ")" + tmp;
        }

        internal bool isDifferent(Lesson lesson)
        {
            return !this.subject.Equals(lesson.getSubject());
        }

        public Boolean Equals(Lesson lesson)
        {
            return this.subject.Equals(lesson.subject) && this.group.Equals(lesson.group) && this.teacher.Equals(lesson.teacher) && this.amount.Equals(lesson.amount) && this.size.Equals(lesson.size);
        }

        public void addLessonToTimetable(int day, int hour)
        {
            slots.Add(new TimeSlot(day, hour));
            this.getGroup().addLesson(this, day,hour);
            this.getTeacher().getTimetable().addLesson(this, day, hour);
            this.getRoom().getTimetable().addLesson(this, day, hour);
        }

        public void removeLessonFromTimetable(int day, int hour) {
            this.getGroup().removeLesson(this, day, hour);
            this.getTeacher().getTimetable().removeLesson(this, day, hour);
            this.getRoom().getTimetable().removeLesson(this, day, hour);
            slots.Remove(new TimeSlot(day, hour));
        }

        public void addLessonToTimetableAndBlockSlot(int day, int hour)
        {
            addLessonToTimetable(day, hour);
            lockSlots(day, hour);
        }

        public void lockSlots(int day, int hour)
        {
            this.getGroup().getTimetable().lockSlot(day, hour);
            this.getTeacher().getTimetable().lockSlot(day, hour);
            this.getRoom().getTimetable().lockSlot(day, hour);
        }
    }
}
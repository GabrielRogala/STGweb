using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Slot
    {
        private List<Lesson> lessons;
        private bool locked;
        private bool permLock;

        public Slot() {
            lessons = new List<Lesson>();
            locked = false;
            permLock = false;
        }

        public void addLesson(Lesson lesson) {
            lessons.Add(lesson);
        }

        public void removeLesson(Lesson lesson) {
            lessons.Remove(lesson);
        }

        public void remoweAllLessons() {
            lessons.Clear();
        }

        public Lesson getLesson(int id) {
            return lessons[id];
        }

        public List<Lesson> getLessons() {
            return lessons;
        }

        public void setPermLock(bool permLock) {
            this.permLock = permLock;
        }

        public bool getPermLock() {
            return permLock;
        }

        public void lockSlot() {
            locked = true;
        }

        public void unlockSlot() {
            locked = false;
        }

        public bool isLocked() {
            return locked || permLock;
        }

        public bool isEmpty()
        {
            return lessons.Count == 0;
        }
    }
}
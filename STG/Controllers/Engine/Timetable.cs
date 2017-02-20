using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace STG.Controllers.Engine
{
    public class Timetable
    {
        private List<Day> days;
        private Group group;
        private Teacher teacher;
        private Room room;
        private int numberOfDays = 5;
        private int numberOfSlots = 9;

        public Timetable()
        {
            days = new List<Day>();
            for (int i = 0; i < numberOfDays; ++i) {
                days.Add(new Day((i+1), numberOfSlots));
            }
            group = null;
            teacher = null;
            room = null;
        }

        public Timetable(int numberOfDays, int numberOfSlots) 
        {
            this.numberOfDays = numberOfDays;
            this.numberOfSlots = numberOfSlots;
            days = new List<Day>();
            for (int i = 0; i < this.numberOfDays; ++i)
            {
                days.Add(new Day((i + 1), this.numberOfSlots));
            }
            group = null;
            teacher = null;
            room = null;
        }

        public Timetable(Group group): this()
        {
            this.group = group;
            this.group.setTimetable(this);
        }

        public Timetable(Teacher teacher): this()
        {
            this.teacher = teacher;
            this.teacher.setTimetable(this);
        }

        public Timetable(Room room): this()
        {
            this.room = room;
            this.room.setTimetable(this);
        }

        public Timetable(Group group, int numberOfDays, int numberOfSlots) : this(numberOfDays, numberOfSlots)
        {
            this.group = group;
            this.group.setTimetable(this);
        }

        public Timetable(Teacher teacher, int numberOfDays, int numberOfSlots) : this(numberOfDays, numberOfSlots)
        {
            this.teacher = teacher;
            this.teacher.setTimetable(this);
        }

        public Timetable(Room room, int numberOfDays, int numberOfSlots) : this(numberOfDays, numberOfSlots)
        {
            this.room = room;
            this.room.setTimetable(this);
        }

        public Group getGroup() {
            return group;
        }

        public Teacher getTeacher()
        {
            return teacher;
        }

        public Room getRoom()
        {
            return room;
        }

        public List<Day> getDays() {
            return days;
        }

        public Lesson getLesson(int day, int slot)
        {
            return days[day].getSlot(slot).getLesson(0);
        }

        public List<Lesson> getLessons(int day, int slot) {
            return days[day].getSlot(slot).getLessons();
        }

        public void addLesson(Lesson lesson, int day, int slot) {
            days[day].getSlot(slot).addLesson(lesson);
        }

        public void removeLesson(Lesson lesson, int day, int slot)
        {
            days[day].getSlot(slot).removeLesson(lesson);
        }

        public void removeAllLessons(int day, int slot)
        {
            days[day].getSlot(slot).remoweAllLessons();
        }

        public void lockSlot(int day, int slot) {
            days[day].getSlot(slot).lockSlot();
        }

        public void unlockSlot(int day, int slot)
        {
            days[day].getSlot(slot).unlockSlot();
        }

        public void unlockAllSlots() {
            foreach (Day d in days) {
                foreach (Slot s in d.getSlots()) {
                    s.unlockSlot();
                }
            }
        }

        public override string ToString()
        {
            String tmp = "";
            if (group != null) {
                tmp += "\n======================" + group.getName() + "======================\n";
            } else if (teacher != null) {
                tmp += "\n======================" + teacher.getName() + "======================\n";
            } else if (room != null) {
                tmp += "\n======================" + room.getName() + "======================\n";
            } else {
                tmp += "\n============================================\n";
            }
            tmp += "\t";
            for (int d = 0; d < numberOfDays; ++d)
            {
                tmp += "|________________" + days[d].getName()+ "_________________";
            }
            tmp += "|\n";
            for (int h = 0; h < numberOfSlots; ++h)
            {
                tmp += h + "\t";
                for (int d = 0; d < numberOfDays; ++d) {
                    if (getLessons(d, h).Count > 0) {
                        tmp += "|";
                        for (int i = 0; i < getLessons(d, h).Count; i++) {
                            tmp += " " + getLesson(i, d, h).ToString() + " ";
                        }
                    } else {
                        tmp += "| ----------------------------------- ";
                    }
                }
                tmp += "|\n";
            }
            tmp += "============================================\n";
            tmp += "fitness : " + fitness() + "\n";
            return tmp.Replace("\n",Environment.NewLine); ;
        }

        private Lesson getLesson(int i, int day, int slot)
        {
            return days[day].getSlot(slot).getLesson(i);
        }

        public List<TimeSlot> getFreeSlotsToLesson(Lesson lesson)
        {
            List<TimeSlot> freeSlots = new List<TimeSlot>();

            if (group != null && group.getParent() != null)
            {       
                List<TimeSlot> fs = group.getTimetable().getFreeSlots(lesson);
                List<TimeSlot> es = group.getTimetable().getSlotsWithLesson();
                List<TimeSlot> groupFS = this.getFreeSlotsWithSubGroupLessons(lesson);
                
                freeSlots = groupFS;

                //int counter = 0;
                foreach (Group g in group.getParent().getSubGroup())
                {
                    if (group.getSubGroupsIndex() == g.getSubGroupsIndex())
                    {

                        List<TimeSlot> fsT = g.getTimetable().getFreeSlots(lesson);
                        List<TimeSlot> esT = g.getTimetable().getSlotsWithLesson();

                        if (group == g)
                        {
                            freeSlots = this.getTheSameSlots(freeSlots,fs);
                        } else {
                            if (esT.Count > es.Count)
                            {
                                freeSlots = this.getTheSameSlots(freeSlots, esT);
                            } else {
                                freeSlots = this.getTheSameSlots(freeSlots, fsT);
                            }
                        }



                        //List<TimeSlot> freeslotsTMP = new List<TimeSlot>();

                        //if (counter > 0)
                        //{
                        //    List<TimeSlot> slots = this.getSlotsWithLesson();
                        //    if (slots.Count > 0)
                        //    {
                        //        freeslotsTMP = getTheSameSlots(slots, group.getParent().getTimetable().getFreeSlotsToLesson(lesson));
                        //    }
                        //    else
                        //    {
                        //        freeslotsTMP = group.getParent().getTimetable().getFreeSlotsToLesson(lesson);
                        //    }

                        //    freeSlots = this.getTheSameSlots(freeSlots, freeslotsTMP);

                        //}
                        //else
                        //{
                        //    List<TimeSlot> slots = this.getSlotsWithLesson();
                        //    if (slots.Count > 0)
                        //    {
                        //        freeSlots = getTheSameSlots(slots, group.getParent().getTimetable().getFreeSlotsToLesson(lesson));
                        //    }
                        //    else
                        //    {
                        //        freeSlots = group.getParent().getTimetable().getFreeSlotsToLesson(lesson);
                        //    }
                        //}
                        //
                        //counter++;
                    }

                }
            } else {
                freeSlots = getFreeSlots(lesson);
            }


            return freeSlots;
        }

        public List<TimeSlot> getFreeSlots(Lesson lesson)
        {
            List<TimeSlot> freeSlots = new List<TimeSlot>();

            for (int h = 0; h < numberOfSlots - (lesson.getSize() - 1); ++h)
            {
                for (int d = 0; d < numberOfDays; ++d)
                {

                    bool result = true;

                    for (int i = 0; i < lesson.getSize(); ++i)
                    {
                        result = result && days[d].getSlot(h + i).isEmpty() && !days[d].getSlot(h + i).isLocked();
                    }

                    if (result)
                    {
                        freeSlots.Add(new TimeSlot(d, h));
                    }
                }
            }

            return freeSlots;
        }

        public List<TimeSlot> getFreeSlotsWithSubGroupLessons(Lesson lesson)
        {
            List<TimeSlot> freeSlots = new List<TimeSlot>();

            for (int h = 0; h < numberOfSlots; ++h)
            {
                for (int d = 0; d < numberOfDays; ++d)
                {
                    if((days[d].getSlot(h).isEmpty() 
                        || ( getLessons(d,h)[0].getGroup().getParent()!=null 
                        && getLessons(d, h)[0].getGroup().getSubGroupsIndex() == lesson.getGroup().getSubGroupsIndex()) ) 
                        && !days[d].getSlot(h).isLocked()) {
                        freeSlots.Add(new TimeSlot(d, h));
                    }
                }
            }

            return freeSlots;
        }

        public int fitness()
        {
            int value = 0;
            if (teacher != null)
            {
                value += fitnessFreeSlots();
            }

            if (group != null)
            {
                value += fitnessType();
                value += fitnessSubject();
                value += fitnessFreeSlots();
            }

            return value;
        }

        public int fitnessFreeSlots()
        {
            int value = 0;
            foreach (Day d in days)
            {
                int lastIndex = -1;
                for (int i = 0; i < d.getSlots().Count; i++)
                {
                    if (!d.getSlot(i).isEmpty() || d.getSlot(i).isLocked())
                    {
                        value += (int)Math.Pow((i - lastIndex - 1), 3);
                        lastIndex = i;
                    }

                }
            }

            return value;
        }

        public int fitnessType()
        {
            int value = 0;
            List<Dictionary<SubjectType, int>> maps = new List<Dictionary<SubjectType, int>>();
            Dictionary<SubjectType, int> mapsAll = new Dictionary<SubjectType, int>();

            if (this.group != null)
            {
                foreach (Day d in days)
                {                

                    maps.Add(new Dictionary<SubjectType, int>());
                    
                    for (int i = 0; i < d.getSlots().Count;)
                    {
                        if (!d.getSlots()[i].isEmpty())
                        {
                            foreach (Lesson l in d.getSlots()[i].getLessons())
                            {
                                if (maps.Last().ContainsKey(l.getSubject().getSubjectType()))
                                {
                                    maps.Last()[l.getSubject().getSubjectType()]++;
                                }
                                else
                                {
                                    maps.Last().Add(l.getSubject().getSubjectType(), 1);
                                }

                                if (mapsAll.ContainsKey(l.getSubject().getSubjectType()))
                                {
                                    mapsAll[l.getSubject().getSubjectType()]++;
                                }
                                else
                                {
                                    mapsAll.Add(l.getSubject().getSubjectType(), 1);
                                }
                            }
                            i += d.getSlots()[i].getLessons()[0].getSize();
                        }
                        else {
                            i++;
                        }
                    }
                }


                foreach (Dictionary<SubjectType, int> map in maps) {
                    foreach (KeyValuePair<SubjectType, int> m in map)
                    {
                        if (m.Value > 0)
                        {
                            
                            value += (int)Math.Pow(2, Math.Abs(m.Value - (mapsAll[m.Key]/days.Count) )) - 1;
                        }
                    }
                }
                
            }
            return value;
        }
        
        public int fitnessSubject() {
            int value = 0;
            Dictionary<Subject, int> maps = new Dictionary<Subject, int>();

            foreach (Day d in days) {
                for (int i = 0; i< d.getSlots().Count;) {
                   
                    if (!d.getSlots()[i].isEmpty())
                    {
                        foreach (Lesson l in d.getSlots()[i].getLessons())
                        {
                            if (maps.ContainsKey(l.getSubject()))
                            {
                                maps[l.getSubject()]++;
                            }
                            else
                            {
                                maps.Add(l.getSubject(), 0);
                            }
                        }
                        i += d.getSlots()[i].getLessons()[0].getSize();
                    } else {
                        i++;
                    }
                }

                foreach (KeyValuePair<Subject,int> map in maps) {
                    if (map.Value > 0) {
                        value += (int)Math.Pow(10,map.Value);
                    }
                }

                maps.Clear();
            }

            return value;
        }

        public List<TimeSlot> getSlotsWithLesson(int size = 1)
        {
            List<TimeSlot> slots = new List<TimeSlot>();

            for (int h = 0; h < numberOfSlots; ++h)
            {
                for (int d = 0; d < numberOfDays; ++d)
                {
                    if (!days[d].getSlot(h).isEmpty() && !days[d].getSlot(h).isLocked() && days[d].getSlot(h).getLesson(0).getSize() == 1)
                    {
                        slots.Add(new TimeSlot(d, h));
                    }
                }
            }

            return slots;
        }

        public List<TimeSlot> getTheSameSlots(List<TimeSlot> freeSlots1, List<TimeSlot> freeSlots2)
        {
            List<TimeSlot> freeSlot = new List<TimeSlot>();

            foreach (TimeSlot ts in freeSlots1)
            {
                if (freeSlots2.Contains(ts))
                {
                    freeSlot.Add(new TimeSlot(ts.day, ts.hour));
                }
            }

            return freeSlot;
        }

        public List<TimeSlot> getSlotsWithLesson()
        {
            List<TimeSlot> slots = new List<TimeSlot>();
            for (int d = 0; d < numberOfDays; ++d)
            {
                for (int h = 0; h < numberOfSlots; ++h)
                {
                    if (this.getLessons(d, h).Count > 0 && this.getLessons(d, h)[0] != null)
                    {
                        slots.Add(new TimeSlot(d, h));
                    }
                }
            }

            return slots;
        }

        public List<Lesson> getAllLessonsWithTimetable()
        {
            List<Lesson> tmpLessons = new List<Lesson>();

            for (int h = 0; h < numberOfSlots; ++h)
            {
                for (int d = 0; d < numberOfDays; ++d)
                {
                    if (!days[d].getSlot(h).isEmpty())
                    {
                        tmpLessons.AddRange(days[d].getSlot(h).getLessons());
                    }
                }
            }

            return tmpLessons;
        }
    }
}
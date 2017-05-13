using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class SchoolTimetable
    {
        private List<Teacher> teachers;
        private List<Group> groups;
        private List<Room> rooms;
        private List<Lesson> lessons;
        private int numberOfDays = 5;
        private int numberOfSlots = 9;
        private List<Timetable> teachersTimetables;
        private List<Timetable> groupsTimetables;
        private List<Timetable> roomsTimetables;
        private STGCfg config;
        private static Random rand = new Random();
        private int fitnessValue;
        private int errorValue;

        public SchoolTimetable() {
            teachers = new List<Teacher>();
            groups = new List<Group>();
            rooms = new List<Room>();
            lessons = new List<Lesson>();
            teachersTimetables = new List<Timetable>();
            groupsTimetables = new List<Timetable>();
            roomsTimetables = new List<Timetable>();
            config = null;
            fitnessValue = 0;
            errorValue = 0;
        }

        public void Free() {
            teachers.Clear();
            groups.Clear();
            rooms.Clear();
            lessons.Clear();
            teachersTimetables.Clear();
            groupsTimetables.Clear();
            roomsTimetables.Clear();
        }

        public SchoolTimetable(List<Teacher> teachers, List<Group> groups, List<Room> rooms, List<Lesson> lessons) : this()
        {

            foreach (Group g in groups)
            {
                Group gr = new Group(g);
                List<Group> sgr = new List<Group>();

                if (g.getSubGroup().Count > 0)
                {
                    foreach (Group s in g.getSubGroup())
                    {
                        Group sg = new Group(s);
                        sg.setParent(gr);
                        sgr.Add(sg);
                    }
                }
                gr.getSubGroup().AddRange(sgr);
                this.groups.Add(gr);
            }

            foreach (Teacher t in teachers)
            {
                this.teachers.Add(new Teacher(t.getName()));
            }

            foreach (Room r in rooms)
            {
                this.rooms.Add(new Room(r.getName(), r.getAmount(), new RoomType(r.getRoomType().getName())));
            }

            //this.groups = groups;
            //this.teachers = teachers;
            //this.rooms = rooms;

            foreach (Lesson l in lessons) {
                Teacher te = null;
                Group gr = null;

                foreach (Group g in this.groups)
                {
                    if (g.Equals(l.getGroup()))
                    {
                        gr = g;
                        break;
                    } else {
                        if (g.getSubGroup().Count > 0) {
                            foreach (Group sg in g.getSubGroup())
                            {
                                if (sg.Equals(l.getGroup()))
                                {
                                    gr = sg;
                                    break;
                                }
                            }
                        }
                    }

                }

                foreach (Teacher t in this.teachers)
                {
                    if (t.Equals(l.getTeacher())) {
                        te = t;
                        break;
                    }
                }

                this.lessons.Add(
                    new Lesson(
                        te,
                        gr,
                        new Subject(
                            l.getSubject().getName(),
                            new SubjectType(
                                l.getSubject().getSubjectType().getName(),
                                l.getSubject().getSubjectType().getPriority()
                            )
                        ),
                        new RoomType(
                            l.getRoomType().getName()
                        ),
                        l.getAmount(),l.getSize()    
                    )
                );
            }
        }

        public SchoolTimetable(List<Teacher> teachers, List<Group> groups, List<Room> rooms, List<Lesson> lessons, int numberOfDays, int numberOfSlots, STGCfg config) : this(teachers, groups, rooms, lessons)
        {
            this.config = config;
            this.numberOfDays = numberOfDays;
            this.numberOfSlots = numberOfSlots;
        }

        public SchoolTimetable(SchoolTimetable s) : this(s.getTeachers(), s.getGroups(), s.getRooms(), s.getLessons(), s.getNumberOfDays(), s.getNumberOfSlots() ,s.getConfig())
        {
            generateGroupsTimetables(groups);
            generateRoomsTimetables(rooms);
            generateTeachersTimetables(teachers);
            unlockAllSlots();

            for (int i = 0; i < s.getLessons().Count; i++) {
                List<TimeSlot> ts = s.getLessons()[i].getSlots();

                if (s.getLessons()[i].getRoom() != null)
                {
                    Room room = this.rooms[s.getRooms().IndexOf(s.getLessons()[i].getRoom())];

                    this.getLessons()[i].setRoom(room);
                    foreach (TimeSlot t in ts)
                    {
                        this.getLessons()[i].addLessonToTimetable(t.day, t.hour);
                    }
                }
            }
            
        }

        public int getFitnessValue() {
            return fitnessValue;
        }

        public int getErrorValue() {
            return errorValue;
        }

        public STGCfg getConfig() {
            return config;
        }

        public int getNumberOfDays() {
            return numberOfDays;
        }

        public int getNumberOfSlots() {
            return numberOfSlots;
        }

        public List<Teacher> getTeachers() {
            return teachers;
        }

        public List<Group> getGroups()
        {
            return groups;
        }

        public List<Room> getRooms()
        {
            return rooms;
        }

        public List<Lesson> getLessons()
        {
            return lessons;
        }

        public void generateTeachersTimetables(List<Teacher> teachers) {
            foreach (Teacher t in teachers) {
                teachersTimetables.Add(new Timetable(t, numberOfDays,numberOfSlots));
            }
        }

        public void generateGroupsTimetables(List<Group> groups)
        {
            foreach (Group g in groups)
            {
                if (g.getSubGroup().Count > 0) {
                    foreach (Group sg in g.getSubGroup())
                    {
                        groupsTimetables.Add(new Timetable(sg, numberOfDays, numberOfSlots));
                    }
                }
                groupsTimetables.Add(new Timetable(g, numberOfDays, numberOfSlots));
            }
        }

        public void generateRoomsTimetables(List<Room> rooms)
        {
            foreach (Room r in rooms)
            {
                roomsTimetables.Add(new Timetable(r, numberOfDays, numberOfSlots));
            }
        }

        public void generateSchoolTimetable() {
            generateGroupsTimetables(groups);
            generateRoomsTimetables(rooms);
            generateTeachersTimetables(teachers);
            genereteTimetable(lessons);
            unlockAllSlots();
        }

        public void genereteTimetable(List<Lesson> lessons)
        {
            List<Lesson> tmpLessons = new List<Lesson>();
            List<Lesson> choosenLesson = new List<Lesson>();

            foreach (Lesson l in lessons) {
                tmpLessons.Add(l);
            }

            sortLessons(tmpLessons);

            while (tmpLessons.Count > 0) {
                choosenLesson.AddRange(findDifferentSubjectTheSameGroup(tmpLessons.Last(), tmpLessons));

                findAndSetLessonsInBestPosition(choosenLesson, tmpLessons);

                foreach (Lesson l in choosenLesson) {
                    tmpLessons.Remove(l);
                }
                choosenLesson.Clear();
            }

        }

        private void findAndSetLessonsInBestPosition(List<Lesson> choosenLesson, List<Lesson> allLesson)
        {
            List<FreeSlotsToLesson> freeSlotsToLesson = new List<FreeSlotsToLesson>();
            Timetable currentTimeTable = new Timetable();
            List<TimeSlot> freeSlots = new List<TimeSlot>();
            List<TimeSlot> groupSlots = new List<TimeSlot>();
            List<TimeSlot> teacherSlots = new List<TimeSlot>();
            List<TimeSlot> theSameSlots = new List<TimeSlot>();
            Group group;
            Teacher teacher;

            foreach (Lesson l in choosenLesson)
            {
                group = l.getGroup();
                teacher = l.getTeacher();

                // przygotowanie listy wolnych slotów dla odpowiednich sal
                List<FreeSlotsInRoomToLesson> freeSlotsInRoomToLesson = new List<FreeSlotsInRoomToLesson>();
                foreach (Timetable tt in roomsTimetables)
                {
                    if (tt.getRoom().getRoomType().getName() == l.getRoomType().getName() && tt.getRoom().getAmount() >= group.getAmount())
                    {
                        freeSlotsInRoomToLesson.Add(new FreeSlotsInRoomToLesson(tt.getFreeSlotsToLesson(l), tt.getRoom()));
                    }
                }

                groupSlots = group.getTimetable().getFreeSlotsToLesson(l);
                teacherSlots = teacher.getTimetable().getFreeSlotsToLesson(l);
                theSameSlots = getTheSameSlots(groupSlots, teacherSlots);

                freeSlotsToLesson.Add(new FreeSlotsToLesson(theSameSlots, l, freeSlotsInRoomToLesson));

                // przygotowanie planu lekcji z wszystkimi wolnymi slotami dla wybranych lekcji
                //foreach(TimeSlot ts in freeSlotsToLesson.Last().getSlots())
                foreach (TimeSlot ts in theSameSlots)
                {
                    currentTimeTable.addLesson(l, ts.day, ts.hour);
                    if (!freeSlots.Contains(ts))
                    {
                        freeSlots.Add(ts);
                    }
                }

            }

            // sortowanie lekcji pod wzgledem wolnych slotów
            freeSlotsToLesson.Sort(new Comparison<FreeSlotsToLesson>(BFSComparator));

            foreach (FreeSlotsToLesson fstl in freeSlotsToLesson)
            {
                //Console.WriteLine(fstl.lesson.ToString());

                if (fstl.slots.Count > 0) {
                    List<int> indexOfSlotsWithMaxCount = new List<int>();
                    int max = 0;
                    // znajdywanie maksymalnej wartości
                    foreach (TimeSlot ts in fstl.slots)
                    {
                        int currentTiteTableSlotCount = currentTimeTable.getLessons(ts.day,ts.hour).Count;
                        if (max > currentTiteTableSlotCount)
                        {
                            max = currentTiteTableSlotCount;
                        }
                    }
                    // znajdywanie wszystki maksymalnych
                    foreach (TimeSlot ts in fstl.slots)
                    {
                        int currentTiteTableSlotCount = currentTimeTable.getLessons(ts.day, ts.hour).Count;
                        if (max == currentTiteTableSlotCount)
                        {
                            indexOfSlotsWithMaxCount.Add(fstl.slots.IndexOf(ts));
                        }
                    }

                    ////////////////////////
                    Room bestRoom = null;
                    TimeSlot bestSlot = getBestTimeSlot(fstl.slots, fstl.roomSlots, ref bestRoom, indexOfSlotsWithMaxCount);
                    // znajdywanie najlepszego slotu
                    for (int i = 0; i < fstl.lesson.getSize(); ++i)
                    {
                        if (fstl.lesson.getGroup().getTimetable().getLessons(bestSlot.day,bestSlot.hour + i).Count == 0) // czy oby na pewno jest wolny slot
                        {
                            fstl.lesson.setRoom(bestRoom);
                            fstl.lesson.addLessonToTimetable(bestSlot.day, bestSlot.hour + i);
                            //fstl.lesson.getGroup().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour + i);
                            //fstl.lesson.getTeacher().getTimetable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour + i);
                            //fstl.lesson.getRoom().getTimetable().addLesson(fstl.lesson, bestSlot.day, bestSlot.hour + i);
                        }
                        else
                        {
                            allLesson.Add(fstl.lesson);
                            //errorValue += 1000;
                            //Console.WriteLine("ERROR!!!!!!!!!!!!!!!! "+ fstl.lesson.ToString()); ////////////////////////wystąpił???!
                        }
                    }

                    // usuwanie wolnych slotów z listy ustawionych lekcji
                    foreach (FreeSlotsToLesson tmp in freeSlotsToLesson)
                    {
                        for (int i = 0; i < fstl.lesson.getSize(); ++i)
                        {
                            TimeSlot tmpSlot = new TimeSlot(bestSlot.day, bestSlot.hour + i);

                            if (tmp.slots.Contains(tmpSlot)) {
                                tmp.slots.Remove(tmpSlot);
                            }
                        }
                    }

                } else {

                    //Console.WriteLine("REMOVE " + fstl.lesson);
                    if (!removeLessonsAndFindNewPosition3(fstl.lesson, ref allLesson))
                    {
                        errorValue += 1000;
                        //Console.WriteLine(" nie znaleziono ");
                    }
                }
            }

        }

        public bool removeLessonsAndFindNewPosition3(Lesson lesson, ref List<Lesson> allLesson, int level = 1)
        {
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>removeLessonsAndFindNewPosition");
            //Console.WriteLine("start " + lesson.ToString() + " level " + level);

            bool result = false;
            Timetable groupTT = lesson.getGroup().getTimetable();
            //List<TimeSlot> groupFreeSlots = groupTT.getFreeSlotsToLesson(lesson);
            Timetable teacherTT = lesson.getTeacher().getTimetable();
            List<TimeSlot> teacherFreeSlots = teacherTT.getFreeSlotsToLesson(lesson);
            // sloty w salach w które można wstawic daną lekcje
            List<FreeSlotsInRoomToLesson> freeSlotsInRoomToLesson = new List<FreeSlotsInRoomToLesson>();
            foreach (Timetable tt in roomsTimetables)
            {
                if (tt.getRoom().getRoomType().Equals(lesson.getRoomType()) && tt.getRoom().getAmount() >= lesson.getGroup().getAmount())
                {
                    freeSlotsInRoomToLesson.Add(new FreeSlotsInRoomToLesson(tt.getFreeSlotsToLesson(lesson), tt.getRoom()));
                }
            }
            // sloty w sali i w planie nauczyciela w ktorych mozna wstawic dana lekcje
            FreeSlotsToLesson freeSlotsToLesson = new FreeSlotsToLesson(teacherFreeSlots, lesson, freeSlotsInRoomToLesson);



            if (freeSlotsToLesson.slots.Count == 0)
            {
                TimeSlot slotToChange = null;
                result = false;
                foreach (TimeSlot rts in freeSlotsToLesson.getSlotsFromRoomLists()) {
                    if(teacherTT.getLessons(rts.day, rts.hour).Count == 1) { 
                        Lesson lessonToChange = teacherTT.getLessons(rts.day, rts.hour)[0];
                        if (level < 5) {
                            result = removeLessonsAndFindNewPosition3(lessonToChange, ref allLesson, 4);
                        }
                        if (result) {
                            slotToChange = rts;
                            break;
                        }
                    }
                }
                if (!result) {
                    //Console.WriteLine("error : freeSlotsToLesson.slots.Count = 0");
                    return result;
                }
                freeSlotsToLesson.slots.Add(slotToChange);
                
            }



            // lista slotów skąd może być ściągnięta lekcja w planie dla grupy
            List<TimeSlot> slots = new List<TimeSlot>();
            foreach (TimeSlot ts in freeSlotsToLesson.slots)
            {
                if (groupTT.getLessons(ts.day, ts.hour).Count == 1)
                {
                    if (groupTT.getLessons(ts.day, ts.hour)[0].getSize() == 1)
                    {
                        slots.Add(ts);
                    }
                }
            }



            if (slots.Count == 0) {
                //slots.Add()
                return false;
            }



            //znajdujemy zastępczy slot dla losowej lekcji
            while (slots.Count > 0) {
                TimeSlot slot = slots[rand.Next(0, slots.Count - 1)];
                slots.Remove(slot);
                //szukam zastepczego slotu
                Lesson lessonToChange = groupTT.getLessons(slot.day,slot.hour)[0];
                List<FreeSlotsInRoomToLesson> freeSlotsInRoomToLessonTMP = new List<FreeSlotsInRoomToLesson>();
                foreach (Timetable tt in roomsTimetables)
                {
                    if (tt.getRoom().getRoomType().Equals(lessonToChange.getRoomType()) && 
                        tt.getRoom().getAmount() >= lessonToChange.getGroup().getAmount())
                    {
                        freeSlotsInRoomToLesson.Add(new FreeSlotsInRoomToLesson(tt.getFreeSlotsToLesson(lessonToChange), tt.getRoom()));
                    }
                }
                List<TimeSlot> groupSlots = groupTT.getFreeSlotsToLesson(lessonToChange);
                List<TimeSlot> teacherSlots = lessonToChange.getTeacher().getTimetable().getFreeSlotsToLesson(lessonToChange);
                List<TimeSlot> theSameSlots = getTheSameSlots(groupSlots, teacherSlots);
                FreeSlotsToLesson freeSlotsToLessonTMP = new FreeSlotsToLesson(theSameSlots, lessonToChange, freeSlotsInRoomToLesson);
                if (freeSlotsToLessonTMP.slots.Count > 0) {
                    Room bestRoom = null;
                    TimeSlot bestSlot = this.getBestTimeSlot(freeSlotsToLessonTMP.slots, freeSlotsToLessonTMP.roomSlots, ref bestRoom);
                    //Console.WriteLine("found : " + lessonToChange.ToString());
                    lessonToChange.setRoom(bestRoom);
                    lessonToChange.addLessonToTimetableAndBlockSlot(bestSlot.day, bestSlot.hour);
                    lessonToChange.removeLessonFromTimetable(slot.day, slot.hour);

                    bestRoom = freeSlotsToLesson.getRoomsFromSlot(slot)[0];

                    lesson.setRoom(bestRoom);
                    lesson.addLessonToTimetableAndBlockSlot(slot.day, slot.hour);
                    result = true;
                    break;
                }
            }



            int i = 0; ;
            while (!result && slots.Count > i) {
                
                TimeSlot slot = slots[i];
                Lesson lessonToChange = groupTT.getLessons(slot.day, slot.hour)[0];
                if (level < 5)
                {
                    result = removeLessonsAndFindNewPosition3(lessonToChange, ref allLesson, 3);
                }
            }



            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<removeLessonsAndFindNewPosition");
            return result;
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

        private TimeSlot getBestTimeSlot(List<TimeSlot> slots, List<FreeSlotsInRoomToLesson> roomSlots, ref Room bestRoom, List<int> indexOfSlotsWithMaxCount = null)
        {
            List<TimeSlot> bestTimeSlots = new List<TimeSlot>();//lista najlepszych slotów
            List<TimeSlot> worstTimeSlots = new List<TimeSlot>();//lista najgorszych slotów
            //podział slotów na 2 kategorie
            foreach (TimeSlot ts in slots)
            {
                if (ts.hour >= config.bottomBorderOfBestSlots && ts.hour <= config.topBorderOfBestSlots) {
                    bestTimeSlots.Add(ts);
                } else {
                    worstTimeSlots.Add(ts);
                }
            }
            TimeSlot slot = null;
            //zwraca losowy slot z listy najlepszych o ile taki istnieje
            if (bestTimeSlots.Count > 0)
            {
                slot = bestTimeSlots[rand.Next(bestTimeSlots.Count - 1)];
            }
            else
            {
                slot = worstTimeSlots[rand.Next(worstTimeSlots.Count - 1)];
            }

            List<Room> freeRooms = new List<Room>();
            foreach (FreeSlotsInRoomToLesson ro in roomSlots)
            {
                if (ro.slots.Contains(slot))
                {
                    freeRooms.Add(ro.room);
                }
            }
            ///////////////////////////////
            bestRoom = freeRooms[rand.Next(freeRooms.Count - 1)]; // wybranie najlepszej sali
            ///////////////////////////////

            return slot;
        }

        private List<Lesson> findDifferentSubjectTheSameGroup(Lesson lesson, List<Lesson> lessons)
        {
            List<Lesson> tmpLessons = new List<Lesson>();
            tmpLessons.Add(lesson);

            for (int i = lessons.Count - 1; i >= 0; i--)
            {
                if (tmpLessons.Count < config.numberOfLessonToPositioning)
                {
                    if (lessons[i].getGroup().Equals(tmpLessons[0].getGroup()))
                    {
                        if (!isContainInLessonsList(tmpLessons, lessons[i]))
                        {
                            tmpLessons.Add(lessons[i]);
                        }
                    }
                } else {
                    break;
                }
            }

            return tmpLessons;
        }

        private bool isContainInLessonsList(List<Lesson> tmpLessons, Lesson lesson)
        {
            foreach (Lesson l in tmpLessons)
            {
                if (!l.isDifferent(lesson))
                {
                    return true;
                }
            }
            return false;
        }

        private void sortLessons(List<Lesson> tmpLessons)
        {
            tmpLessons.Sort(new Comparison<Lesson>(lessonComparator));
        }

        public int lessonComparator(Lesson l1, Lesson l2)
        {
            return subGroupLessonComparator(l1, l2);
        }

        public int subGroupLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getGroup().getParent() != null) {
                if (l2.getGroup().getParent() != null) {
                    return l1.getGroup().getParent().GetHashCode() - l2.getGroup().getParent().GetHashCode();
                } else {
                    return 1;
                }
                
            } else if (l2.getGroup().getParent() != null) {
                return -1;
            } else {
                return sizeLessonComparator(l1, l2);
            }
           
        }

        public int sizeLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getSize() > l2.getSize())
            {
                return 1;
            }
            else if (l1.getSize() < l2.getSize())
            {
                return -1;
            }
            else
            {
                return amountLessonComparator(l1, l2);
            }
        }

        public int typeLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getSubject().getSubjectType().getPriority() > l2.getSubject().getSubjectType().getPriority())
            {
                return 1;
            }
            else if(l1.getSubject().getSubjectType().getPriority() < l2.getSubject().getSubjectType().getPriority())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int amountLessonComparator(Lesson l1, Lesson l2)
        {
            if (l1.getAmount() > l2.getAmount())
            {
                return 1;
            }
            else if (l1.getAmount() < l2.getAmount())
            {
                return -1;
            }
            else
            {
                return typeLessonComparator(l1, l2);
            }
        }

        public int BFSComparator(FreeSlotsToLesson o1, FreeSlotsToLesson o2)
        {
            if (o1.lesson.getSize() > o2.lesson.getSize())
            {
                return -1;
            }
            else if (o1.lesson.getSize() < o2.lesson.getSize())
            {
                return 1;
            }
            else
            {
                return o1.size.CompareTo(o2.size);
            }
        }

        public void print()
        {
            foreach (Teacher t in teachers)
            {
                Console.WriteLine(t.getTimetable().ToString());
            }

            foreach (Group g in groups)
            {
                Console.WriteLine(g.getTimetable().ToString());
                if (g.getSubGroup().Count > 0) {
                    foreach (Group sg in g.getSubGroup())
                    {
                        //Console.WriteLine(sg.getTimetable().ToString());
                    }
                }
            }

            foreach (Room r in rooms)
            {
                Console.WriteLine(r.getTimetable().ToString());
            }
        }

        public int fitness() {
            int value = 0;

            foreach (Timetable tt in groupsTimetables) {
                value += tt.fitness();
            }
            foreach (Timetable tt in teachersTimetables)
            {
                value += tt.fitness();
            }

            fitnessValue = value + errorValue;

            if (isCorrect()) {
                fitnessValue += 1000;
            }
            
            return value;
        }

        public List<Lesson> getLessonsForGroup(Group gr) {
            List<Lesson> tmpLessons = new List<Lesson>();
            foreach (Lesson l in lessons) {
                if (l.getGroup().Equals(gr) || (gr.getParent() != null  && l.getGroup().Equals(gr.getParent()))) {
                    tmpLessons.Add(l);
                }
            }
            return tmpLessons;
        }

        public bool isCorrect() {
            List<Lesson> tmpLessons = new List<Lesson>();

            foreach (Group g in groups) {
                List<Lesson> lessonsInGroupTimetable = g.getTimetable().getAllLessonsWithTimetable();
                foreach (Lesson l in getLessonsForGroup(g)) {
                    if (lessonsInGroupTimetable.Contains(l)) {
                        lessonsInGroupTimetable.Remove(l);
                    } else {
                        //Console.WriteLine(l.ToString() + ": does not exist in timetable");
                        return false;
                    }
                }
            }
            
            return true;
        }

        public void crossover(SchoolTimetable stt) {
            List<Lesson> tmp_lessons = new List<Lesson>();
            int counter = 0;

            for(int i = 0; i < this.groupsTimetables.Count; i++) {
                if (this.groupsTimetables[i].getGroup().getParent() == null) {

                    foreach (TimeSlot ts in this.groupsTimetables[i].getSlotsWithLesson())
                    {
                        if (this.groupsTimetables[i].getLessons(ts.day, ts.hour).Count == 1 &&
                            stt.groupsTimetables[i].getLessons(ts.day, ts.hour).Count == 1 &&
                            this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].getSize() == 1 &&
                            this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].getSubject().Equals(
                                stt.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].getSubject()
                                )
                            )
                        {
                            counter++;
                            //Console.Write(this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].ToString() + " <=> ");
                            //Console.WriteLine(stt.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].ToString());
                            this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].lockSlots(ts.day, ts.hour);
                        }
                        else
                        {
                            if (this.groupsTimetables[i].getLessons(ts.day, ts.hour).Count > 0)
                            {
                                //Console.WriteLine("x: " + this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].ToString());
                                if (ts.Equals(this.groupsTimetables[i].getLessons(ts.day, ts.hour)[0].getSlots()[0])) {
                                    tmp_lessons.AddRange(this.groupsTimetables[i].getLessons(ts.day, ts.hour));
                                }
                                

                            }
                        }
                    }
                }
            }

            foreach (Lesson l in tmp_lessons)
            {
                //Console.WriteLine("rem : "+ l.ToString());
                for (int i = 0; i < l.getSize(); i++) {
                    if (l.getSlots().Count > 0 && l.getSlots()[0] != null)
                    {
                        //Console.WriteLine(l.getSlots()[0].day + ":" + l.getSlots()[0].hour);
                        l.removeLessonFromTimetable(l.getSlots()[0].day, l.getSlots()[0].hour);
                    }
                }
                
                l.getSlots().Clear();
                l.setRoom(null);
            }

            sortLessons(tmp_lessons);
            //Console.WriteLine("============ do wstawienia =========");
            foreach (Lesson l in tmp_lessons)
            {
                //Console.WriteLine("d : " + l.ToString());
            }

            //print();

            //Console.WriteLine("====================================");


            this.genereteTimetable(tmp_lessons);
        }

        public void mutate() {
            List<Lesson> lessonsToChange = new List<Lesson>();

            foreach (Timetable tt in groupsTimetables)
            {
                List<TimeSlot> tmpTimeSlots = tt.getSlotsWithLesson();
                int amoutToChange = (int)((tmpTimeSlots.Count() * config.probabilityOfMutation)/100);

                for (int i = 0; i < amoutToChange; i++)
                {
                    TimeSlot tmp_slot = tmpTimeSlots[rand.Next(tmpTimeSlots.Count() - 1)];

                    if (
                        tt.getLessons(tmp_slot.day, tmp_slot.hour).Count == 1 && 
                        tt.getLessons(tmp_slot.day, tmp_slot.hour)[0].getSize() == 1 &&
                        tt.getLessons(tmp_slot.day, tmp_slot.hour)[0].getGroup().getParent() == null
                        )
                    {
                        Lesson tmp_lesson = tt.getLessons(tmp_slot.day, tmp_slot.hour)[0];

                        lessonsToChange.Add(tmp_lesson);
                        tmpTimeSlots.Remove(tmp_slot);

                        //tmp_lesson.removeLessonFromTimetable(tmp_slot.day, tmp_slot.hour);
                    }
                    else
                    {
                        tmpTimeSlots.Remove(tmp_slot);
                        --i;
                    }
                }
            }

            //lessons = lessonsToChange;

            foreach (Lesson l in lessonsToChange)
            {
                
                //Console.WriteLine("rem : " + l.ToString());
                for (int i = 0; i < l.getSize(); i++)
                {
                    //Console.WriteLine(l.getSlots()[0].day + ":" + l.getSlots()[0].hour);
                    l.removeLessonFromTimetable(l.getSlots()[0].day, l.getSlots()[0].hour);
                }

                l.getSlots().Clear();
                l.setRoom(null);
            }

            sortLessons(lessonsToChange);
            //Console.WriteLine("============ do wstawienia =========");
            foreach (Lesson l in lessonsToChange)
            {
                //Console.WriteLine("d : " + l.ToString());
            }

            this.genereteTimetable(lessonsToChange);
        }

        public void genWeb(string fileName = "output")
        {

            string mydocpath = @"G:\Dropbox\INŻ\Projekt\STGweb\webExport";
            string beginStr = "";
            string endStr = "";
            string contentStr = "";

            beginStr += "<!DOCTYPE html><html><head>\n";
            beginStr += "<link href = \""+ mydocpath + "/Content/bootstrap.css\" rel = \"stylesheet\" />\n";
            beginStr += "<link href = \"" + mydocpath + "/Content/site.css\" rel = \"stylesheet\" />\n";
            beginStr += "<script src = \"" + mydocpath + "/Scripts/modernizr-2.6.2.js\"></script>\n";
            beginStr += "</head><body>\n";
            beginStr += "<div class=\"container body-content\">\n";
        
            endStr += "</div>\n<script src = \"" + mydocpath + "/Scripts/jquery-1.10.2.js\"></script>\n";
            endStr += "<script src= \"" + mydocpath + "/Scripts/bootstrap.js\"></script>\n";
            endStr += "<script src= \"" + mydocpath + "/Scripts/respond.js\"></script>\n";
            endStr += "</body></html>\n";

            
            foreach (Timetable tt in this.teachersTimetables) {
                contentStr += "<div class=\"row\"> \n <div class=\"panel panel-default\"><div class=\"panel-body\"> <table class=\"table\">\n";
                ///////////////////////////////////////////////
                contentStr += "<h3>" + tt.getTeacher().getName() + "</h3>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                contentStr += "<td>Hour:</td>\n";
                foreach (Day d in tt.getDays()) {
                    contentStr += "<td>" +d.getName() + "</td>\n";
                }
                contentStr += "</tr>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                for (int h=0 ; h < numberOfSlots; h++ ) {
                    contentStr += "<td>" + h + "</td>\n";
                    for (int d = 0; d < numberOfDays; d++)
                    {
                        contentStr += "<td>";
                        int counter = 0;
                        foreach (Lesson l in tt.getLessons(d,h)) {
                            if (counter > 0)
                            {
                                contentStr += "<hr>\n";
                            }
                            counter++;
                            contentStr += "<ul>";
                            contentStr += "<li>" + l.getSubject().getName() + "</li> \n";
                            contentStr += "<li>" + l.getGroup().getName() + "</li> \n";
                            //contentStr += "<li>" + l.getTeacher().getName() + "</li> \n";
                            contentStr += "<li>" + l.getRoom().getName() + "</li> \n";
                            contentStr += "</ul>";
                        }
                        contentStr += "</td>\n";
                    }
                    contentStr += "</tr>\n";
                }
                
                //=============================================

                ///////////////////////////////////////////////
                contentStr += "</table> \n </div></div></div>";
            }

            foreach (Timetable tt in this.groupsTimetables)
            {
                contentStr += "<div class=\"row\"> \n <div class=\"panel panel-default\"><div class=\"panel-body\"> <table class=\"table\">\n";
                ///////////////////////////////////////////////
                contentStr += "<h3>" + tt.getGroup().getName() + "</h3>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                contentStr += "<td>Hour:</td>\n";
                foreach (Day d in tt.getDays())
                {
                    contentStr += "<td>" + d.getName() + "</td>\n";
                }
                contentStr += "</tr>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                for (int h = 0; h < numberOfSlots; h++)
                {
                    contentStr += "<td>" + h + "</td>\n";
                    for (int d = 0; d < numberOfDays; d++)
                    {
                        contentStr += "<td>";
                        int counter = 0;
                        foreach (Lesson l in tt.getLessons(d, h))
                        {
                            if (counter > 0)
                            {
                                contentStr += "<hr>\n";
                            }
                            counter++;
                            contentStr += "<ul>";
                            contentStr += "<li>" + l.getSubject().getName() + "</li> \n";
                            //contentStr += "<li>" + l.getGroup().getName() + "</li> \n";
                            contentStr += "<li>" + l.getTeacher().getName() + "</li> \n";
                            contentStr += "<li>" + l.getRoom().getName() + "</li> \n";
                            contentStr += "</ul>";
                        }
                        contentStr += "</td>\n";
                    }
                    contentStr += "</tr>\n";
                }
                //=============================================

                ///////////////////////////////////////////////
                contentStr += "</table> \n </div></div></div>";
            }

            foreach (Timetable tt in this.roomsTimetables)
            {
                contentStr += "<div class=\"row\"> \n <div class=\"panel panel-default\"><div class=\"panel-body\"> <table class=\"table\">\n";
                ///////////////////////////////////////////////
                contentStr += "<h3>" + tt.getRoom().getName() + "</h3>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                contentStr += "<td>Hour:</td>\n";
                foreach (Day d in tt.getDays())
                {
                    contentStr += "<td>" + d.getName() + "</td>\n";
                }
                contentStr += "</tr>\n";
                //--------------------------------------------
                contentStr += "<tr class=\"info\">\n";
                for (int h = 0; h < numberOfSlots; h++)
                {
                    contentStr += "<td>" + h + "</td>\n";
                    for (int d = 0; d < numberOfDays; d++)
                    {
                        contentStr += "<td>";
                        int counter = 0;
                        foreach (Lesson l in tt.getLessons(d, h))
                        {
                            if (counter > 0)
                            {
                                contentStr += "<hr>\n";
                            }
                            counter++;
                            contentStr += "<ul>";
                            contentStr += "<li>" + l.getSubject().getName() + "</li> \n";
                            contentStr += "<li>" + l.getGroup().getName() + "</li> \n";
                            contentStr += "<li>" + l.getTeacher().getName() + "</li> \n";
                            //contentStr += "<li>" + l.getRoom().getName() + "</li> \n";
                            contentStr += "</ul>";
                        }
                        contentStr += "</td>\n";
                    }
                    contentStr += "</tr>\n";
                }
               
                //=============================================

                ///////////////////////////////////////////////
                contentStr += "</table> \n </div></div></div>";
            }

            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\" + fileName+ ".html"))
            {
                outputFile.WriteLine(beginStr + contentStr + endStr);
            }


        }

        public void unlockAllSlots() {
            foreach (Teacher t in teachers)
            {
                t.getTimetable().unlockAllSlots();
            }

            foreach (Group g in groups)
            {
                g.getTimetable().unlockAllSlots();
                if (g.getSubGroup().Count > 0)
                {
                    foreach (Group sg in g.getSubGroup())
                    {
                        sg.getTimetable().unlockAllSlots();
                    }
                }
            }

            foreach (Room r in rooms)
            {
                r.getTimetable().unlockAllSlots();
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class Day
    {
        private int name;
        private List<Slot> slots;

        public Day(int name, int slotSize)
        {
            this.name = name;
            slots = new List<Slot>();
            for (int i = 0; i < slotSize;++i) {
                slots.Add(new Slot());
            }
        }

        public int getName() {
            return name;
        }

        public Slot getSlot(int id) {
            return slots[id];
        }

        public List<Slot> getSlots() {
            return slots;
        }
    }
}
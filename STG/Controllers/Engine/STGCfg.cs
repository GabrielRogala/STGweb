using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STG.Controllers.Engine
{
    public class STGCfg
    {
        public int populationSize;
        public int epocheSize;
        public int numberOfLessonToPositioning;
        public int bottomBorderOfBestSlots;
        public int topBorderOfBestSlots;
        public int probabilityOfMutation;

        public STGCfg(int populationSize, int epocheSize, int numberOfLessonToPositioning, int bottomBorderOfBestSlots, int topBorderOfBestSlots, int probabilityOfMutation)
        {
            this.populationSize = populationSize;
            this.epocheSize = epocheSize;
            this.numberOfLessonToPositioning = numberOfLessonToPositioning;
            this.bottomBorderOfBestSlots = bottomBorderOfBestSlots;
            this.topBorderOfBestSlots = topBorderOfBestSlots;
            this.probabilityOfMutation = probabilityOfMutation;
        }
    }
}
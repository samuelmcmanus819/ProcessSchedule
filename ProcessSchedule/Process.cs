using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessSchedule
{
    public class Process
    {
        public int processNum
        {
            get;
            set;
        }
        public TimeSpan startTime
        {
            get;
            set;
        }

        public int priority
        {
            get;
            set;
        }

        public TimeSpan duration
        {
            get;
            set;
        }
    }
}

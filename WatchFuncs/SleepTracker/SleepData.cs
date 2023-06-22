using System;
using System.Collections.Generic;

namespace OOP_Lab6.WatchFuncs
{
    public class SleepData
    {
        //fields
        //=================================================================================================================================
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; set; }
        public List<SleepStage> SleepStages { get; private set; } = new List<SleepStage>();
        //=================================================================================================================================

        //constructors
        //=================================================================================================================================
        public SleepData(DateTime startTime)
        {
            StartTime = startTime;
        }
        
        public TimeSpan GetSleepTime()
        {
            if (EndTime == null)
            {
                throw new Exception("Sleep data not complete.");
            }
            return EndTime.Value - StartTime;
        }
        //=================================================================================================================================
        
        
    }
}
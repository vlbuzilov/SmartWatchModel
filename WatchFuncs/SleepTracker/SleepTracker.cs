using System;
using System.Collections.Generic;

namespace OOP_Lab6.WatchFuncs
{
    public class SleepTrackerWatch : SmartWatch
{
    // Fields
    //=================================================================================================================================
    private List<SleepData> sleepDataList = new List<SleepData>();
    private SleepData currentSleepData = null;
    //=================================================================================================================================
    
    //properties
    //=================================================================================================================================
    public SleepData CurrentSleepData { get => currentSleepData; }
    public List<SleepData> SleepDataList { get => sleepDataList; }
    //=================================================================================================================================
    
    // Constructor
    //=================================================================================================================================
    public SleepTrackerWatch(string name) : base(name) {}
    //=================================================================================================================================
    
    //delegates
    //=================================================================================================================================
    public delegate void SleepTrackingStartedEventHandler(DateTime startTime);
    public delegate void SleepTrackingEndedEventHandler(DateTime endTime);

    public event SleepTrackingStartedEventHandler SleepTrackingStarted;
    public event SleepTrackingEndedEventHandler SleepTrackingEnded;
    //=================================================================================================================================
    
    // Methods
    //=================================================================================================================================
    public void MenuOptions()
    {
        bool marker = true;
        SleepTrackingStarted += TimeOfStart;
        SleepTrackingEnded += TimeOfEnd;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Sleep Tracker");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("1. Start sleep tracking");
        Console.WriteLine("2. Stop sleep tracking");
        Console.WriteLine("3. Get total sleep time");
        Console.WriteLine("4. Print sleep data");
        Console.WriteLine("0. Exit");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        while (marker)
        {
            Console.WriteLine();
            Console.WriteLine("Enter option: ");
            int answ = AskIntValue();

            try
            {
                switch (answ)
                {
                    case 1:
                        StartSleepTracking();
                        Console.WriteLine("Sleep tracking started");
                        break;

                    case 2:
                        StopSleepTracking();
                        Console.WriteLine("Sleep tracking ended");
                        break;

                    case 3:
                        Console.WriteLine($"Total sleep time: {GetTotalSleepTime()}");
                        break;

                    case 4:
                        PrintSleepData();
                        break;

                    case 0:
                        marker = false;
                        break;

                    default:
                        Console.WriteLine("No such option...");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CreateLog.log.Error($"Error occurred in MenuOptions method, in SleepTracker: {e.Message}");
            }
        }
    }

    public void AddSleepStage(SleepStage stage)
    {
        currentSleepData.SleepStages.Add(stage);
    }

    public SleepStage GetSleepStage(TimeSpan totalSleepTime)
    {
        if (totalSleepTime < TimeSpan.FromHours(4))
        {
            return SleepStage.Awake;
        }
        else if (totalSleepTime < TimeSpan.FromHours(6))
        {
            return SleepStage.LightSleep;
        }
        else if (totalSleepTime < TimeSpan.FromHours(8))
        {
            return SleepStage.DeepSleep;
        }
        else
        {
            return SleepStage.REM;
        }
    }

    public void StartSleepTracking()
    {
        if (currentSleepData != null && currentSleepData.EndTime == null)
        {
            throw new Exception("Sleep tracking already in progress.");
        }
        currentSleepData = new SleepData(DateTime.Now);
        AddSleepStage(SleepStage.Awake);
        SleepTrackingStarted?.Invoke(currentSleepData.StartTime);
    }

    private void TimeOfStart(DateTime time)
    {
        Console.WriteLine($"Time of start sleeping: {DateTime.Now}");
    }


    public void StopSleepTracking()
    {
        currentSleepData.EndTime = DateTime.Now;
        AddSleepStage(GetSleepStage(currentSleepData.GetSleepTime()));
        sleepDataList.Add(currentSleepData);
        currentSleepData = null;
        SleepTrackingEnded?.Invoke(DateTime.Now);
    }
    
    private void TimeOfEnd(DateTime time)
    {
        Console.WriteLine($"Time of end sleeping: {DateTime.Now}");
    }

    public TimeSpan GetTotalSleepTime()
    {
        TimeSpan totalSleepTime = TimeSpan.Zero;
        foreach (SleepData sleepData in sleepDataList)
        {
            totalSleepTime += sleepData.GetSleepTime();
        }
        if (currentSleepData != null && currentSleepData.EndTime != null)
        {
            totalSleepTime += currentSleepData.GetSleepTime();
        }
        return totalSleepTime;
    }
    
        public void PrintSleepData()
        {
            foreach (var sleepData in sleepDataList)
            {
                Console.WriteLine($"Sleep data: start time - {sleepData.StartTime}, end time - {sleepData.EndTime}");
                foreach (var stage in sleepData.SleepStages)
                {
                    Console.WriteLine($"Sleep stage: {stage}");
                }
                Console.WriteLine($"Sleep stage based on total sleep time: {GetSleepStage(sleepData.GetSleepTime())}\n");
            }
            if (currentSleepData != null && currentSleepData.EndTime == null)
            {
                Console.WriteLine($"Current sleep data: start time - {currentSleepData.StartTime}");
                foreach (var stage in currentSleepData.SleepStages)
                {
                    Console.WriteLine($"Current sleep stage: {stage}");
                }
                Console.WriteLine($"Current sleep stage based on total sleep time: {GetSleepStage(currentSleepData.GetSleepTime())}\n");
            }
            
        }
        //=================================================================================================================================
    }

    public enum SleepStage
    {
        Awake,
        LightSleep,
        DeepSleep,
        REM
    }
}
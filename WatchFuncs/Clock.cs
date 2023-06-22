using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OOP_Lab6.WatchFuncs
{
    public class Clock
    {
        //fields
        //=================================================================================================================================
        private Stopwatch stopwatch;
        private Timer timer;
        private DateTime alarm;
        //=================================================================================================================================
        
        //properties
        //=================================================================================================================================
        public Stopwatch Stopwatch { get => stopwatch; }
        public Timer Timer { get => timer; }
        public DateTime Alarm { get => alarm; }
        //=================================================================================================================================
        
        //delegates
        //=================================================================================================================================
        public delegate void AlarmEventHandler(object sender, EventArgs e);
        
        public event AlarmEventHandler AlarmEvent;
        //=================================================================================================================================
        
        //constructor
        //=================================================================================================================================
        public Clock()
        {
            try
            {
                stopwatch = new Stopwatch();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //=================================================================================================================================

        //methods
        //=================================================================================================================================
        public void StartStopWatch()
        {
            try
            {
                stopwatch.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public TimeSpan StopStopWatch()
        {
            try
            {
                stopwatch.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return stopwatch.Elapsed;
        }

        public void ResetStopWatch()
        {
            try
            {
                stopwatch.Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SetAlarm(int hours, int minutes, int seconds)
        {
            try
            {
                alarm = DateTime.Today.Add(new TimeSpan(hours, minutes, seconds));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public bool CheckAlarm()
        {
            if (DateTime.Now >= alarm)
            {
                OnAlarm(EventArgs.Empty);
                return true;
            }
            return false;
        }

        private void OnAlarm(EventArgs e)
        {
            AlarmEvent?.Invoke(this, e);
            Console.WriteLine("Good morning!");
            Console.WriteLine("Wake up, and say hello everyone!");
        }
        //=================================================================================================================================
    }
}
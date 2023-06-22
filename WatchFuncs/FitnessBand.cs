using System;
using System.Diagnostics;

namespace OOP_Lab6.WatchFuncs
{
    public class FitnessBand
    {
        //fields
        private int heartRate;
        private int countOfSteps;
        private int calories;
        private double kilometers;
        
        //delegates
        public delegate void WorkoutRecordedEventHandler(int minutes, double distance);
        
        public event WorkoutRecordedEventHandler WorkoutRecorded;
    
        public void AddWorkoutRecordedSubscriber(WorkoutRecordedEventHandler handler)
        {
            WorkoutRecorded += handler;
        }
    
        public void RemoveWorkoutRecordedSubscriber(WorkoutRecordedEventHandler handler)
        {
            WorkoutRecorded -= handler;
        }
        
        //constructors
        public FitnessBand()
        {
            heartRate = 0;
            countOfSteps = 0;
            calories = 0;
            kilometers = 0;
        }
        
        //properties
        public int HeartRate => heartRate;
        public int CountOfSteps => countOfSteps;
        public int Calories => calories;
        public double Kilometers => kilometers;
        
        //methods
    
        public void PrintStat()
        {
            Console.WriteLine($"Steps: {CountOfSteps}");
            Console.WriteLine($"HeartRate: {HeartRate}");
            Console.WriteLine($"Burned calories: {Calories}");
            Console.WriteLine($"Kilometers: {Kilometers}");
        }
        
        public void AddSteps()
        {
            try
            {
                Console.WriteLine("Walking started...(enter any key to finish)");
                Stopwatch startTime = new Stopwatch();
                startTime.Start();
                Console.ReadKey();
                int steps = (int)(startTime.Elapsed.TotalSeconds / 1.5);
    
                if (steps < 0) throw new ArgumentException("The number of steps cannot be negative.");
    
                countOfSteps += steps;
                kilometers += steps * 0.0007; // assuming average step length of 0.7 meters
                calories += steps / 20; // assuming burning 1 calorie per 20 steps
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void MeasureHeartRate()
        {
            try
            {
                Random rnd = new Random();
                heartRate = rnd.Next(60, 180); // assuming normal heart rate range of 60-180 bpm
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    
        public void CheckHeartAttack()
        {
            Console.WriteLine($"Heart rate: {HeartRate}");
            if (heartRate > 165)
            {
                throw new ArgumentException("High risk of heart attack!!!");
            }
        }
        
        public void CheckHeartAttack(int minutes, double distance)
        {
            Console.WriteLine($"Heart rate: {HeartRate}");
            if (heartRate > 165)
            {
                throw new ArgumentException("High risk of heart attack!!!");
            }
        }
        
        public void ResetCounters()
        {
            heartRate = 0;
            countOfSteps = 0;
            calories = 0;
            kilometers = 0;
        }
        
        public void RecordWorkout(int minutes, double distance)
        {
            try
            {
                double pace = distance / minutes; // calculate average pace in km/min
                calories += (int)(minutes * 7.5); // assuming a moderate workout burns 7.5 calories per minute
                kilometers += distance;
                Console.WriteLine($"Workout average pace: {pace:F2} km/min, total calories were burned: {calories}");
    
                // invoke the WorkoutRecorded event with the workout data
                WorkoutRecorded?.Invoke(minutes, distance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        //subscriber methods
        public void HandleWorkoutRecorded(int minutes, double distance)
        {
            Console.WriteLine($"New workout recorded: {minutes} minutes, {distance:F2} km");
        }
    }
}
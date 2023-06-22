using System;
using System.Threading;
using System.Threading.Tasks;
using OOP_Lab6.WatchFuncs;

namespace OOP_Lab6
{
    public class SmartWatch
    {
        //fields
        //=================================================================================================================================
        private string name;
        private GPSTracker gps = new GPSTracker();
        private FitnessBand fitnessBand = new FitnessBand();
        private Clock clock = new Clock();
        private Weather weather = new Weather();
        private Phone phone = new Phone();
        public Battery battery = new Battery(100);
        private const string APIkey = "AIzaSyCnlvP1TYLUPnWsQ0fWSQrM1VvVztx96sU";
        //=================================================================================================================================

        //properties
        //=================================================================================================================================
        public string Name { get; set; }
        //=================================================================================================================================

        //constructors 
        //=================================================================================================================================
        public SmartWatch(string name)
        {
            Name = name;
        }
        //=================================================================================================================================
        
        //methods
        //=================================================================================================================================
        public async Task GPS()
        {
            double speed;
            string DestinationAddress, MyAddress;
            bool marker = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GPS navigator");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Get your coordinates");
            Console.WriteLine("2. Get distance to point");
            Console.WriteLine("3. Get expected time of arrival to this point");
            Console.WriteLine("4. Get direction to this point");
            Console.WriteLine("5. Is destination point in 1km radius?");
            Console.WriteLine("0. Exit");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            
            Console.WriteLine("Enter address of your location: ");
            MyAddress = Console.ReadLine();
            var myCoordinates = gps.GetCoordinates(MyAddress);
            gps.Latitude = myCoordinates.latitude;
            gps.Longitude = myCoordinates.longitude;
            
            Console.WriteLine("Enter address of destination point: ");
            DestinationAddress = Console.ReadLine();

            var coordinates =  gps.GetCoordinates(DestinationAddress);
            Thread.Sleep(5000);
            Console.WriteLine($"For'{DestinationAddress}' coordinates is: {coordinates.latitude}, {coordinates.longitude}");

            Console.WriteLine("Enter speed: ");
            speed = AskDoubleValueAny();

            while (marker)
            {
                Console.WriteLine();
                Console.WriteLine("Enter option:");
                int answ = AskIntValue();

                try
                {
                    switch (answ)
                    {
                        case 1:
                            Console.WriteLine("Your coordinates:");
                            Console.WriteLine(gps.GetLastCoordinates());
                            break;
                    
                        case 2:
                            Console.WriteLine($"Distance to this point is {gps.GetDistance(coordinates.latitude, coordinates.longitude)}");
                            break;
                    
                        case 3:
                            Console.WriteLine($"Expected time of arrival: {gps.GetETA(coordinates.latitude, coordinates.longitude, speed)}");
                            break;
                    
                        case 4:
                            Console.WriteLine($"Direction to this point: {gps.GetDestination(coordinates.latitude, coordinates.longitude)}");
                            break;
                    
                        case 5:
                            Console.WriteLine($"Is this point in radius 100km: {(gps.IsWithinRadius(coordinates.latitude, coordinates.longitude,100) ? "yes" : "no")}");
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
                    CreateLog.log.Error($"Error occurred in GPS method, in smartWatchClass: {e.Message}");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void FitnessBand()
        {
            fitnessBand.AddWorkoutRecordedSubscriber(fitnessBand.HandleWorkoutRecorded);
            fitnessBand.AddWorkoutRecordedSubscriber(fitnessBand.CheckHeartAttack);
            
            bool marker = true;
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fitness band");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Print stats");
            Console.WriteLine("2. Add steps");
            Console.WriteLine("3. Check your heart rate");
            Console.WriteLine("4. Record workout");
            Console.WriteLine("5. Reset counters");
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
                            fitnessBand.PrintStat();
                            break;
                    
                        case 2:
                            fitnessBand.AddSteps();
                            break;
                    
                        case 3:
                            Console.WriteLine("Your heart rate:");
                            fitnessBand.MeasureHeartRate();
                            try
                            {
                                fitnessBand.CheckHeartAttack();
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter minutes: ");
                            int minutes = AskIntValue();
                            Console.WriteLine("Enter distance: ");
                            double distance = AskDoubleValueAny();
                            fitnessBand.RecordWorkout(minutes, distance);
                            break;
                    
                        case 5:
                            fitnessBand.ResetCounters();
                            Console.WriteLine("Counters have been reset");
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
                    CreateLog.log.Error($"Error occurred in FitnessBand method, in smartWatchClass: {e.Message}");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Clock()
        {
            bool marker = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Clock");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Run stopwatch");
            Console.WriteLine("2. Stop stopwatch");
            Console.WriteLine("3. Reset stopwatch");
            Console.WriteLine("4. Set an alarm");
            Console.WriteLine("5. Check an alarm");
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
                            clock.StartStopWatch();
                            break;
                    
                        case 2:
                            Console.WriteLine($"Time spent: {clock.StopStopWatch()}");
                            break;
                    
                        case 3:
                            clock.ResetStopWatch();
                            Console.WriteLine("Timer has been reset");
                            break;
                    
                        case 4:
                            Console.WriteLine("Enter hours: ");
                            int hours = AskIntValue();
                            Console.WriteLine("Enter minutes: ");
                            int minutes = AskIntValue();
                            Console.WriteLine("Enter seconds: ");
                            int seconds = AskIntValue();
                            clock.SetAlarm(hours, minutes, seconds);
                            Console.WriteLine("Alarm has already been set");
                            break;
                    
                        case 5:
                            if(!clock.CheckAlarm())
                            {
                                Console.WriteLine("To early for your alarm...");
                            }
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
                    CreateLog.log.Error($"Error occurred in Clock method, in smartWatchClass: {e.Message}");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Weather()
        {
            try
            {
                Console.WriteLine();
                weather.GetWeather();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CreateLog.log.Error($"Error occurred in Weather method, in smartWatchClass: {e.Message}");
            }
        }

        public void PhoneBook()
        {
            bool marker = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Phonebook");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Add number");
            Console.WriteLine("2. Remove number");
            Console.WriteLine("3. Update number");
            Console.WriteLine("4. Get number");
            Console.WriteLine("5. Print phonebook");
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
                            Console.WriteLine("Enter name: ");
                            string name = InputNames();
                            Console.WriteLine("Enter number: ");
                            int number = AskIntValue();
                            phone.Add(name, number);
                            break;
                    
                        case 2:
                            Console.WriteLine("Enter name: ");
                            name = InputNames();
                            phone.Remove(name);
                            break;
                    
                        case 3:
                            Console.WriteLine("Enter name: ");
                            name = InputNames();
                            Console.WriteLine("Enter new number: ");
                            number = AskIntValue();
                            phone.Update(name, number);
                            break;
                    
                        case 4:
                            Console.WriteLine("Enter name:");
                            name = InputNames();
                            number = phone.GetNumber(name);
                            if (number == 0)
                            {
                                Console.WriteLine("There isn't this person...");
                            }
                            else
                            {
                                Console.WriteLine($"{name} has number {number}");
                            }
                            break;
                    
                        case 5:
                            phone.PrintPhoneBookSorted();
                            break;
                    
                        case 0:
                            marker = false;
                            break;
                    
                        default:
                            Console.WriteLine("No such option... ");
                            break;
                    }
                }
                catch (Exception e)
                {
                    CreateLog.log.Error($"Error occurred in Phone method, in smartWatchClass: {e.Message}");
                    Console.WriteLine(e.Message);
                }
            }
        }
        //=================================================================================================================================
        
        //helpers
        //=================================================================================================================================
        protected string InputNames()
        {
            string word = Console.ReadLine();
            word.ToLower();
            
            if (string.IsNullOrEmpty(word))
            {
                return word;
            }

            char[] letters = word.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        
        protected static int AskIntValue(){
            int a;
            while (true)
            {
                string answ = Console.ReadLine();
                if (!int.TryParse(answ, out a))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong type of data, try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if(a < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Must be positive...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else break;
            }

            return a;
        }

        protected static double AskDoubleValueAny(){
            double a;
            while (true)
            {
                string answ = Console.ReadLine();
                if (!double.TryParse(answ, out a))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong type of data, try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else break;
            }

            return a;
        }
    } 
    //=================================================================================================================================
}

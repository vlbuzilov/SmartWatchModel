using System;
using OOP_Lab6.logger;
using OOP_Lab6.WatchFuncs;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace OOP_Lab6
{
    static class CreateLog
    {
        public static readonly log4net.ILog log = Logger.GetLogger();
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Menu();
        }

        static void Conditions(string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Smartwatch: '{name}'");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. GPS navigator");
            Console.WriteLine("2. Phonebook");
            Console.WriteLine("3. Weather");
            Console.WriteLine("4. Clock");
            Console.WriteLine("5. Fitness band");
            Console.WriteLine("6. Sleep tracker");
            Console.WriteLine("7. Charge battery");
            Console.WriteLine("0. Exit");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void Menu()
        {
            bool marker = true;
            Console.WriteLine("Enter name of your watch:");
            string name = Console.ReadLine();
            SmartWatch smartWatch = new SmartWatch(name);
            SleepTrackerWatch SleepTrackerWatch = new SleepTrackerWatch(name);

            Conditions(smartWatch.Name);

            while (marker)
            {
                Console.WriteLine($"Battery level: {smartWatch.battery.CurrentCharge}");
                Console.WriteLine();
                try
                {
                    smartWatch.battery.Discharge(5);
                }
                catch (Exception e)
                {
                    CreateLog.log.Info("Battery was discharged...");
                    Console.WriteLine(e.Message);
                    break;
                }
                Console.WriteLine("Enter option:");
                int answ = AskIntValue();

                try
                {
                    switch (answ)
                    {
                        case 1:
                            smartWatch.GPS();
                            break;
                    
                        case 2:
                            smartWatch.PhoneBook();
                            break;
                    
                        case 3:
                            smartWatch.Weather();
                            break;
                    
                        case 4:
                            smartWatch.Clock();
                            break;
                    
                        case 5:
                            smartWatch.FitnessBand();
                            break;
                    
                        case 6:
                            SleepTrackerWatch.MenuOptions();
                            break;
                    
                        case 7:
                            smartWatch.battery.Charge();
                            Console.WriteLine("Battery has been charged");
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
                    CreateLog.log.Error($"Error in main method: {e.Message}");
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        private static int AskIntValue(){
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
    }
}
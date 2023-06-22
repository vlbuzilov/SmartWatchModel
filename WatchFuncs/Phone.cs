using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Lab6.WatchFuncs
{
    public class Phone
    {
        //fields
        //=================================================================================================================================
        private Hashtable phoneBook = new Hashtable();
        //=================================================================================================================================

        //methods
        //=================================================================================================================================
        public void Add(string name, int phoneNumber)
        {
            try
            {
                phoneBook.Add(name, phoneNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        
        public void Remove(string name)
        {
            try
            {
                phoneBook.Remove(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void Update(string name, int newPhoneNumber)
        {
            try
            {
                if (phoneBook.Contains(name))
                {
                    phoneBook[name] = newPhoneNumber;
                }
                else
                {
                    Console.WriteLine($"Person with name {name} does not exist in phone book.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int GetNumber(string name)
        {
            try
            {
                if (phoneBook.Contains(name))
                {
                    return (int)phoneBook[name];
                }
                else
                {
                    Console.WriteLine("This person doesn't exist...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public void PrintPhoneBookSorted()
        {
            try
            {
                Console.WriteLine("Phone book (sorted by name):");
                Console.WriteLine();
                List<string> sortedNames = new List<string>(phoneBook.Keys.Cast<string>());
                sortedNames.Sort();
                foreach (string name in sortedNames)
                {
                    int phoneNumber = (int)phoneBook[name];
                    Console.WriteLine($"{name} : {phoneNumber}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //=================================================================================================================================
    }
}
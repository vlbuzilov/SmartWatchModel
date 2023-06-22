using System;
using System.Linq.Expressions;

namespace OOP_Lab6.WatchFuncs
{
    public class Battery
    {
        //fields
        private int maxCharge;
        private int currentCharge;

        //constructors
        public Battery(int currentCharge)
        {
            this.currentCharge = currentCharge;
            maxCharge = currentCharge;
        }

        //properties
        public int MaxCharge { get;}

        public int CurrentCharge
        {
            get
            {
                return currentCharge;
            }
            set
            {
                if (value >= 0 && value <= maxCharge)
                {
                    currentCharge = value;
                }
                else
                {
                    throw new ArgumentException("Invalid charge level.");
                }
            }
        }

        // Methods
        public void Charge()
        {
            currentCharge = maxCharge;
        }

        public void Discharge(int percent)
        {
            if ((currentCharge - percent) > 0)
            {
                currentCharge -= percent;
            }
            else
            {
                throw new ArgumentException("Watch is discharged now...");
            }
        }
    }
}
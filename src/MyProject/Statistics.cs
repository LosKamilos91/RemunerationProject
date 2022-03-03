using System;

namespace MyProject
{
    public class Statistics
    {
        public double Sum;
        public double Hight;
        public double Low;
        public int Count;
        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            Hight = double.MinValue;
            Low = double.MaxValue;
        }
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            Hight = Math.Max(number, Hight);
            Low = Math.Min(number, Low);
        }
    }
}
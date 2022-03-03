using System;
using System.Collections.Generic;
namespace MyProject
{
    public abstract class EmployeeBase : IEmployee
    {
        public string fullName;
        public int Age { get; }
        public EmployeeBase(string name, string surname)
        {
            this.fullName = name + " " + surname;
        }
        public EmployeeBase(string name, string surname, int age)
        {
            Age = age;
            if (Age <= 0 || 18 > Age)
            {
                MyFunctions.WriteMessage("Bad value of age!", true, MyFunctions.SettingWarnings.Error);
            }
            else
            {
                this.fullName = name + " " + surname;
            }
        }
        protected const double nationaMinimumWage = 1800.00;
        protected const int minimumBonus = 100;
        protected const int intermediateBonus = 200;
        protected const int maximumBonus = 300;
        protected List<double> remuneration = new List<double>();
        public event EventHandler<string> BonusAdded;
        public abstract void AddRemuneration(double money);
        public abstract void AddRemuneration(double money, bool bonus = false);
        public abstract Statistics GetStatistics();
        public void InfoBonus(object sender, string message)
        {
            Console.WriteLine("Message: " + message);
        }
        protected void ShowMessageAboutBonus()
        {
            BonusAdded?.Invoke(this, "Maximum bonus");
        }
    }
}
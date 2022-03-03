using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyProject
{
    public class InMemoryEmployee : EmployeeBase, IEmployee
    {
        public InMemoryEmployee(string name, string surname) : base(name, surname) { }
        public InMemoryEmployee(string name, string surname, int age) : base(name, surname, age) { }

        public override void AddRemuneration(double money)
        {
            if (money < nationaMinimumWage)
            {
                Console.WriteLine("Can't add remuneration for [ " + fullName + " ] because value < " + nationaMinimumWage);
            }
            else remuneration.Add(money);
        }
        public override void AddRemuneration(double money, bool bonus = false)
        {
            if (money < nationaMinimumWage)
            {
                Console.WriteLine("Can't add remuneration for " + fullName + " because value < " + nationaMinimumWage);
            }
            else if (bonus == true)
            {
                Console.Write("Choose bonus: [100, 200 or 300]: ");
                int ChooseBonus = int.Parse(Console.ReadLine());
                try
                {
                    switch (ChooseBonus)
                    {
                        case 100:
                            remuneration.Add(money += minimumBonus);
                            break;
                        case 200:
                            remuneration.Add(money += intermediateBonus);
                            break;
                        case 300:
                            remuneration.Add(money += maximumBonus);
                            ShowMessageAboutBonus();
                            break;
                        default:
                            throw new ArgumentException("Invalid value");
                    }
                }
                catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                remuneration.Add(money);
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            for (var index = 0; index < remuneration.Count; index += 1)
            {
                result.Add(remuneration[index]);
            }
            Console.WriteLine($"Average remuneration: = {result.Average}");
            Console.WriteLine("Hight remuneration: {0:F2}", result.Hight);
            Console.WriteLine("Low remuneration: {0:F2}", result.Low);
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MyProject
{
    public class SavedEmployeeInFile : EmployeeBase, IEmployee
    {
        const string autoSaved = "audit.txt";
        const string saveInFile = "Employees.txt";
        protected int numberBonus = 0;
        public SavedEmployeeInFile(string name, string surname) : base(name, surname) { }
        public SavedEmployeeInFile(string name, string surname, int age) : base(name, surname, age) { }
        public override void AddRemuneration(double money)
        {
            var name = fullName;
            using (var writer = File.AppendText($"{name}"))
            using (var auto = File.AppendText($"{autoSaved}"))
            {
                writer.WriteLine($"{money}");
                auto.WriteLine($"{name}, {money}, Time: " + DateTime.UtcNow);
            }
        }
        public override void AddRemuneration(double money, bool bonus = false)
        {
            var cash = money;
            var name = fullName;
            using (var writer = File.AppendText($"{name}"))
            using (var auto = File.AppendText($"{autoSaved}"))
            {
                if (bonus == true)
                {
                    Console.Write("Choose bonus: [100, 200 or 300]: ");
                    int ChooseBonus = int.Parse(Console.ReadLine());
                    try
                    {
                        switch (ChooseBonus)
                        {
                            case 100:
                                cash += minimumBonus;
                                numberBonus = 1;
                                MyFunctions.WriteMessage($"Renumeration for [ {name} ] added with bonus + {minimumBonus}. Press any key to continue.",
                                    true, MyFunctions.SettingWarnings.Success);
                                break;
                            case 200:
                                cash += intermediateBonus;
                                numberBonus = 2;
                                MyFunctions.WriteMessage($"Renumeration for [ {name} ] added with bonus + {intermediateBonus}. Press any key to continue.",
                                    true, MyFunctions.SettingWarnings.Success);
                                break;
                            case 300:
                                cash += maximumBonus;
                                numberBonus = 3;
                                MyFunctions.WriteMessage($"Renumeration for [ {name} ] added with bonus + {maximumBonus}. Press any key to continue.",
                                    true, MyFunctions.SettingWarnings.Success);
                                break;
                            default:
                                MyFunctions.WriteMessage($"Invalid value. Press any key to try again.", true, MyFunctions.SettingWarnings.Error);
                                break;
                        }
                    }
                    catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
                    writer.WriteLine($"{cash}");
                    var x = numberBonus == 1 ? minimumBonus : numberBonus == 2 ? intermediateBonus : maximumBonus;
                    auto.WriteLine($"{name} - {money} + bonus {x}, Time: " + DateTime.UtcNow);
                }
                else
                {
                    writer.WriteLine($"{money}");
                    auto.WriteLine($"{name} - {money}, Time: " + DateTime.UtcNow);
                }
            }
        }
        public static SavedEmployeeInFile SaveEmployee(string name, string surname, int age)
        {
            SavedEmployeeInFile saveEmployInFile = new SavedEmployeeInFile(name, surname, age);
            using (var nameEmployee = File.AppendText($"{saveInFile}"))
            {
                nameEmployee.WriteLine($"{name.ToLower()} {surname.ToLower()} | age: {age}");
            }
            return saveEmployInFile;
        }
        public static SavedEmployeeInFile SaveEmployee(string name, string surname)
        {
            SavedEmployeeInFile saveEmployInFile = new SavedEmployeeInFile(name, surname);
            using (var nameEmployee = File.AppendText($"{saveInFile}"))
            {
                nameEmployee.WriteLine($"{name.ToLower()} {surname.ToLower()}");
            }
            return saveEmployInFile;
        }
        public override Statistics GetStatistics()
        {
            Statistics result = new Statistics();
            var name = fullName;
            using (var reader = File.OpenText($"{name}"))
            {
                string line = reader.ReadLine();
                var number = double.Parse(line);
                result.Add(number);
                while (line != null)
                {
                    number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            Console.WriteLine("Average remuneration: {0:F2}", result.Average);
            Console.WriteLine("Hight remuneration: {0:F2}", result.Hight);
            Console.WriteLine("Low remuneration: {0:F2}", result.Low);
            return result;
        }
        public static void ShowEmployeeInConsole()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Your Employees:");
                Console.WriteLine();
                using (var reader = File.OpenText($"{saveInFile}"))
                {
                    string readLine = reader.ReadLine();
                    Console.WriteLine(readLine);
                    while (readLine != null)
                    {
                        readLine = reader.ReadLine();
                        Console.WriteLine(readLine);
                    }
                }
                MyFunctions.WriteMessage("Press any key to return.", true, MyFunctions.SettingWarnings.Information);
            }
            catch (FileNotFoundException)
            {
                MyFunctions.WriteMessage("File not exist. You need add employee. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
            }
            catch (ArgumentException)
            {
                MyFunctions.WriteMessage("Bad argument!. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
            }
            catch (Exception)
            {
                MyFunctions.WriteMessage("Something went wrong!. Press any key to try again.", true, MyFunctions.SettingWarnings.Information);
            }
        }
        public static void AddEmployeeRemunerationInConsole()
        {
            try
            {
                string name, surname;
                bool showOptionsRemuneration = false;
                while (true)
                {
                    EnterNameEmployee(out showOptionsRemuneration, out name, out surname);
                    if (showOptionsRemuneration == true)
                    {
                        SavedEmployeeInFile newEmployee = new SavedEmployeeInFile(name, surname);
                        Console.Write("Renumeration: ");
                        double remuneration = double.Parse(Console.ReadLine());
                        if (remuneration < nationaMinimumWage)
                        {
                            MyFunctions.WriteMessage($"Can't add remuneration because value < {nationaMinimumWage}. Press any key to try again.",
                                true, MyFunctions.SettingWarnings.Warning);
                        }
                        else
                        {
                            Console.Write("Do you want give him bonus? [yes/no]: ");
                            string bonus = Console.ReadLine().ToLower();
                            if (bonus == "yes")
                            {
                                newEmployee.AddRemuneration(remuneration, true);
                                break;
                            }
                            else if (bonus == "no")
                            {
                                newEmployee.AddRemuneration(remuneration);
                                MyFunctions.WriteMessage($"Renumeration for [ {name} {surname} ] added. Press any key to continue.", true, MyFunctions.SettingWarnings.Success);
                                break;
                            }
                            else
                            {
                                MyFunctions.WriteMessage($"Bad format. Renumeration for [ {name} {surname} ] not added. Try again.", true, MyFunctions.SettingWarnings.Warning);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MyFunctions.WriteMessage("File not exist. You need add employee. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
            }
            catch (NullReferenceException)
            {
                MyFunctions.WriteMessage($"Can't found this employee. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
            }
        }
        public static void StatisticsInConsole()
        {
            string name, surname;
            bool showStatistics = false;
            while (true)
            {
                EnterNameEmployee(out showStatistics, out name, out surname);
                if (showStatistics == true)
                {
                    SavedEmployeeInFile n = new SavedEmployeeInFile(name, surname);
                    Console.WriteLine();
                    Console.ForegroundColor = System.ConsoleColor.Green;
                    Console.WriteLine($"Statistic - {name} {surname}");
                    Console.ResetColor();
                    Console.WriteLine();
                    n.GetStatistics();
                    Console.Write("Do you want show statistics for another employee [yes/no]: ");
                    string again = Console.ReadLine().ToLower();
                    if (again == "yes")
                    {
                        Console.Clear();
                    }
                    else if (again == "no")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        MyFunctions.WriteMessage("Bad format. Press any key to try again.", true, MyFunctions.SettingWarnings.Error);
                        break;
                    }
                }
            }
        }
        private static void EnterNameEmployee(out bool options, out string name, out string surname)
        {
            Console.WriteLine("Enter the name of the employee to whom you want to add the remuneration");
            Console.Write("Give name: ");
            name = Console.ReadLine().ToLower();
            Console.Write("Give surname: ");
            surname = Console.ReadLine().ToLower();
            using (var reader = File.OpenText($"{saveInFile}"))
            {
                string readLine = reader.ReadLine();
                options = readLine.Contains($"{name} {surname}");
                while (options == false)
                {
                    readLine = reader.ReadLine();
                    options = readLine.Contains($"{name} {surname}");
                }
            }
        }
    }
}
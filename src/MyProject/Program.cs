using System;
using System.Collections.Generic;
using System.IO;

namespace MyProject
{
    class Program
    {
        static void Main(string[] args)
        {   
            SavedEmployeeInFile employee;
            bool isOpenProgram = false;
            while (true)
            {
                try
                {
                    if (isOpenProgram == true)
                        break;
                    else
                    {
                        MyFunctions.WriteMessage
                        (
                            "[1] - Add employee",
                            "[2] - Show employee",
                            "[3] - Add employee remuneration",
                            "[4] - Employee statistics",
                            "[0] - Exit"
                        );
                        Console.Write("Select number: ");
                        int selectNumber = int.Parse(Console.ReadLine());
                        if (selectNumber == 0)
                        {
                            Console.Clear();
                            Console.Write("Do you want exit program? [yes/no]: ");
                            string exit = Console.ReadLine().ToLower(); 
                            if (exit == "yes")
                            {
                                isOpenProgram = true;
                                break;
                            }
                            else if (exit == "no")
                            {
                                Console.Clear();
                            }
                            else
                            {
                                MyFunctions.WriteMessage("Bad format. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                            }
                        }
                        else if (1 <= selectNumber || selectNumber <= 4)
                        {
                            Console.Clear();
                            switch (selectNumber)
                            {
                                case 1:
                                    while (true)
                                    {
                                        Console.Write("Do you want add new Employee? [yes/no]: ");
                                        string addNewEmployee = Console.ReadLine().ToLower();
                                        if (addNewEmployee == "yes")
                                        {
                                            while (true)
                                            {
                                                Console.Write("Add name: ");
                                                string name = Console.ReadLine();
                                                Console.Write("Add surname: ");
                                                string surname = Console.ReadLine();
                                                Console.Write("Do you want add age [yes/no]: ");
                                                string AgeYesOrNo = Console.ReadLine().ToLower();
                                                if (AgeYesOrNo == "yes")
                                                {
                                                    Console.Write("Add age: ");
                                                    try
                                                    {
                                                        int age = int.Parse(Console.ReadLine());
                                                        employee = SavedEmployeeInFile.SaveEmployee(name, surname, age);
                                                        MyFunctions.WriteMessage("Employee added. Press any key to continue.", true, MyFunctions.SettingWarnings.Success);

                                                    }
                                                    catch (FormatException)
                                                    {
                                                        MyFunctions.WriteMessage("Employee not added. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                                                    }
                                                }
                                                else if (AgeYesOrNo == "no")
                                                {
                                                    employee = SavedEmployeeInFile.SaveEmployee(name, surname);
                                                    MyFunctions.WriteMessage("Employee added. Press any key to continue.", true, MyFunctions.SettingWarnings.Success);
                                                }
                                                else
                                                {
                                                    MyFunctions.WriteMessage("Employee not added. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                                                }
                                                break;
                                            }
                                        }
                                        else if (addNewEmployee == "no")
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                        else
                                        {
                                            MyFunctions.WriteMessage("Employee not added. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                                        }
                                    }
                                    break;
                                case 2:
                                    SavedEmployeeInFile.ShowEmployeeInConsole();
                                    break;
                                case 3:
                                        SavedEmployeeInFile.AddEmployeeRemunerationInConsole();                  
                                    break;
                                case 4:
                                    try
                                    {
                                        SavedEmployeeInFile.StatisticsInConsole();
                                    }
                                    catch (Exception)
                                    {
                                        MyFunctions.WriteMessage("Can't show statistics. Add remuneration. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                                    }
                                    break;
                                default:
                                    MyFunctions.WriteMessage("Wrong number. Press any key to try again.", true, MyFunctions.SettingWarnings.Warning);
                                    break;
                            }
                        }
                    }
                }
                catch (ArgumentNullException nullArgument)
                {
                    Console.WriteLine(nullArgument.Message);
                }
                catch (FormatException)
                {
                    MyFunctions.WriteMessage("Bad format. Press any key to try again.", true, MyFunctions.SettingWarnings.Error);
                }
                catch (OverflowException)
                {
                    MyFunctions.WriteMessage("Value was either too large or too small for an Int32. Employee not added. Press any key to try again.", 
                        true, MyFunctions.SettingWarnings.Error);
                }
            }
        }
    }
}

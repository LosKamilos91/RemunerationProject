using System;
using System.Collections.Generic;
using System.IO;

namespace MyProject
{
    public class MyFunctions
    {
        public enum SettingWarnings { Warning, Success, Information, Error }
        public static void WriteMessage(params string[] message)
        {
            foreach (var textMessage in message)
            {
                Console.WriteLine($"{textMessage}");
            }
        }
        public static void WriteMessage(string description, bool consoleClear = false, SettingWarnings setting = SettingWarnings.Warning)
        {
            if (setting == SettingWarnings.Success)
            {
                Console.ForegroundColor = System.ConsoleColor.Green;
                Console.Write($"Success: {description}");
                Console.ResetColor();
            }
            else if (setting == SettingWarnings.Information)
            {
                Console.ForegroundColor = System.ConsoleColor.DarkGreen;
                Console.Write($"Information: {description}");
                Console.ResetColor();
            }
            else if (setting == SettingWarnings.Warning)
            {
                Console.ForegroundColor = System.ConsoleColor.DarkYellow;
                Console.Write($"Warning: {description}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.Write($"Error: {description}");
                Console.ResetColor();
            }
            if (consoleClear == true)
            {
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
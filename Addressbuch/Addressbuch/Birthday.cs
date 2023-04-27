using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    // Class to give out todays Birthday of Adressbook.txt with age
    class Birthday
    {
        public static void BirthdayToday()
        {
            try
            {
                string[] lines = File.ReadAllLines("addressbook.txt");
                string[] fields = new string[9];
                string[] birthday = new string[3];
                string[] today = new string[3];
                int age = 0;
                int todayDay = DateTime.Now.Day;
                int todayMonth = DateTime.Now.Month;
                int todayYear = DateTime.Now.Year;
                int birthdayDay = 0;
                int birthdayMonth = 0;
                int birthdayYear = 0;
                bool found = false;

                foreach (string line in lines)
                {
                    fields = line.Split(',');
                    birthday = fields[6].Split('.');
                    birthdayDay = Convert.ToInt32(birthday[0]);
                    birthdayMonth = Convert.ToInt32(birthday[1]);
                    birthdayYear = Convert.ToInt32(birthday[2]);

                    if (todayDay == birthdayDay && todayMonth == birthdayMonth)
                    {
                        age = todayYear - birthdayYear;
                        Console.WriteLine($"Heute hat {fields[0]} {fields[1]} Geburtstag und wird {age} Jahre alt.");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Heute hat niemand Geburtstag.");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Fehler beim Lesen der addressbook.txt Datei: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Es ist ein Fehler aufgetreten: " + e.Message);
            }
        }
    }
}

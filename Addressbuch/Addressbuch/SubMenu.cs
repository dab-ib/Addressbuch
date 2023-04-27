using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class SubMenu
    {
        static public void ShowSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔══════════════════════════════════╗");
                Console.WriteLine("\t\t║        Was möchtest du tun?      ║");
                Console.WriteLine("\t\t╚══════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔══════════════════════════════════╗");
                Console.WriteLine("\t\t║ B - Eintrag bearbeiten           ║");
                Console.WriteLine("\t\t║                                  ║");
                Console.WriteLine("\t\t║ L - Eintrag löschen              ║");
                Console.WriteLine("\t\t║                                  ║");
                Console.WriteLine("\t\t║ F - Duplikate anzeigen           ║");
                Console.WriteLine("\t\t║                                  ║");
                Console.WriteLine("\t\t║ D - Duplikate entfernen          ║");
                Console.WriteLine("\t\t║                                  ║");
                Console.WriteLine("\t\t║ X - Alles Löschen                ║");
                Console.WriteLine("\t\t║                                  ║");
                Console.WriteLine("\t\t║ Z - Zurück zum Hauptmenü         ║");
                Console.WriteLine("\t\t╚══════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "B":
                        Console.Clear();
                        Addressbook.EditEntry();
                        break;
                    case "L":
                        Console.Clear();
                        Addressbook.DeleteEntry();
                        break;
                    case "D":
                        Console.Clear();
                        Duplicates.RemoveDuplicates();
                        break;
                    case "F":
                        Console.Clear();
                        Duplicates.ShowDuplicates();
                        break;
                    case "X":
                        Console.Clear();
                        DeleteAll.DeleteFile();
                        break;
                    case "Z":
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }
}

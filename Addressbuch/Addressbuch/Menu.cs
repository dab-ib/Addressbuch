using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    internal class Menu
    {
        public static void Hauptmenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║           Was möchtest du tun?       ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║ N - Neuer Eintrag anlegen            ║");
                Console.WriteLine("\t\t║ A - Gesamtes Adressbuch anzeigen     ║");
                Console.WriteLine("\t\t║ G - heutige Geburtstage              ║");
                Console.WriteLine("\t\t║ M - Einträge verwalten               ║");
                Console.WriteLine("\t\t║ S - Such-Menü anzeigen               ║");
                Console.WriteLine("\t\t║ E - Export-Menü anzeigen             ║");
                Console.WriteLine("\t\t║ I - Import-Menü anzeigen             ║");
                Console.WriteLine("\t\t║ P - PLZ-Menü                         ║");
                Console.WriteLine("\t\t║ B - Programm Beenden                 ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "N":
                        Console.Clear();
                        Addressbook.AddEntry();
                        break;
                    case "A":
                        Console.Clear();
                        Addressbook.ShowAddressBook();
                        Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        break;
                    case "S":
                        Console.Clear();
                        SearchMenu.ShowSearchMenu();
                        break;
                    case "M":
                        Console.Clear();
                        SubMenu.ShowSubMenu();
                        break;
                    case "B":
                        Environment.Exit(0);
                        break;
                    case "G":
                        Console.Clear();
                        Birthday.BirthdayToday();
                        Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        break;
                    case "E":
                        Console.Clear();
                        ExportMenu.ShowExportMenu();
                        break;
                    case "I":
                        Console.Clear();
                        ImportMenu.ShowImportMenu();
                        break;
                    case "P":
                        Console.Clear();
                        PLZMenu.ShowPLZMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class PLZMenu : Menu
    {
        static public new void ShowPLZMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║           Was möchtest du tun?       ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║ P - PLZ eingeben                     ║");
                Console.WriteLine("\t\t║ O - Ort eingeben                     ║");
                Console.WriteLine("\t\t║ Z - Zurück zum Hauptmenü             ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "P":
                        Console.Clear();
                        PLZHelper.PLZ_Finder();
                        Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        break;
                    case "O":
                        Console.Clear();
                        PLZHelper.Ort_Finder();
                        Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        break;
                    case "Z":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }
}

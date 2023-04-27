using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    public static class DeleteAll
    {
        public static void DeleteFile()
        {
            Console.WriteLine("Sind Sie sich sicher alle Kontakte zu löschen? Antworten Sie mit 'Ja' oder 'Nein'.");
            string input = Console.ReadLine();
            if (input == "Ja")
            {
                Console.WriteLine("Alle Kontakte werden nun gelöscht. Und das Programm wird geschlossen.");
                File.Delete("addressbook.txt");

                Console.WriteLine("\nWarte auf Eingabe um das Programm zu beenden...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (input == "Nein")
            {
                Console.WriteLine("Die Kontakte werden nicht gelöscht.");
                Console.WriteLine("");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Falsche Eingabe.");

                Console.WriteLine("");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }
    }
}

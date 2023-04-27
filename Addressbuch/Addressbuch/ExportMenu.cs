using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class ExportMenu
    {
        static public void ShowExportMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║           Was möchtest du tun?       ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔══════════════════════════════════════╗");
                Console.WriteLine("\t\t║ C - Ein Kontakt als CSV exportieren  ║");
                Console.WriteLine("\t\t║                                      ║");
                Console.WriteLine("\t\t║ A - Gesamtes Adressbuch exportieren  ║");
                Console.WriteLine("\t\t║                                      ║");
                Console.WriteLine("\t\t║ Z - Zurück zum Hauptmenü             ║");
                Console.WriteLine("\t\t╚══════════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "C":
                        Console.Clear();
                        Console.WriteLine("Vorname und Nachname des zu exportierenden Kontakts:");
                        string fullname = Console.ReadLine();
                        CsvExporter.ExportToCsv(fullname, "addressbook.txt", "Kontakt_" + fullname + ".csv");
                        break;
                    case "A":
                        Console.Clear();
                        AllcsvExporter.ExportContactsToCsv("AlleKontakte.csv");
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

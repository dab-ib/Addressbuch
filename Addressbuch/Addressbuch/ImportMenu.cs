using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class ImportMenu
    {
        static public void ShowImportMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔═════════════════════════════════════╗");
                Console.WriteLine("\t\t║        Was möchtest du tun?         ║");
                Console.WriteLine("\t\t╚═════════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔═════════════════════════════════════╗");
                Console.WriteLine("\t\t║ I - Aus einer CSV Datei Importieren ║");
                Console.WriteLine("\t\t║                                     ║");
                Console.WriteLine("\t\t║ Z - Zurück zum Hauptmenü            ║");
                Console.WriteLine("\t\t╚═════════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "I":
                        Console.WriteLine("Die CSV Datei muss wie folgt aufgebaut sein, damit diese erfolgreich importiert werden kann:");
                        Console.WriteLine("Vorname, Nachname, Strasse privat, Postleitzahl privat, Ort privat, Telefon (privat), Geburtstag, E-mail-Adresse, Firma");
                        Console.WriteLine("Dateipfad der zu importierenden CSV-Datei:");
                        string input1 = Console.ReadLine();
                        CsvToAddressbookConverter.ConvertCsvToAddressbook(input1);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class SearchMenu
    {
        static public void ShowSearchMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n\t\t╔════════════════════════════════╗");
                Console.WriteLine("\t\t║        Was möchtest du tun?    ║");
                Console.WriteLine("\t\t╚════════════════════════════════╝\n");
                Console.WriteLine("\t\t╔════════════════════════════════╗");
                Console.WriteLine("\t\t║ N - Nach Namen suchen          ║");
                Console.WriteLine("\t\t║                                ║");
                Console.WriteLine("\t\t║ T - Nach Telefonnummer suchen  ║");
                Console.WriteLine("\t\t║                                ║");
                Console.WriteLine("\t\t║ E - Nach E-Mail suchen         ║");
                Console.WriteLine("\t\t║                                ║");
                Console.WriteLine("\t\t║ A - Nach Adresse suchen        ║");
                Console.WriteLine("\t\t║                                ║");
                Console.WriteLine("\t\t║ F - Nach Firma suchen          ║");
                Console.WriteLine("\t\t║                                ║");
                Console.WriteLine("\t\t║ Z - Zurück zum Hauptmenü       ║");
                Console.WriteLine("\t\t╚════════════════════════════════╝\n");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "N":
                        SearchEntryName.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "T":
                        SearchEntryNumber.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "E":
                        SearchEntryEmail.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "A":
                        SearchEntryAdress.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "F":
                        SearchEntryFirma.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
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

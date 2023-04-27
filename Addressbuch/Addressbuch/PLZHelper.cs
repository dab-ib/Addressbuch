using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class PLZHelper
    {
        static public void PLZ_Finder()
        {
            Console.Write("Bitte PLZ eingeben: ");
            string input = Console.ReadLine();

            List<PlzData> results = ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.PLZ == input)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Keine Ergebnisse gefunden.");
                return;
            }

            foreach (PlzData data in results)
            {
                Console.WriteLine($"Ort: {data.Ort}");
                Console.WriteLine($"Zusatz: {data.Zusatz}");
                Console.WriteLine($"PLZ: {data.PLZ}");
                Console.WriteLine($"Vorwahl: {data.Vorwahl}");
                Console.WriteLine($"Bundesland: {data.Bundesland}");
                Console.WriteLine("");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }

        static public void Ort_Finder()
        {
            Console.Write("Bitte Ort eingeben: ");
            string input = Console.ReadLine();

            List<PlzData> results = ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.Ort == input)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Keine Ergebnisse gefunden.");
                return;
            }

            foreach (PlzData data in results)
            {
                Console.WriteLine($"Ort: {data.Ort}");
                Console.WriteLine($"Zusatz: {data.Zusatz}");
                Console.WriteLine($"PLZ: {data.PLZ}");
                Console.WriteLine($"Vorwahl: {data.Vorwahl}");
                Console.WriteLine($"Bundesland: {data.Bundesland}");
                Console.WriteLine("");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }

        static List<PlzData> ReadEmbeddedCsvFile(string resourceName)
        {
            List<PlzData> data = new List<PlzData>();

            // Die Assembly laden, die die Ressource enthält
            Assembly asm = Assembly.GetExecutingAssembly();

            // Den Ressourcenpfad ermitteln
            string resourcePath = asm.GetName().Name + "." + resourceName + ".csv";

            // Die Ressource als Stream öffnen
            using (Stream stream = asm.GetManifestResourceStream(resourcePath))
            {
                // Den Stream als CSV-Datei lesen
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(';');

                        if (fields.Length == 5)
                        {
                            // Neue Instanz von PlzData erstellen
                            PlzData item = new PlzData();

                            // Die Felder der Instanz zuweisen
                            item.Ort = fields[0];
                            item.Zusatz = fields[1];
                            item.PLZ = fields[2];
                            item.Vorwahl = fields[3];
                            item.Bundesland = fields[4];

                            // Die Instanz zur Liste hinzufügen
                            data.Add(item);
                        }
                        else
                        {
                            //Console.WriteLine($"Ungültige Zeile: "+line);
                        }
                    }
                }
            }

            return data;
        }
    }
}

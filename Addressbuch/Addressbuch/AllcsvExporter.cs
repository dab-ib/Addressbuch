using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    internal class AllcsvExporter
    {
        static public void ExportContactsToCsv(string filePath)
        {
            try
            {
                // Lese die Daten aus der Textdatei in eine Liste
                List<string> lines = File.ReadAllLines("addressbook.txt").ToList();

                // Erstelle eine leere Liste, um alle Kontakte zu speichern
                List<string[]> contacts = new List<string[]>();

                // Gehe durch jede Zeile und teile sie in Felder auf
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');

                    // Überprüfe, ob alle Felder vorhanden sind
                    if (fields.Length == 10)
                    {
                        // Füge den Kontakt zur Liste der Kontakte hinzu
                        contacts.Add(fields);
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Zeile: {0}", line);
                    }
                }

                // Erstelle eine CSV-Datei und schreibe die Kontakte
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Schreibe die Spaltenüberschriften
                    writer.WriteLine("Vorname,Nachname,Strasse privat ,Postleitzahl privat,Ort privat,Telefon (privat), Geburtstag, E-mail-Adresse, Firma, Gruppe");

                    // Schreibe jeden Kontakt in eine neue Zeile
                    foreach (string[] contact in contacts)
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                            contact[0], contact[1], contact[2], contact[3], contact[4],
                            contact[5], contact[6], contact[7], contact[8], contact[9]);
                    }

                    Console.WriteLine($"Alle Kontakte wurden exportiert in den Pfad:{filePath}");
                    Console.WriteLine("");
                    Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                    Console.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Die Datei \"addressbook.txt\" konnte nicht gefunden werden.");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }
    }
}

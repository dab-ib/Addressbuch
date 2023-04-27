using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    public static class CsvExporter
    {
        public static void ExportToCsv(string name, string pathToAddressBook, string pathToExportCsv)
        {
            // Split name into first and last name
            var nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string lastName = nameParts.Length > 1 ? nameParts[1] : "";

            try
            {
                // Die addressbuch.txt-Datei auslesen und alle Zeilen in ein Array laden
                string[] lines = File.ReadAllLines(pathToAddressBook);

                // Alle Kontakte aus der addressbuch.txt-Datei filtern, die den angegebenen Namen haben
                var contacts = from line in lines
                               let fields = line.Split(',')
                               where fields[0].Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                     && fields[1].Equals(lastName, StringComparison.OrdinalIgnoreCase)
                               select new
                               {
                                   Name = fields[0],
                                   Nachname = fields[1],
                                   Address = fields[2],
                                   Zip = fields[3],
                                   City = fields[4],
                                   Phone = fields[5],
                                   Birthday = fields[6],
                                   Email = fields[7],
                                   Company = fields[8]
                               };

                if (contacts.Any())
                {
                    // Die CSV-Datei erstellen
                    using (var writer = new StreamWriter(pathToExportCsv))
                    {
                        // Header schreiben
                        writer.WriteLine(
                            "Vorname,Nachname,E-mail-Adresse,Telefon (privat),Strasse privat,Postleitzahl privat, Ort privat, Geburtstag, Firma");

                        foreach (var contact in contacts)
                        {
                            // Jeden Kontakt in das CSV-Format konvertieren und in die Datei schreiben
                            string csvLine =
                                $"\"{contact.Name}\",\"{contact.Nachname}\",\"{contact.Email}\",\"{contact.Phone}\",\"{contact.Address}, {contact.Zip} {contact.City}\",\"{contact.Birthday} {contact.Company}\"";
                            writer.WriteLine(csvLine);
                        }
                    }

                    Console.WriteLine($"CSV-Datei wurde erfolgreich unter {pathToExportCsv} erstellt.");
                    Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Keine Kontakte gefunden, die den Namen \"{name}\" enthalten.");
                    Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                    Console.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Die Datei {pathToAddressBook} konnte nicht gefunden werden.");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    class CsvToAddressbookConverter
    {
        public static void ConvertCsvToAddressbook(string filePath)
        {
            try
            {
                // Die CSV-Datei als Stream öffnen
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Die erste Zeile, welche den Header enthält, einlesen und ignorieren
                    string header = reader.ReadLine();

                    // Die Kontaktdaten einlesen und in die addressbook.txt schreiben
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] fields = line.Split(',');

                        if (fields.Length == 9)
                        {
                            string name = fields[0];
                            string nachname = fields[1];
                            string address = fields[2];
                            string zip = fields[3];
                            string city = fields[4];
                            string phone = fields[5];
                            string birthday = fields[6];
                            string email = fields[7];
                            string company = fields[8];
                            string group = fields[9];

                            // Den Kontakt in die addressbook.txt schreiben
                            using (StreamWriter writer = new StreamWriter("addressbook.txt", true))
                            {
                                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", name, nachname, address, zip, city, phone, birthday, email, company, group);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Zeile: {0}", line);
                            Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                            Console.ReadLine();
                        }
                    }
                }

                Console.WriteLine("CSV-Datei wurde erfolgreich in das Adressbuch importiert.");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Die Datei {filePath} wurde nicht gefunden.");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der CSV-Datei: {ex.Message}");
                Console.WriteLine("\nWarte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }
    }
}

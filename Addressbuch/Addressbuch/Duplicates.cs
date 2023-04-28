using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    // Diese Klasse kümmert sich um Duplikate.
    class Duplicates
    {
        static public void ShowDuplicates()
        {
            List<string> entries = new List<string>();
            List<string> duplicateEntries = new List<string>();

            try
            {
                // Read all entries from the file
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        entries.Add(entry);
                    }
                }

                for (int i = 0; i < entries.Count; i++)
                {
                    string[] fields1 = entries[i].Split(',');
                    for (int j = i + 1; j < entries.Count; j++)
                    {
                        string[] fields2 = entries[j].Split(',');

                        if (fields1[0] == fields2[0] && fields1[1] == fields2[1] || fields1[5] == fields2[5] ||
                            fields1[7] == fields2[7])
                        {
                            if (!duplicateEntries.Contains(entries[i]))
                            {
                                duplicateEntries.Add(entries[i]);
                            }

                            if (!duplicateEntries.Contains(entries[j]))
                            {
                                duplicateEntries.Add(entries[j]);
                            }
                        }
                    }
                }

                if (duplicateEntries.Count == 0)
                {
                    Console.WriteLine("Keine Duplikate gefunden.");
                    Console.WriteLine("\n Drücke eine Taste um fortzufahren...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"Es wurden {duplicateEntries.Count} Duplikate gefunden:");

                for (int i = 0; i < duplicateEntries.Count; i++)
                {
                    Console.WriteLine($"[{i}] {duplicateEntries[i]}");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Fehler beim Lesen der addressbook.txt Datei: " + e.Message);

                Console.WriteLine("");
                Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Es ist ein Fehler aufgetreten: " + e.Message);
                Console.WriteLine("");
                Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }




        static public void RemoveDuplicates()
        {
            Console.WriteLine("Suche nach Duplikaten...");

            List<string> entries = new List<string>();
            Dictionary<int, List<int>> duplicateIndices = new Dictionary<int, List<int>>();

            try
            {
                // Read all entries from the file
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        entries.Add(entry);
                    }
                }

                for (int i = 0; i < entries.Count; i++)
                {
                    string[] fields1 = entries[i].Split(',');

                    for (int j = i + 1; j < entries.Count; j++)
                    {
                        string[] fields2 = entries[j].Split(',');

                        if (fields1[0] == fields2[0] && fields1[1] == fields2[1] || fields1[5] == fields2[5] ||
                            fields1[7] == fields2[7])
                        {
                            if (!duplicateIndices.ContainsKey(i))
                            {
                                duplicateIndices[i] = new List<int>();
                            }

                            if (!duplicateIndices.ContainsKey(j))
                            {
                                duplicateIndices[j] = new List<int>();
                            }

                            duplicateIndices[i].Add(j);
                            duplicateIndices[j].Add(i);
                        }
                    }
                }

                if (duplicateIndices.Count == 0)
                {
                    Console.WriteLine("Keine Duplikate gefunden.");
                    Console.WriteLine("\n Drücke eine Taste um fortzufahren...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"Es wurden {duplicateIndices.Count} Duplikate gefunden:");

                foreach (var key in duplicateIndices.Keys)
                {
                    Console.WriteLine($"[{key + 1}] {entries[key]}");
                }

                Console.Write("Bitte geben Sie die Nummer des Eintrags an, den Sie löschen möchten: ");
                if (int.TryParse(Console.ReadLine(), out int indexToDelete) &&
                    duplicateIndices.ContainsKey(indexToDelete - 1))
                {
                    string entryToDelete = entries[indexToDelete - 1];
                    Console.WriteLine($"Eintrag zum Löschen: {entryToDelete}");
                    Console.Write("Möchten Sie diesen Eintrag wirklich löschen? (j/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation.ToLower() == "j")
                    {
                        string tempFile = Path.GetTempFileName();
                        using (StreamReader reader = new StreamReader("addressbook.txt"))
                        using (StreamWriter writer = new StreamWriter(tempFile))
                        {
                            while (!reader.EndOfStream)
                            {
                                string entry = reader.ReadLine();
                                if (entry != entryToDelete)
                                {
                                    writer.WriteLine(entry);
                                }
                            }
                        }

                        File.Delete("addressbook.txt");
                        File.Move(tempFile, "addressbook.txt");
                        Console.WriteLine("Eintrag gelöscht!");
                        Console.WriteLine("\n Drücke eine Taste um fortzufahren...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Löschvorgang abgebrochen.");
                        Console.WriteLine("\n Drücke eine Taste um fortzufahren...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Kein Eintrag wurde gelöscht.");
                    Console.WriteLine("\n Drücke eine Taste um fortzufahren...");
                    Console.ReadLine();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Fehler beim Lesen der addressbook.txt Datei: " + e.Message);

                Console.WriteLine("");
                Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Es ist ein Fehler aufgetreten: " + e.Message);
                Console.WriteLine("");
                Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                Console.ReadLine();
            }
        }


    }
}

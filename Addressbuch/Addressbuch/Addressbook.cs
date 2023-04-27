using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
    static class Addressbook
    {

        // Fügt einen neuen Eintrag hinzu
        static public void AddEntry()
        {
            Console.WriteLine("Neuer Eintrag:");
            Console.WriteLine("");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Nachname: ");
            string nachname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Fehler: Vorname darf nicht leer sein!");
                Console.WriteLine("Kehre zurück ins Hauptmenü!");
                return;
            }

            if (string.IsNullOrWhiteSpace(nachname))
            {
                Console.WriteLine("Fehler: Nachname darf nicht leer sein!");
                Console.WriteLine("Kehre zurück ins Hauptmenü!");
                return;
            }

            Console.Write("Adresse: ");
            string address = Console.ReadLine();
            Console.Write("Postleitzahl: ");
            string zip = Console.ReadLine();
            Console.Write("Stadt: ");
            string city = Console.ReadLine();
            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();
            Console.Write("Geburtstag (Format: TT.MM.JJJJ): ");
            string birthday = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Firma: ");
            string company = Console.ReadLine();
            Console.Write("Gruppe: ");
            string group = Console.ReadLine();

            // Verwende Standardwerte für fehlende Felder
            if (string.IsNullOrWhiteSpace(address))
            {
                address = "-";
            }

            if (string.IsNullOrWhiteSpace(zip))
            {
                zip = "-";
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                city = "-";
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = "-";
            }

            if (string.IsNullOrWhiteSpace(birthday))
            {
                birthday = "-";
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                email = "-";
            }

            if (string.IsNullOrWhiteSpace(company))
            {
                company = "-";
            }

            if (string.IsNullOrWhiteSpace(group))
            {
                group = "-";
            }

            string entry = $"{name},{nachname},{address},{zip},{city},{phone},{birthday},{email},{company},{group}";

            using (StreamWriter writer = File.AppendText("addressbook.txt"))
            {
                writer.WriteLine(entry);
            }

            Console.WriteLine("Eintrag hinzugefügt!");
        }


        // Zeigt das Adressbuch an
        static public void ShowAddressBook()
        {
            int i = 1;
            if (!File.Exists("addressbook.txt"))
            {
                Console.WriteLine("Die Datei 'addressbook.txt' wurde scheinbar gelöscht!");
                Console.WriteLine("Erstelle eine neue im Ordner oder lege einfach einen Eintrag an");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Addressbuch:");
                Console.ResetColor();
                Console.WriteLine("");

                Dictionary<string, List<string[]>> groups = new Dictionary<string, List<string[]>>();

                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields.Length == 10)
                        {
                            string groupName = fields[9];
                            if (!groups.ContainsKey(groupName))
                            {
                                groups[groupName] = new List<string[]>();
                            }
                            groups[groupName].Add(fields);
                        }
                    }
                }

                foreach (var groupName in groups.Keys.OrderBy(x => x))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Gruppe: {groupName}");
                    Console.ResetColor();

                    foreach (string[] fields in groups[groupName])
                    {
                        Console.WriteLine($"{i}:");
                        Console.WriteLine($"Name          : {fields[0]}");
                        Console.WriteLine($"Nachname      : {fields[1]}");
                        Console.WriteLine($"Adresse       : {fields[2]}");
                        Console.WriteLine($"Postleitzahl  : {fields[3]}");
                        Console.WriteLine($"Stadt         : {fields[4]}");
                        Console.WriteLine($"Telefonnummer : {fields[5]}");
                        Console.WriteLine($"Geburtstag    : {fields[6]}");
                        Console.WriteLine($"Email         : {fields[7]}");
                        Console.WriteLine($"Firma         : {fields[8]}");
                        Console.WriteLine(new string('-', 40));

                        i++;
                    }
                }
            }
        }


        // Methode um Einträge zu editieren.
        static public void EditEntry()
        {
            Console.Write("Welchen Eintrag möchten Sie bearbeiten? Bitte geben Sie den Namen an: ");
            string name = Console.ReadLine();

            string tempFile = Path.GetTempFileName();
            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    bool entryFound = false;

                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[0] == name)
                        {
                            entryFound = true;

                            Console.WriteLine($"Aktueller Eintrag:");
                            Console.WriteLine($"Name          : {fields[0]}");
                            Console.WriteLine($"Nachname      : {fields[1]}");
                            Console.WriteLine($"Adresse       : {fields[2]}");
                            Console.WriteLine($"Postleitzahl  : {fields[3]}");
                            Console.WriteLine($"Stadt         : {fields[4]}");
                            Console.WriteLine($"Telefonnummer : {fields[5]}");
                            Console.WriteLine($"Geburtstag    : {fields[6]}");
                            Console.WriteLine($"Email         : {fields[7]}");
                            Console.WriteLine($"Firma         : {fields[8]}");
                            Console.WriteLine($"Gruppe        : {fields[9]}");
                            Console.WriteLine(new string('-', 40));


                            Console.Write("Neuer Vorname (leer lassen, um unverändert zu lassen): ");
                            string newName = Console.ReadLine();
                            if (newName == "")
                            {
                                newName = fields[0];
                            }

                            Console.Write("Neuer Nachname (leer lassen, um unverändert zu lassen): ");
                            string newNachname = Console.ReadLine();
                            if (newNachname == "")
                            {
                                newNachname = fields[1];
                            }

                            Console.Write("Neue Adresse (leer lassen, um unverändert zu lassen): ");
                            string newAddress = Console.ReadLine();
                            if (newAddress == "")
                            {
                                newAddress = fields[2];
                            }

                            Console.Write("Neue Postleitzahl (leer lassen, um unverändert zu lassen): ");
                            string newZip = Console.ReadLine();
                            if (newZip == "")
                            {
                                newZip = fields[3];
                            }

                            Console.Write("Neue Stadt (leer lassen, um unverändert zu lassen): ");
                            string newCity = Console.ReadLine();
                            if (newCity == "")
                            {
                                newCity = fields[4];
                            }

                            Console.Write("Neue Telefonnummer (leer lassen, um unverändert zu lassen): ");
                            string newPhone = Console.ReadLine();
                            if (newPhone == "")
                            {
                                newPhone = fields[5];
                            }

                            Console.Write("Neuer Geburtstag TT.MM.JJJJ (leer lassen, um unverändert zu lassen): ");
                            string newBirthday = Console.ReadLine();
                            if (newBirthday == "")
                            {
                                newBirthday = fields[6];
                            }

                            Console.Write("Neue E-Mail-Adresse (leer lassen, um unverändert zu lassen): ");
                            string newEmail = Console.ReadLine();
                            if (newEmail == "")
                            {
                                newEmail = fields[7];
                            }

                            Console.Write("Neue Firma (leer lassen, um unverändert zu lassen): ");
                            string newCompany = Console.ReadLine();
                            if (newCompany == "")
                            {
                                newCompany = fields[8];
                            }

                            Console.Write("Neue Gruppe (leer lassen, um unverändert zu lassen): ");
                            string newGroup = Console.ReadLine();
                            if (newGroup == "")
                            {
                                newGroup = fields[9];
                            }

                            entry =
                                $"{newName},{newNachname},{newAddress},{newZip},{newCity},{newPhone},{newBirthday},{newEmail},{newCompany},{newGroup}";
                        }

                        writer.WriteLine(entry);
                    }

                    if (!entryFound)
                    {
                        Console.WriteLine("Eintrag nicht gefunden!");
                    }
                }

                File.Delete("addressbook.txt");
                File.Move(tempFile, "addressbook.txt");

                Console.WriteLine("Eintrag bearbeitet!");
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


        // Eintrag löschen
        static public void DeleteEntry()
        {
            Console.Write("Welchen Eintrag möchten Sie löschen? Bitte geben Sie den Vornamen an: ");
            string firstName = Console.ReadLine();
            Console.Write("Bitte geben Sie den Nachnamen an: ");
            string lastName = Console.ReadLine();

            string tempFile = Path.GetTempFileName();

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    bool entryFound = false;
                    List<string> matchingEntries = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[0] == firstName && fields[1] == lastName)
                        {
                            entryFound = true;
                            matchingEntries.Add(entry);
                        }
                        else
                        {
                            writer.WriteLine(entry);
                        }
                    }

                    if (!entryFound)
                    {
                        Console.WriteLine("Eintrag nicht gefunden!");
                    }
                    else
                    {
                        string entryToDelete = "";

                        if (matchingEntries.Count > 1)
                        {
                            Console.WriteLine("Es wurden mehrere Einträge gefunden:");
                            for (int i = 0; i < matchingEntries.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {matchingEntries[i]}");
                            }

                            Console.Write("Bitte geben Sie die Nummer des Eintrags an, den Sie löschen möchten: ");
                            if (int.TryParse(Console.ReadLine(), out int indexToDelete) && indexToDelete > 0 &&
                                indexToDelete <= matchingEntries.Count)
                            {
                                entryToDelete = matchingEntries[indexToDelete - 1];
                            }
                            else
                            {
                                Console.WriteLine("Ungültige Eingabe. Kein Eintrag wurde gelöscht.");
                                return;
                            }
                        }
                        else
                        {
                            entryToDelete = matchingEntries[0];
                        }

                        Console.WriteLine($"Eintrag zum Löschen: {entryToDelete}");
                        Console.Write("Möchten Sie diesen Eintrag wirklich löschen? (j/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation.ToLower() == "j")
                        {
                            writer.WriteLine(entryToDelete);
                            Console.WriteLine("Eintrag gelöscht!");
                        }
                        else
                        {
                            Console.WriteLine("Löschvorgang abgebrochen.");
                        }
                    }
                }

                File.Delete("addressbook.txt");
                File.Move(tempFile, "addressbook.txt");
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

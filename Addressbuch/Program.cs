using System;
using System.Net;
using System.Numerics;


namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
                Console.Write("\n \t \t |#############################|");
                Console.Write("\n \t \t |# N - Neue Adresse eingeben #|  \t");
                Console.Write("\n \t \t |# A - Datensätze anzeigen   #|  \t");
                Console.Write("\n \t \t |# S - Eintrag suchen        #|  \t");
                Console.Write("\n \t \t |# M - Einträge verwalten    #|  \t");
                Console.Write("\n \t \t |# B - Beenden               #|");
                Console.Write("\n \t \t |#############################|");
                Console.WriteLine("\n \t \t -----------------------------");


                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "N":
                        AddEntry();
                        break;
                    case "A":
                        ShowAddressBook();
                        break;
                    case "S":
                        SearchEntry();
                        break;
                    case "M":
                        ShowSubMenu();
                        break;
                    case "B":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }

        static void ShowSubMenu()
        {
            while (true)
            {
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
                Console.Write("\n \t \t |#############################|");
                Console.Write("\n \t \t |# B - Eintrag bearbeiten   #|  \t");
                Console.Write("\n \t \t |# L - Eintrag löschen      #|  \t");
                Console.Write("\n \t \t |# D - Duplikate entfernen  #|  \t");
                Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü #|");
                Console.Write("\n \t \t |#############################|");
                Console.WriteLine("\n \t \t -----------------------------");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "B":
                        EditEntry();
                        break;
                    case "L":
                        DeleteEntry();
                        break;
                    case "D":
                        RemoveDuplicates();
                        break;
                    case "Z":
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }


        static void AddEntry()
        {
            Console.WriteLine("Neuer Eintrag:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Nachname: ");
            string nachname = Console.ReadLine();
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

            // Verwende Standardwerte für fehlende Felder
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "-";
            }
            if (string.IsNullOrWhiteSpace(nachname))
            {
                nachname = "-";
            }
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

            string entry = $"{name},{nachname},{address},{zip},{city},{phone},{birthday},{email},{company}";

            using (StreamWriter writer = File.AppendText("addressbook.txt"))
            {
                writer.WriteLine(entry);
            }

            Console.WriteLine("Eintrag hinzugefügt!");
        }


        static void ShowAddressBook()
        {
            if (!File.Exists("addressbook.txt"))
            {
                Console.WriteLine("Das Adressbuch ist leer!");
                return;
            }

            Console.WriteLine("Addressbuch:");

            if (!File.Exists("addressbook.txt"))
            {
                Console.WriteLine("Das Adressbuch ist leer!");
                return;
            }
            else
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields.Length == 9)
                        {
                            Console.WriteLine($"Name: {fields[0]}");
                            Console.WriteLine($"Nachname: {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                        }
                    }

                }
            }
        }


        static void EditEntry()
        {
            Console.Write("Welchen Eintrag möchten Sie bearbeiten? Bitte geben Sie den Namen an: ");
            string name = Console.ReadLine();

            string tempFile = Path.GetTempFileName();
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
                        Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                        Console.WriteLine($"Adresse: {fields[2]}");
                        Console.WriteLine($"Postleitzahl: {fields[3]}");
                        Console.WriteLine($"Stadt: {fields[4]}");
                        Console.WriteLine($"Telefonnummer: {fields[5]}");
                        Console.WriteLine($"Geburtstag: {fields[6]}");
                        Console.WriteLine($"Email: {fields[7]}");
                        Console.WriteLine($"Firma: {fields[8]}");

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
                        string newBirthday= Console.ReadLine();
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
                        entry =
                        $"{newName},{newNachname},{newAddress},{newZip},{newCity},{newPhone},{newBirthday},{newEmail},{newCompany}";
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



        static void DeleteEntry()
        {
            Console.Write("Welchen Eintrag möchten Sie löschen? Bitte geben Sie den Nachnamen an: ");
            string name = Console.ReadLine();

            string tempFile = Path.GetTempFileName();
            using (StreamReader reader = new StreamReader("addressbook.txt"))
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                bool entryFound = false;

                while (!reader.EndOfStream)
                {
                    string entry = reader.ReadLine();
                    string[] fields = entry.Split(',');

                    if (fields[1] == name)
                    {
                        entryFound = true;
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
            }

            File.Delete("addressbook.txt");
            File.Move(tempFile, "addressbook.txt");

            Console.WriteLine("Eintrag gelöscht!");
        }
        static void SearchEntry()
        {
            Console.Write("Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie einen Namen oder eine Telefonnummer ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie einen Namen oder eine Telefonnummer ein.");
                return;
            }

            bool found = false;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[0].ToLower().Contains(searchQuery) || fields[1].ToLower().Contains(searchQuery))
                        {
                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();

                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Kein passender Eintrag gefunden.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Die Adressbuch-Datei wurde nicht gefunden!");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {e.Message}");
            }
        }

        static void RemoveDuplicates()
        {
            List<string> uniqueEntries = new List<string>();
            List<string> duplicateEntries = new List<string>();

            // read all entries from file
            using (StreamReader reader = new StreamReader("addressbook.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string entry = reader.ReadLine();

                    if (uniqueEntries.Contains(entry))
                    {
                        // entry is already in list, so it's a duplicate
                        duplicateEntries.Add(entry);
                    }
                    else
                    {
                        // entry is not in list, so add it to uniqueEntries
                        uniqueEntries.Add(entry);
                    }
                }
            }

            if (duplicateEntries.Count == 0)
            {
                Console.WriteLine("Keine Duplikate gefunden.");
                return;
            }

            Console.WriteLine($"Es wurden {duplicateEntries.Count} Duplikate gefunden:");

            for (int i = 0; i < duplicateEntries.Count; i++)
            {
                Console.WriteLine($"[{i}] {duplicateEntries[i]}");
            }

            Console.Write("Welches Duplikat möchten Sie entfernen? (Geben Sie eine Zahl ein): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 0 || index >= duplicateEntries.Count)
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            // remove selected duplicate from list
            duplicateEntries.RemoveAt(index);

            // overwrite file with unique entries and remaining duplicates
            using (StreamWriter writer = new StreamWriter("addressbook.txt"))
            {
                foreach (string entry in uniqueEntries)
                {
                    writer.WriteLine(entry);
                }

                foreach (string duplicate in duplicateEntries)
                {
                    writer.WriteLine(duplicate);
                }
            }

            Console.WriteLine("Duplikat entfernt.");
        }




    }
}

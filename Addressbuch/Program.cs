using System;


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
                Console.WriteLine("S - Eintrag suchen");
                Console.Write("\n \t \t |# E - Eintrag bearbeiten    #|  \t");
                Console.Write("\n \t \t |# L - Eintrag löschen       #|  \t");
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
                    case "E":
                        EditEntry();
                        break;
                    case "L":
                        DeleteEntry();
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

        static void AddEntry()
        {
            Console.WriteLine("Neuer Eintrag:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Adresse: ");
            string address = Console.ReadLine();
            Console.Write("Postleitzahl: ");
            string zip = Console.ReadLine();
            Console.Write("Stadt: ");
            string city = Console.ReadLine();
            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();
            Console.Write("Geburtstag (Format: DD.MM.YYYY): ");
            string birthday = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Firma: ");
            string company = Console.ReadLine();

            string entry = $"{name},{address},{zip},{city},{phone},{birthday},{email},{company}";

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

            using (StreamReader reader = new StreamReader("addressbook.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string entry = reader.ReadLine();
                    string[] fields = entry.Split(',');

                    Console.WriteLine($"Name: {fields[0]}");
                    Console.WriteLine($"Adresse: {fields[1]}");
                    Console.WriteLine($"Postleitzahl: {fields[2]}");
                    Console.WriteLine($"Stadt: {fields[3]}");
                    Console.WriteLine($"Telefonnummer: {fields[4]}");
                    Console.WriteLine($"Geburtstag: {fields[5]}");
                    Console.WriteLine($"Email: {fields[6]}");
                    Console.WriteLine($"Firma: {fields[7]}");
                    Console.WriteLine();
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
                        Console.WriteLine($"Name: {fields[0]}");
                        Console.WriteLine($"Adresse: {fields[1]}");
                        Console.WriteLine($"Telefonnummer: {fields[2]}");
                        Console.WriteLine($"Geburtsdatum: {fields[3]}");
                        Console.WriteLine($"Postleitzahl: {fields[4]}");
                        Console.WriteLine($"Stadt: {fields[5]}");
                        Console.WriteLine($"E-Mail: {fields[6]}");
                        Console.WriteLine($"Firma: {fields[7]}");

                        Console.Write("Neuer Name (leer lassen, um unverändert zu lassen): ");
                        string newName = Console.ReadLine();
                        if (newName == "")
                        {
                            newName = fields[0];
                        }

                        Console.Write("Neue Adresse (leer lassen, um unverändert zu lassen): ");
                        string newAddress = Console.ReadLine();
                        if (newAddress == "")
                        {
                            newAddress = fields[1];
                        }

                        Console.Write("Neue Telefonnummer (leer lassen, um unverändert zu lassen): ");
                        string newPhone = Console.ReadLine();
                        if (newPhone == "")
                        {
                            newPhone = fields[2];
                        }

                        Console.Write("Neues Geburtsdatum (leer lassen, um unverändert zu lassen): ");
                        string newBirthday = Console.ReadLine();
                        if (newBirthday == "")
                        {
                            newBirthday = fields[3];
                        }

                        Console.Write("Neue Postleitzahl (leer lassen, um unverändert zu lassen): ");
                        string newZipCode = Console.ReadLine();
                        if (newZipCode == "")
                        {
                            newZipCode = fields[4];
                        }

                        Console.Write("Neue Stadt (leer lassen, um unverändert zu lassen): ");
                        string newCity = Console.ReadLine();
                        if (newCity == "")
                        {
                            newCity = fields[5];
                        }

                        Console.Write("Neue E-Mail-Adresse (leer lassen, um unverändert zu lassen): ");
                        string newEmail = Console.ReadLine();
                        if (newEmail == "")
                        {
                            newEmail = fields[6];
                        }

                        Console.Write("Neue Firma (leer lassen, um unverändert zu lassen): ");
                        string newCompany = Console.ReadLine();
                        if (newCompany == "")
                        {
                            newCompany = fields[7];
                        }

                        entry =
                            $"{newName},{newAddress},{newPhone},{newBirthday},{newZipCode},{newCity},{newEmail},{newCompany}";
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
            Console.Write("Welchen Eintrag möchten Sie löschen? Bitte geben Sie den Namen an: ");
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

        // Diese erweiterte Version ermöglicht es dem Benutzer, nach Einträgen zu suchen, die bestimmte Kriterien erfüllen, wie z.B. den Geburtstag, die Postleitzahl, die Stadt, die E-Mail-Adresse oder die Firma. Es werden nur die Einträge angezeigt, die alle Kriterien erfüllen, die der Benutzer eingegeben hat.
        static void SearchEntry()
        {
            Console.WriteLine("Nach welchen Kriterien möchten Sie suchen?");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Adresse: ");
            string address = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Firma: ");
            string company = Console.ReadLine();

            bool foundEntry = false;

            using (StreamReader reader = new StreamReader("addressbook.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string entry = reader.ReadLine();
                    string[] fields = entry.Split(',');

                    if ((name == "" || fields[0].Contains(name)) &&
                        (address == "" || fields[1].Contains(address)) &&
                        (phone == "" || fields[2].Contains(phone)) &&
                        (email == "" || fields[3].Contains(email)) &&
                        (company == "" || fields[4].Contains(company)))
                    {
                        Console.WriteLine($"Name: {fields[0]}");
                        Console.WriteLine($"Adresse: {fields[1]}");
                        Console.WriteLine($"Telefonnummer: {fields[2]}");
                        Console.WriteLine($"Email: {fields[3]}");
                        Console.WriteLine($"Firma: {fields[4]}");
                        Console.WriteLine();
                        foundEntry = true;
                    }
                }
            }

            if (!foundEntry)
            {
                Console.WriteLine("Kein passender Eintrag gefunden.");
            }
        }



    }
}

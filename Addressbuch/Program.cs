using System.Text;
using AddressBook;

while (true)
{
    Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
    Console.Write("\n \t \t |#############################|");
    Console.Write("\n \t \t |# N - Neue Adresse eingeben #|  \t");
    Console.Write("\n \t \t |# A - Datensätze anzeigen   #|  \t");
    Console.Write("\n \t \t |# G - heutige Geburtstage   #|  \t");
    Console.Write("\n \t \t |# S - Eintrag suchen        #|  \t");
    Console.Write("\n \t \t |# M - Einträge verwalten    #|  \t");
    Console.Write("\n \t \t |# E - Export-Menü anzeigen  #|  \t");
    Console.Write("\n \t \t |# B - Beenden               #|");
    Console.Write("\n \t \t |#############################|");
    Console.WriteLine("\n \t \t -----------------------------");


    string input = Console.ReadLine();

    switch (input.ToUpper())
    {
        case "N":
            Addressbook.AddEntry();
            break;
        case "A":
            Addressbook.ShowAddressBook();
            break;
        case "S":
            Search.SearchEntry();
            break;
        case "M":
            ShowSubMenu();
            break;
        case "B":
            Environment.Exit(0);
            break;
        case "G":
            Birthday.BirthdayToday();
            break;
        case "E":
            ShowExportMenu();
            break;
        default:
            Console.WriteLine("Ungültige Eingabe!");
            break;
    }
}

void ShowExportMenu()
{
    while (true)
    {
        Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
        Console.Write("\n \t \t |#############################|");
        Console.Write("\n \t \t |# P - Als PDF exportieren   #|  \t");
        Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü  #|");
        Console.Write("\n \t \t |#############################|");
        Console.WriteLine("\n \t \t -----------------------------");

        string input = Console.ReadLine();

        switch (input.ToUpper())
        {
            case "P":
                var pdfExport = new PDF_Export("addressbook.txt");
                if (pdfExport.Export())
                {
                    Console.WriteLine("PDF successfully exported.");
                }
                else
                {
                    Console.WriteLine("PDF export failed.");
                }
                break;
            case "Z":
                return;
            default:
                Console.WriteLine("Ungültige Eingabe!");
                break;
        }
    }
}

void ShowSubMenu()
{
    while (true)
    {
        Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
        Console.Write("\n \t \t |#############################|");
        Console.Write("\n \t \t |# B - Eintrag bearbeiten   #|  \t");
        Console.Write("\n \t \t |# L - Eintrag löschen      #|  \t");
        Console.Write("\n \t \t |# F - Duplikate anzeigen   #|  \t");
        Console.Write("\n \t \t |# D - Duplikate entfernen  #|  \t");
        Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü #|");
        Console.Write("\n \t \t |#############################|");
        Console.WriteLine("\n \t \t -----------------------------");

        string input = Console.ReadLine();

        switch (input.ToUpper())
        {
            case "B":
                Addressbook.EditEntry();
                break;
            case "L":
                Addressbook.DeleteEntry();
                break;
            case "D":
                Duplicates.RemoveDuplicates();
                break;
            case "F":
                Duplicates.ShowDuplicates();
                break;
            case "Z":
                return;
            default:
                Console.WriteLine("Ungültige Eingabe!");
                break;
        }
    }
}


namespace AddressBook
{
    static class Addressbook
    {

        // Fügt einen neuen Eintrag hinzu
        static public void AddEntry()
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

        // Zeigt das Adressbuch an
        static public void ShowAddressBook()
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

        // Methode um Einträge zu editieren.
        static public void EditEntry()
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


        // Eintrag löschen
        static public void DeleteEntry()
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
    }

    // Diese Klasse enthält die Methoden, die für die Suche nach Einträgen zuständig sind.
    class Search
    {
        static public void SearchEntry()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie einen Namen oder eine Telefonnummer ein: ");
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
    }

    // Diese Klasse kümmert sich um Duplicate.
    class Duplicates
    {
        static public void ShowDuplicates()
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
        }

        static public void RemoveDuplicates()
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

    // Class to give out todays Birthday of Adressbook.txt with age
    class Birthday
    {
        public static void BirthdayToday()
        {
            string[] lines = File.ReadAllLines("addressbook.txt");
            string[] fields = new string[9];
            string[] birthday = new string[3];
            string[] today = new string[3];
            int age = 0;
            int todayDay = DateTime.Now.Day;
            int todayMonth = DateTime.Now.Month;
            int todayYear = DateTime.Now.Year;
            int birthdayDay = 0;
            int birthdayMonth = 0;
            int birthdayYear = 0;
            bool found = false;

            foreach (string line in lines)
            {
                fields = line.Split(',');
                birthday = fields[6].Split('.');
                birthdayDay = Convert.ToInt32(birthday[0]);
                birthdayMonth = Convert.ToInt32(birthday[1]);
                birthdayYear = Convert.ToInt32(birthday[2]);

                if (todayDay == birthdayDay && todayMonth == birthdayMonth)
                {
                    age = todayYear - birthdayYear;
                    Console.WriteLine($"Heute hat {fields[0]} {fields[1]} Geburtstag und wird {age} Jahre alt.");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Heute hat niemand Geburtstag.");
            }
        }
    }

    public class PDF_Export
    {
        private const int PageWidth = 595; // A4 size in points
        private const int PageHeight = 842;

        private const int MarginLeft = 50;
        private const int MarginRight = 50;
        private const int MarginTop = 50;
        private const int MarginBottom = 50;

        private const int LineSpacing = 15;

        private readonly string _fileName;

        public PDF_Export(string fileName)
        {
            _fileName = fileName;
        }

        public bool Export()
        {
            if (!File.Exists(_fileName))
            {
                Console.WriteLine($"File '{_fileName}' does not exist.");
                return false;
            }

            try
            {
                var lines = File.ReadAllLines(_fileName);

                var sb = new StringBuilder();

                // Generate the PDF header
                sb.AppendLine("%PDF-1.7");

                // Generate the font dictionary
                sb.AppendLine("1 0 obj");
                sb.AppendLine("<< /Type /Font");
                sb.AppendLine("/Subtype /Type1");
                sb.AppendLine("/BaseFont /Helvetica");
                sb.AppendLine(">>");
                sb.AppendLine("endobj");

                // Generate the content stream
                sb.AppendLine("2 0 obj");
                sb.AppendLine("<< /Length 3 0 R >>");
                sb.AppendLine("stream");
                sb.AppendLine("BT");
                sb.AppendLine("/F1 12 Tf");

                var y = PageHeight - MarginTop;

                foreach (var line in lines)
                {
                    var fields = line.Split(',');

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0} {1}) Tj", fields[0], fields[1]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0}) Tj", fields[2]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0} {1}) Tj", fields[3], fields[4]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0}) Tj", fields[5]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0}) Tj", fields[6]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0}) Tj", fields[7]);
                    sb.AppendLine();

                    y -= LineSpacing;

                    sb.AppendFormat("{0} {1} Td", MarginLeft, y);
                    sb.AppendFormat("({0}) Tj", fields[8]);
                    sb.AppendLine();

                    y -= LineSpacing * 2;
                }

                sb.AppendLine("ET");
                sb.AppendLine("endstream");
                sb.AppendLine("endobj");

                // Generate the pages object
                sb.AppendLine("4 0 obj");
                sb.AppendLine("<< /Type /Pages");
                sb.AppendLine("/Kids [3 0 R]");
                sb.AppendLine("/Count 1 >>");
                sb.AppendLine("endobj");

                // Generate the catalog object
                sb.AppendLine("5 0 obj");
                sb.AppendLine("<< /Type /Catalog");
                sb.AppendLine("/Pages 4 0 R >>");
                sb.AppendLine("endobj");

                // Generate the cross-reference table
                sb.AppendFormat("xref\n0 {0}\n0000000000 65535 f \n", 6 + lines.Length * 2);
                sb.AppendLine("0000000010 00000 n ");
                sb.AppendLine("0000000065 00000 n ");
                sb.AppendLine("0000000116 00000 n ");
                sb.AppendLine("0000000355 00000 n ");
                sb.AppendLine("0000000454 00000 n ");
                sb.AppendFormat("{0:0000000000} {1:00000} n \n", sb.Length, 0);
                sb.AppendFormat("{0:0000000010} {1:00000} n \n", sb.Length, 0);

                // Generate the trailer
                sb.AppendLine("trailer");
                sb.AppendLine("<< /Size 6");
                sb.AppendLine("/Root 5 0 R >>");
                sb.AppendLine("startxref");
                sb.AppendLine(sb.Length.ToString());
                sb.AppendLine("%%EOF");

                // Save the PDF to disk
                var pdfFile = Path.GetFileNameWithoutExtension(_fileName) + ".pdf";
                File.WriteAllText(pdfFile, sb.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting PDF: {ex.Message}");
                return false;
            }
        }
    }
}
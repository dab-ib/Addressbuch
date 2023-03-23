using System.Text;
using System.IO;
using System.Reflection;
using AddressBook;

class Program
{
    static void Main(string[] args)
    {
        //AddressBook.Menu.Hauptmenu();
        PLZHelper.ReadLine();
    }
}

namespace AddressBook
{
    class Menu
    {
        public static void Hauptmenu()
        {
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
                        SubMenu.ShowSubMenu();
                        break;
                    case "B":
                        Environment.Exit(0);
                        break;
                    case "G":
                        Birthday.BirthdayToday();
                        break;
                    case "E":
                        SubMenu.ShowExportMenu();
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }

    class SubMenu
    {
        static public void ShowExportMenu()
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
                        PDF_Export.WriteAddressBookToPDF("addressbook.txt", "addressbook.pdf");
                        break;
                    case "Z":
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }

        static public void ShowSubMenu()
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
    }

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

        static public void WriteAddressBookToPDF(string inputFilename, string outputFilename)
        {
            string[] lines = File.ReadAllLines(inputFilename);
            StringBuilder pdfContent = new StringBuilder();

            pdfContent.AppendLine("%PDF-1.5");
            pdfContent.AppendLine("1 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Type /Catalog");
            pdfContent.AppendLine("/Outlines 2 0 R");
            pdfContent.AppendLine("/Pages 3 0 R");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("endobj");

            pdfContent.AppendLine("2 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Type /Outlines");
            pdfContent.AppendLine("/Count 0");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("endobj");

            pdfContent.AppendLine("3 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Type /Pages");
            pdfContent.AppendLine("/Kids [4 0 R]");
            pdfContent.AppendLine("/Count 1");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("endobj");

            pdfContent.AppendLine("4 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Type /Page");
            pdfContent.AppendLine("/Parent 3 0 R");
            pdfContent.AppendLine("/MediaBox [0 0 612 792]");
            pdfContent.AppendLine("/Contents 5 0 R");
            pdfContent.AppendLine("/Resources <<");
            pdfContent.AppendLine("/Font <<");
            pdfContent.AppendLine("/F1 6 0 R");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("endobj");

            StringBuilder textContent = new StringBuilder();
            foreach (string line in lines)
            {
                textContent.AppendLine("BT");
                textContent.AppendFormat("/F1 12 Tf 0 0 0 rg 50 {0} Td", 750 - 15 * Array.IndexOf(lines, line));
                textContent.AppendFormat("({0}) Tj", line);
                textContent.AppendLine("ET");
            }

            pdfContent.AppendLine("5 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Length " + textContent.Length);
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("stream");
            pdfContent.Append(textContent);
            pdfContent.AppendLine("endstream");
            pdfContent.AppendLine("endobj");

            pdfContent.AppendLine("6 0 obj");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Type /Font");
            pdfContent.AppendLine("/Subtype /Type1");
            pdfContent.AppendLine("/Name /F1");
            pdfContent.AppendLine("/BaseFont /Helvetica");
            pdfContent.AppendLine("/Encoding /WinAnsiEncoding");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("endobj");

            pdfContent.AppendLine("xref");
            pdfContent.AppendLine("0 7");
            pdfContent.AppendLine("0000000000 65535 f");
            pdfContent.AppendLine("0000000010 00000 n");
            pdfContent.AppendLine("0000000079 00000 n");
            pdfContent.AppendLine("0000000173 00000 n");
            pdfContent.AppendLine("0000000301 00000 n");
            pdfContent.AppendLine("0000000419 00000 n");
            pdfContent.AppendLine("0000000543 00000 n");

            pdfContent.AppendLine("trailer");
            pdfContent.AppendLine("<<");
            pdfContent.AppendLine("/Size 7");
            pdfContent.AppendLine("/Root 1 0 R");
            pdfContent.AppendLine(">>");
            pdfContent.AppendLine("startxref");
            pdfContent.AppendLine("0000000631");
            pdfContent.AppendLine("%%EOF");

            File.WriteAllText(outputFilename, pdfContent.ToString());
        }
    }

    class PLZHelper
    {
        private void ReadLine()
        {
            static void Main(string[] args)
            {
                List<MyData> data = ReadEmbeddedCsvFile("PLZ_2021");

                foreach (MyData item in data)
                {
                    Console.WriteLine("PLZ: {0}, Ort: {1}, Ortsteil: {2}", item.PLZ, item.Ort, item.Ortsteil);
                }
            }

            static List<MyData> ReadEmbeddedCsvFile(string resourceName)
            {
                List<MyData> data = new List<MyData>();

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
                            string[] fields = line.Split(',');

                            if (fields.Length == 3)
                            {
                                // Neue Instanz von MyData erstellen
                                MyData item = new MyData();

                                // Die Felder der Instanz zuweisen
                                item.PLZ = fields[0];
                                item.Ort = fields[1];
                                item.Ortsteil = fields[2];

                                // Die Instanz zur Liste hinzufügen
                                data.Add(item);
                            }
                            else
                            {
                                Console.WriteLine("Ungültige Zeile: {0}", line);
                            }
                        }
                    }
                }

                return data;
            }
        }

        class MyData
        {
            public string PLZ { get; set; }
            public string Ort { get; set; }
            public string Ortsteil { get; set; }
        }
    }
}

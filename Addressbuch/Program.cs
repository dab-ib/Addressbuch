using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using AddressBook;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        AddressBook.Menu.Hauptmenu();
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
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t ----------------------------------------");
                Console.Write("\n \t \t |######################################|");
                Console.Write("\n \t \t |# N - Neuer Eintrag anlegen          #|  \t");
                Console.Write("\n \t \t |# A - Gesamtes Adressbuch anzeigen   #|  \t");
                Console.Write("\n \t \t |# G - heutige Geburtstage            #|  \t");
                Console.Write("\n \t \t |# M - Einträge verwalten             #|  \t");
                Console.Write("\n \t \t |# S - Such-Menü anzeigen             #|  \t");
                Console.Write("\n \t \t |# E - Export-Menü anzeigen           #|  \t");
                Console.Write("\n \t \t |# I - Import-Menü anzeigen           #|  \t");
                Console.Write("\n \t \t |# B - Programm Beenden               #|");
                Console.Write("\n \t \t |######################################|");
                Console.WriteLine("\n \t \t ----------------------------------------");


                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "N":
                        Console.Clear();
                        Addressbook.AddEntry();
                        break;
                    case "A":
                        Console.Clear();
                        Addressbook.ShowAddressBook();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        break;
                    case "S":
                        Console.Clear();
                        SearchMenu.ShowSearchMenu();
                        break;
                    case "M":
                        Console.Clear();
                        SubMenu.ShowSubMenu();
                        break;
                    case "B":
                        Environment.Exit(0);
                        break;
                    case "G":
                        Console.Clear();
                        Birthday.BirthdayToday();
                        break;
                    case "E":
                        Console.Clear();
                        ExportMenu.ShowExportMenu();
                        break;
                    case "I":
                        Console.Clear();
                        ImportMenu.ShowImportMenu();
                        break;
                    case "T":
                        PLZHelper.Einlesen();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }

    class ExportMenu
    {
        static public void ShowExportMenu()
        {
            while (true)
            {
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
                Console.Write("\n \t \t |#########################################|");
                Console.Write("\n \t \t |# C - Ein Kontakt als CSV exportieren   #|  \t");
                Console.Write("\n \t \t |# A - Gesamtes Adressbuch exportieren   #|  \t");
                Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü              #|");
                Console.Write("\n \t \t |#########################################|");
                Console.WriteLine("\n \t \t -----------------------------");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "C":
                        Console.Clear();
                        Console.WriteLine("Name des zu exportierenden Kontakts:");
                        string input1 = Console.ReadLine();
                        CsvExporter.ExportToCsv(input1, "addressbook.txt", "Kontakt_" + input1 + ".csv");
                        break;
                    case "A":
                        Console.Clear();
                        AllcsvExporter.ExportContactsToCsv("AlleKontakte.csv");
                        break;
                    case "Z":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }

    class SubMenu
    {
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
                Console.Write("\n \t \t |# X - Alles Löschen        #|  \t");
                Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü #|");
                Console.Write("\n \t \t |#############################|");
                Console.WriteLine("\n \t \t -----------------------------");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "B":
                        Console.Clear();
                        Addressbook.EditEntry();
                        break;
                    case "L":
                        Console.Clear();
                        Addressbook.DeleteEntry();
                        break;
                    case "D":
                        Console.Clear();
                        Duplicates.RemoveDuplicates();
                        break;
                    case "F":
                        Console.Clear();
                        Duplicates.ShowDuplicates();
                        break;
                    case "X":
                        Console.Clear();
                        DeleteAll.DeleteFile();
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

    class ImportMenu { 
        static public void ShowImportMenu()
        {
            while (true)
            {
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
                Console.Write("\n \t \t |#########################################|");
                Console.Write("\n \t \t |# I - Aus einer CSV Datei Importieren   #|  \t");
                Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü              #|");
                Console.Write("\n \t \t |#########################################|");
                Console.WriteLine("\n \t \t -----------------------------");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "I":
                        Console.WriteLine("Die CSV Datei muss wie folgt aufgebaut sein, damit diese erfolgreich importiert werden kann:");
                        Console.WriteLine("Vorname, Nachname, Strasse privat, Postleitzahl privat, Ort privat, Telefon (privat), Geburtstag, E-mail-Adresse, Firma");
                        Console.WriteLine("Dateipfad der zu importierenden CSV-Datei:");
                        string input1 = Console.ReadLine();
                        CsvToAddressbookConverter.ConvertCsvToAddressbook(input1);
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

    class SearchMenu
    {
        static public void ShowSearchMenu()
        {
            while (true)
            {
                Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
                Console.Write("\n \t \t |#########################################|");
                Console.Write("\n \t \t |# N - Nach Namen suchen                 #|  \t");
                Console.Write("\n \t \t |# T - Nach Telefonnummer suchen         #|  \t");
                Console.Write("\n \t \t |# E - Nach E-Mail suchen                #|  \t");
                Console.Write("\n \t \t |# A - Nach Adresse suchen               #|  \t");
                Console.Write("\n \t \t |# F - Nach Firma suchen                 #|  \t");
                Console.Write("\n \t \t |# Z - Zurück zum Hauptmenü              #|");
                Console.Write("\n \t \t |#########################################|");
                Console.WriteLine("\n \t \t -----------------------------");

                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "N":
                        SearchEntryName.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "T":
                        SearchEntryNumber.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "E":
                        SearchEntryEmail.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "A":
                        SearchEntryAdress.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "F":
                        SearchEntryFirma.Search();
                        Console.WriteLine("");
                        Console.WriteLine("Warte auf Eingabe um fortzufahren...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "Z":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
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
            Console.WriteLine("");
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

            string forbiddenentry = "-,-,-,-,-,-,-,-,-";

            if (entry.Equals(forbiddenentry))
            {
                Console.WriteLine("Fehler: Alle Felder eingetragenen Felder sind leer!");
                Console.WriteLine("Kehre zurück ins Hauptmenü!");
            }
            else
            {
                using (StreamWriter writer = File.AppendText("addressbook.txt"))
                {
                    writer.WriteLine(entry);
                }

                Console.WriteLine("Eintrag hinzugefügt!");
            }
        }

        // Zeigt das Adressbuch an
        static public void ShowAddressBook()
        {
            int i = 1;
            if (!File.Exists("addressbook.txt"))
            {
                Console.WriteLine("Das Adressbuch ist leer!");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Addressbuch:");
                Console.ResetColor();
                Console.WriteLine("");

                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields.Length == 9)
                        {
                            Console.WriteLine(i + ":");
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
                            i++;
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
    class SearchEntryName
    {
        static public void Search()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie einen Vornamen oder Nachnamen oder beides ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie einen Vornamen oder Nachnamen oder beides ein.");
                return;
            }

            bool found = false;
            int foundnumber = 1;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');
                        string firstname = fields[0];
                        string lastname = fields[1];
                        string fullname = firstname + " " + lastname;

                        if (fields[0].ToLower().Contains(searchQuery) || fields[1].ToLower().Contains(searchQuery) || fullname.ToLower().Contains(searchQuery))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Gefundener Eintrag Nr." + foundnumber + ":");
                            Console.ResetColor();
                            Console.WriteLine();

                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                            foundnumber++;
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

    class SearchEntryNumber
    {
        static public void Search()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie eine Telefonnummer ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie eine Telefonnummer ein.");
                return;
            }

            bool found = false;
            int foundnumber = 1;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[5].Contains(searchQuery))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Gefundener Eintrag Nr." + foundnumber + ":");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                            foundnumber++;
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

    class SearchEntryEmail
    {
        static public void Search()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie eine E-Mail Adresse ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie eine E-Mail Adresse ein.");
                return;
            }

            bool found = false;
            int foundnumber = 1;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[7].ToLower().Contains(searchQuery))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Gefundener Eintrag Nr." + foundnumber + ":");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                            foundnumber++;
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

    class SearchEntryFirma
    {
        static public void Search()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie eine Firma ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie eine Firma ein.");
                return;
            }

            bool found = false;
            int foundnumber = 1;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[8].ToLower().Contains(searchQuery))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Gefundener Eintrag Nr." + foundnumber + ":");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                            foundnumber++;
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

    class SearchEntryAdress
    {
        static public void Search()
        {
            Console.Write(
                "Nach welchem Eintrag möchten Sie suchen? Bitte geben Sie eine Adresse ein: ");
            string searchQuery = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie eine Adresse ein.");
                return;
            }

            bool found = false;
            int foundnumber = 1;

            try
            {
                using (StreamReader reader = new StreamReader("addressbook.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string entry = reader.ReadLine();
                        string[] fields = entry.Split(',');

                        if (fields[2].ToLower().Contains(searchQuery))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Gefundener Eintrag Nr." + foundnumber + ":");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine($"Name: {fields[0]} {fields[1]}");
                            Console.WriteLine($"Adresse: {fields[2]}");
                            Console.WriteLine($"Postleitzahl: {fields[3]}");
                            Console.WriteLine($"Stadt: {fields[4]}");
                            Console.WriteLine($"Telefonnummer: {fields[5]}");
                            Console.WriteLine($"Geburtstag: {fields[6]}");
                            Console.WriteLine($"Email: {fields[7]}");
                            Console.WriteLine($"Firma: {fields[8]}");
                            Console.WriteLine();
                            foundnumber++;
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

    public static class DeleteAll
    {
        public static void DeleteFile()
        {
            Console.WriteLine("Sind Sie sich sicher alle Kontakte zu löschen? Antworten Sie mit 'Ja' oder 'Nein'.");
            string input = Console.ReadLine();
            if (input == "Ja")
            {
                Console.WriteLine("Alle Kontakte werden nun gelöscht. Und das Programm wird geschlossen.");
                File.Delete("addressbook.txt");
                Environment.Exit(0);
            }
            else if(input == "Nein")
            {
                Console.WriteLine("Die Kontakte werden nicht gelöscht.");
            }
            else
            {
                Console.WriteLine("Falsche Eingabe.");
            }
        }
    }

    public static class AllcsvExporter
    {

        static public void ExportContactsToCsv(string filePath)
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
                if (fields.Length == 9)
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
                writer.WriteLine("Vorname,Nachname,Strasse privat ,Postleitzahl privat,Ort privat,Telefon (privat), Geburtstag, E-mail-Adresse, Firma");

                // Schreibe jeden Kontakt in eine neue Zeile
                foreach (string[] contact in contacts)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                        contact[0], contact[1], contact[2], contact[3], contact[4],
                        contact[5], contact[6], contact[7], contact[8]);
                }
            }
        }

    }

    public static class CsvExporter
    {
        public static void ExportToCsv(string name, string pathToAddressBook, string pathToExportCsv)
        {
            // Die addressbuch.txt-Datei auslesen und alle Zeilen in ein Array laden
            string[] lines = File.ReadAllLines(pathToAddressBook);

            // Alle Kontakte aus der addressbuch.txt-Datei filtern, die den angegebenen Namen haben
            var contacts = from line in lines
                           let fields = line.Split(',')
                           where fields[0].Equals(name, StringComparison.OrdinalIgnoreCase)
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
                    writer.WriteLine("Vorname,Nachname,E-mail-Adresse,Telefon (privat),Strasse privat,Postleitzahl privat, Ort privat, Geburtstag, Firma");

                    foreach (var contact in contacts)
                    {
                        // Jeden Kontakt in das CSV-Format konvertieren und in die Datei schreiben
                        string csvLine = $"\"{contact.Name}\",\"{contact.Nachname}\",\"{contact.Email}\",\"{contact.Phone}\",\"{contact.Address}, {contact.Zip} {contact.City}\",\"{contact.Birthday} {contact.Company}\"";
                        writer.WriteLine(csvLine);
                    }
                }

                Console.WriteLine($"CSV-Datei wurde erfolgreich unter {pathToExportCsv} erstellt.");
            }
            else
            {
                Console.WriteLine($"Keine Kontakte gefunden, die den Namen \"{name}\" enthalten.");
            }
        }
    }

    class CsvToAddressbookConverter
    {
        public static void ConvertCsvToAddressbook(string filePath)
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

                        // Den Kontakt in die addressbook.txt schreiben
                        using (StreamWriter writer = new StreamWriter("addressbook.txt", true))
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", name, nachname, address, zip, city, phone, birthday, email, company);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Zeile: {0}", line);
                    }
                }
            }

            Console.WriteLine("CSV-Datei wurde erfolgreich in das Adressbuch importiert.");
        }
    }

    class PLZHelper
    {
        static public void Einlesen()
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
                            Console.WriteLine($"Ungültige Zeile: {line}");
                        }
                    }
                }
            }

            return data;
        }
    }

    class PlzData
    {
        public string Ort { get; set; }
        public string Zusatz { get; set; }
        public string PLZ { get; set; }
        public string Vorwahl { get; set; }
        public string Bundesland { get; set; }
    }

}
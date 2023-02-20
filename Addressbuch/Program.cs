using System;
using System.IO;

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
            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();

            string entry = $"{name},{address},{phone}";

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
                    Console.WriteLine($"Telefonnummer: {fields[2]}");
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

                        entry = $"{newName},{newAddress},{newPhone}";
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
    }
}

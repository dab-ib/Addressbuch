﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbuch
{
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
}

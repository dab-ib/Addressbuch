namespace Addressbuch
{
    class Program
    {
        static void Main(string[] args)
        {
            Menue();
        }
        static void WriteFile()
        {
            StreamWriter schreiben = new StreamWriter(@"test.txt", true);
            schreiben.Write(Console.ReadLine());
            schreiben.Close();
        }
        static void ReadFile()
        {
            StreamReader lesen = new StreamReader("test.txt");
            while (!lesen.EndOfStream)
            {
                Console.WriteLine(lesen.ReadLine());
            }
            lesen.Close();
            Console.ReadLine();
        }
        static void Menue()
        {
            bool Ende = true;
            string[] neueAdresse = new string[7];
            neueAdresse[0] = "Name";
            neueAdresse[1] = "Vorname";
            neueAdresse[2] = "Strasse";
            neueAdresse[3] = "Plz";
            neueAdresse[4] = "Ort";
            neueAdresse[5] = "Telefonnr.";
            neueAdresse[6] = "Geburtstag";
            string eingabe;
            Console.WriteLine("\n\n\t \t      Was möchtest du tun? \n\n \t \t -----------------------------");
            Console.Write("\n \t \t |#############################|");
            Console.Write("\n \t \t |# N - Neue Adresse eingeben #| \n \t");
            Console.Write("\t |# A - Datensätze anzeigen   #| \n \t \t |# B - Beenden               #|");
            Console.Write("\n \t \t |#############################|");
            Console.WriteLine("\n \t \t -----------------------------");

            do
            {
                switch (eingabe = Console.ReadLine().ToUpper())
                {
                    case "N":
                        foreach (string adressen in neueAdresse)
                        {
                            Console.Write("{0}: ", adressen);
                            WriteFile();
                        }
                        Menue();
                        break;

                    case "A":
                        ReadFile();
                        Menue();
                        break;

                    case "B":
                        Ende = true;
                        break;
                    default:
                        Console.WriteLine("Fehlerhafte Eingabe!");
                        Menue();
                        break;
                }
            } while (!Ende);
        }

    }
}
using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Emit;
using Addressbuch;

namespace Addressbuch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu.Hauptmenu();
        }
    }

}
using NUnit.Framework;
using System;
using System.IO;

namespace Addressbuch.Tests
{
    [TestFixture]
    public class SearchEntryAddressTests
    {
        [Test]
        public void Search_WithValidAddress_ShouldPrintMatchingEntries()
        {
            // Arrange
            string expectedEntry = "Max,Mustermann,Musterstraße 123,12345,Musterstadt,0123456789,01.01.2000,max@example.com,Musterfirma,Mustergruppe";
            string searchQuery = "musterstraße";
            using (StreamWriter writer = new StreamWriter("addressbook.txt"))
            {
                writer.WriteLine(expectedEntry);
            }

            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            SearchEntryAdress.Search();

            // Assert
            string consoleOutputString = consoleOutput.ToString();
            Assert.IsTrue(consoleOutputString.Contains(expectedEntry));
        }

        [Test]
        public void Search_WithEmptyAddress_ShouldDisplayErrorMessage()
        {
            // Arrange
            string searchQuery = "";
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            SearchEntryAdress.Search();

            // Assert
            string consoleOutputString = consoleOutput.ToString();
            Assert.IsTrue(consoleOutputString.Contains("Ungültige Eingabe! Bitte geben Sie eine Adresse ein."));
        }

        [Test]
        public void Search_WithNoMatchingEntries_ShouldDisplayNoMatchMessage()
        {
            // Arrange
            string expectedEntry = "Max,Mustermann,Musterstraße 123,12345,Musterstadt,0123456789,01.01.2000,max@example.com,Musterfirma,Mustergruppe";
            string searchQuery = "test";
            using (StreamWriter writer = new StreamWriter("addressbook.txt"))
            {
                writer.WriteLine(expectedEntry);
            }

            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            SearchEntryAdress.Search();

            // Assert
            string consoleOutputString = consoleOutput.ToString();
            Assert.IsTrue(consoleOutputString.Contains("Kein passender Eintrag gefunden."));
        }
    }
}

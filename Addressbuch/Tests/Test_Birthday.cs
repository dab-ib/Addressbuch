using NUnit.Framework;
using System;
using System.IO;

namespace Addressbuch.Tests
{
    [TestFixture]
    public class BirthdayTests
    {
        private string addressBookFilePath;

        [SetUp]
        public void Setup()
        {
            // Pfad zur Testdatei "addressbook.txt" festlegen
            addressBookFilePath = "test_addressbook.txt";

            // Testdaten in die Testdatei schreiben
            using (StreamWriter writer = new StreamWriter(addressBookFilePath))
            {
                writer.WriteLine("Max,Mustermann,-,12345,Musterstadt,-,01.01.1990,-,-,-");
                writer.WriteLine("Anna,Musterfrau,-,54321,Musterort,-,12.05.1985,-,-,-");
            }
        }

        [TearDown]
        public void Cleanup()
        {
            // Testdatei nach dem Test entfernen
            if (File.Exists(addressBookFilePath))
            {
                File.Delete(addressBookFilePath);
            }
        }

        [Test]
        public void BirthdayToday_HasBirthday_PrintsMessage()
        {
            // Arrange
            string expectedOutput = $"Heute hat Max Mustermann Geburtstag und wird {GetAge("01.01.1990")} Jahre alt.";

            // Redirect Console.WriteLine output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Birthday.BirthdayToday();

                // Assert
                Assert.AreEqual(expectedOutput, sw.ToString().Trim());
            }
        }

        [Test]
        public void BirthdayToday_NoBirthday_PrintsNoMessage()
        {
            // Arrange
            string expectedOutput = "Heute hat niemand Geburtstag.";

            // Redirect Console.WriteLine output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Birthday.BirthdayToday();

                // Assert
                Assert.AreEqual(expectedOutput, sw.ToString().Trim());
            }
        }

        private int GetAge(string birthday)
        {
            DateTime birthdate = DateTime.ParseExact(birthday, "dd.MM.yyyy", null);
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            if (birthdate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}

using NUnit.Framework;
using System.IO;

namespace Addressbuch.Tests
{
    [TestFixture]
    public class AllcsvExporterTests
    {
        [Test]
        public void ExportContactsToCsv_ValidFilePath_ContactsExported()
        {
            // Arrange
            string filePath = "test.csv";

            // Act
            AllcsvExporter.ExportContactsToCsv(filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public void ExportContactsToCsv_InvalidFilePath_FileNotFoundError()
        {
            // Arrange
            string filePath = "invalid/path/test.csv";

            // Assert
            Assert.Throws<FileNotFoundException>(() => AllcsvExporter.ExportContactsToCsv(filePath));
        }
    }
}

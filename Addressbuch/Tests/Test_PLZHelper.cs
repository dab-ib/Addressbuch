using NUnit.Framework;
using System.Collections.Generic;

namespace Addressbuch.Tests
{
    [TestFixture]
    public class PLZHelperTests
    {
        [Test]
        public void PLZ_Finder_ValidInput_MatchingResults()
        {
            // Arrange
            string input = "12345";

            // Act
            List<PlzData> results = PLZHelper.ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.PLZ == input)
                .ToList();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [Test]
        public void PLZ_Finder_InvalidInput_NoResults()
        {
            // Arrange
            string input = "00000";

            // Act
            List<PlzData> results = PLZHelper.ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.PLZ == input)
                .ToList();

            // Assert
            Assert.IsTrue(results.Count == 0);
        }

        [Test]
        public void Ort_Finder_ValidInput_MatchingResults()
        {
            // Arrange
            string input = "Berlin";

            // Act
            List<PlzData> results = PLZHelper.ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.Ort == input)
                .ToList();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [Test]
        public void Ort_Finder_InvalidInput_NoResults()
        {
            // Arrange
            string input = "InvalidCity";

            // Act
            List<PlzData> results = PLZHelper.ReadEmbeddedCsvFile("plz_de")
                .Where(x => x.Ort == input)
                .ToList();

            // Assert
            Assert.IsTrue(results.Count == 0);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace csv_analysis.test
{
    [TestClass]
    public class Program
    {
        [TestMethod]
        public void DisplayData()
        {
            // Arrange
            string[] csvData = new string[]
            {
            "Test,File,Score",
            "John,Doe,80",
            "Jane,Smith,70",
            "Adam,Johnson,90"
            };
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                DisplayData(csvData);
                string output = sw.ToString();
                // Assert
                Assert.IsTrue(output.Contains("John"));
                Assert.IsTrue(output.Contains("Doe"));
                Assert.IsTrue(output.Contains("Score: 80"));
                Assert.IsFalse(output.Contains("Jane"));
                Assert.IsFalse(output.Contains("Smith"));
                Assert.IsFalse(output.Contains("Score: 70"));
                Assert.IsTrue(output.Contains("Adam"));
                Assert.IsTrue(output.Contains("Johnson"));
                Assert.IsTrue(output.Contains("Score: 90"));
            }
        }
    }
}
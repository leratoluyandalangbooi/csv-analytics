using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace csv_analytics.Tests
{
    [TestClass()]
    public class ProgramUnitTests
    {
        private string[] csvData = new string[] { "Name,Second,Score", "Adam,Johnson,90" };

        [TestMethod()]
        public void DisplayDataTest()
        {
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Program.DisplayData(csvData);

                string output = writer.ToString();

                Assert.IsNotNull(output);
                Assert.IsTrue(output.Contains("Johnson"));
                Assert.IsTrue(output.Contains("Score: 90"));
            }
        }

        [TestMethod]
        public void TestInsertNewData_InputValidData_WritesToCSVFile()
        {
            string expectedCSVContent = "Adam,Johnson,90";

            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.SetIn(new StringReader("Adam\r\nJohnson\r\n90\r\n"));
                Program.InsertNewData(csvData);

                string output = writer.ToString();

                Assert.IsTrue(output.Contains("Input values written to CSV file."));
                Assert.IsTrue(File.Exists("TestData.csv"));
                string csvContent = File.ReadAllText("TestData.csv");
                Assert.IsTrue(csvContent.Contains(expectedCSVContent));
            }
        }

        [TestMethod]
        public void TestInsertNewData_InputEmptyData_ShouldPromptError()
        {
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.SetIn(new StringReader(""));
                Program.InsertNewData(csvData);

                string output = writer.ToString();
                Assert.IsTrue(output.Contains("Name can not be empty"));
            }
        }

        [TestMethod]
        public void TestInsertNewData_InputInvalidScore_ShouldPromptError()
        {
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.SetIn(new StringReader("Adam\r\nJohnson\r\nInvalidScore\r\n"));
                Program.InsertNewData(csvData);

                string output = writer.ToString();
                Assert.IsTrue(output.Contains("Score must be a number"));
            }
        }
    }
}
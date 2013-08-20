using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Infrastructure;

using FluentAssertions;

using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.IO;

namespace Pellared.SalaryBook.Tests
{
    [TestClass]
    public class CsvSalariesExporterTests
    {
        [TestMethod]
        public void Export_TwoEntities_FirstNameOfFirstEntityWritten()
        {
            // Arrange and Act
            var values = ArrangeAndActForFirstEntityMappingTests(x => x.FirstName);

            // Assert
            string firstName = values.CsvValue;
            firstName.Should().Be(values.ExpectedValue);       
        }

        [TestMethod]
        public void Export_TwoEntities_LastNameOfFirstEntityWritten()
        {
            // Arrange and Act
            var values = ArrangeAndActForFirstEntityMappingTests(x => x.LastName);

            // Assert
            string lastName = values.CsvValue;
            lastName.Should().Be(values.ExpectedValue);
        }

        [TestMethod]
        public void Export_TwoEntities_BirthDateOfFirstEntityWritten()
        {
            // Arrange and Act
            var values = ArrangeAndActForFirstEntityMappingTests(x => x.BirthDate);

            // Assert
            DateTime parsedBirthDate = DateTime.Parse(values.CsvValue);
            parsedBirthDate.Should().Be(values.ExpectedValue);
        }

        [TestMethod]
        public void Export_TwoEntities_SalaryValueOfFirstEntityWritten()
        {
            // Arrange and Act
            var values = ArrangeAndActForFirstEntityMappingTests(x => x.SalaryValue);

            // Assert
            double parsedSalaryValue = double.Parse(values.CsvValue);
            parsedSalaryValue.Should().Be(values.ExpectedValue);
        }

        /// <summary>
        /// Gets the value (string) for the first entity of the coresponding column.
        /// </summary>
        private EntityMappingResult<T> ArrangeAndActForFirstEntityMappingTests<T>(Expression<Func<Salary, T>> columnSelector)
        {
            // Arrange
            var salaries = CreateTwoSalaries();
            var csvStringtWriter = new StringWriter();
            CsvSalariesExporter cut = new CsvSalariesExporter();

            // Act
            cut.Export(salaries, csvStringtWriter);

            // Pre-Assert
            string csvContent = csvStringtWriter.ToString();
            string csvValue = GetColumnValueOfFirstEntity(csvContent, columnSelector);

            T expectedValue = columnSelector.Compile()(salaries.First());

            return new EntityMappingResult<T>(expectedValue, csvValue);
        }

        private static Salary[] CreateTwoSalaries()
        {
            var salaries
                    = new[]
                          {
                                  new Salary
                                      {
                                              BirthDate = new DateTime(1990, 11, 1),
                                              FirstName = "Jan",
                                              LastName = "Kowalski",
                                              SalaryValue = 1000.5,
                                      },
                                  new Salary
                                      {
                                              BirthDate = new DateTime(1990, 11, 21),
                                              FirstName = "Adam",
                                              LastName = "Nowak",
                                              SalaryValue = 2000,
                                      }
                          };
            return salaries;
        }

        private string GetColumnValueOfFirstEntity<T>(string csvContent, Expression<Func<Salary, T>> columnSelector)
        {
            string[] columnNames;
            string[] firstEntityValues;
            GetColumnNamesAndFirstEntityValues(csvContent, out columnNames, out firstEntityValues);

            int columnIndex = GetColumnIndex(columnSelector, columnNames);

            string result = firstEntityValues[columnIndex];
            return result;
        }

        private static void GetColumnNamesAndFirstEntityValues(string csvContent, out string[] columnNames, out string[] firstEntityValues)
        {
            const char separator = ',';
            using (var csvReader = new StringReader(csvContent))
            {
                // ReSharper disable PossibleNullReferenceException
                columnNames = csvReader.ReadLine().Split(separator);
                firstEntityValues = csvReader.ReadLine().Split(separator);
                // ReSharper restore PossibleNullReferenceException
            }
        }

        private static int GetColumnIndex<T>(Expression<Func<Salary, T>> columnSelector, string[] columnNames)
        {
            string columnName = ReflectionUtils.ExtractPropertyName(columnSelector);
            int columnIndex = Array.FindIndex(columnNames, x => x == columnName);
            if (columnIndex == -1)
                Assert.Fail("column not found");
           
            return columnIndex;
        }

        #region Private classes
        
        private class EntityMappingResult<T>
        {
            internal EntityMappingResult(T expectedValue, string csvValue)
            {
                ExpectedValue = expectedValue;
                CsvValue = csvValue;
            }

            internal string CsvValue { get; private set; }
            internal T ExpectedValue { get; private set; }
        }

        #endregion
    }
}

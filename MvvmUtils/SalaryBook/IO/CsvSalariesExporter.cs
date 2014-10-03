using CsvHelper;
using CsvHelper.Configuration;
using Pellared.SalaryBook.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Pellared.SalaryBook.IO
{
    public class CsvSalariesExporter : ISalariesExporter
    {
        public void Export(IEnumerable<Salary> salaries, TextWriter writer)
        {
            var configuration = new CsvConfiguration() { CultureInfo = CultureInfo.InvariantCulture };
            using (var csv = new CsvWriter(writer, configuration))
            {
                csv.WriteRecords(salaries);
            }
        }
    }
}
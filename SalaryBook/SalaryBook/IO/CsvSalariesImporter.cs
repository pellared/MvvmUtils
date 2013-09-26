using CsvHelper;
using CsvHelper.Configuration;
using Pellared.SalaryBook.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Pellared.SalaryBook.IO
{
    public class CsvSalariesImporter : ISalariesImporter
    {
        public IEnumerable<Salary> Import(TextReader reader)
        {
            var configuration = new CsvConfiguration() { CultureInfo = CultureInfo.InvariantCulture };
            using (var csv = new CsvReader(reader, configuration))
            {
                var result = csv.GetRecords<Salary>();
                return result;
            }
        }
    }
}
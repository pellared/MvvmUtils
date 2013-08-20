using System.IO;

using CsvHelper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.IO
{
    public class CsvSalariesImporter : ISalariesImporter
    {
        public IEnumerable<Salary> Import(TextReader reader)
        {
            using (var csv = new CsvReader(reader))
            {
                var result = csv.GetRecords<Salary>().ToArray();
                return result;
            }
        }
    }
}
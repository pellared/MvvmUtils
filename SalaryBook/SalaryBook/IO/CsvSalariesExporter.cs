using System.IO;

using CsvHelper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.IO
{
    public class CsvSalariesExporter : ISalariesExporter
    {
        public void Export(IEnumerable<Salary> salaries, TextWriter writer)
        {
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(salaries);
            }
        }
    }
}
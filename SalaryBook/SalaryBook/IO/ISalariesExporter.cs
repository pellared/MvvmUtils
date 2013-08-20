using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.IO
{
    public interface ISalariesExporter
    {
        void Export(IEnumerable<Salary> salaries, TextWriter writer);
    }
}
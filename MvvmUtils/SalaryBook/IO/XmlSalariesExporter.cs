using System.IO;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.IO
{
    public class XmlSalariesExporter : ISalariesExporter
    {
        public void Export(IEnumerable<Salary> salaries, TextWriter writer)
        {
            var serializer = new XmlSerializer(typeof(List<Salary>));
            serializer.Serialize(writer, salaries.ToList());
        }
    }
}
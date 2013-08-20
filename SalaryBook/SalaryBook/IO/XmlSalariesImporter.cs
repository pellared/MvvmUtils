using System.IO;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.IO
{
    public class XmlSalariesImporter : ISalariesImporter
    {
        public IEnumerable<Salary> Import(TextReader reader)
        {
            var serializer = new XmlSerializer(typeof(List<Salary>));
            return serializer.Deserialize(reader) as IEnumerable<Salary>;
        }
    }
}
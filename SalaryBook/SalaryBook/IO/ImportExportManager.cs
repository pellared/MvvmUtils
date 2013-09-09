using Pellared.SalaryBook.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pellared.SalaryBook.IO
{
    public class ImportExportManager
    {
        private readonly ISalariesImporter csvSalariesImporter;
        private readonly ISalariesExporter csvSalariesExporter;
        private readonly ISalariesImporter xmlSalariesImporter;
        private readonly ISalariesExporter xmlSalariesExporter;

        public ImportExportManager(
                ISalariesImporter csvSalariesImporter,
                ISalariesExporter csvSalariesExporter,
                ISalariesImporter xmlSalariesImporter,
                ISalariesExporter xmlSalariesExporter)
        {
            this.csvSalariesImporter = csvSalariesImporter;
            this.csvSalariesExporter = csvSalariesExporter;
            this.xmlSalariesImporter = xmlSalariesImporter;
            this.xmlSalariesExporter = xmlSalariesExporter;
        }

        public IEnumerable<Salary> Import(TextReader reader, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Csv:
                    return csvSalariesImporter.Import(reader);
                case FileType.Xml:
                    return xmlSalariesImporter.Import(reader);
                default:
                    throw new ArgumentOutOfRangeException("fileType");
            }
        }

        public void Export(IEnumerable<Salary> salaries, TextWriter writer, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Csv:
                    csvSalariesExporter.Export(salaries, writer);
                    break;
                case FileType.Xml:
                    xmlSalariesExporter.Export(salaries, writer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fileType");
            }
        }


    }
}
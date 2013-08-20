using System;
using System.Collections.Generic;
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

        public ISalariesImporter CsvSalariesImporter
        {
            get { return csvSalariesImporter; }
        }

        public ISalariesExporter CsvSalariesExporter
        {
            get { return csvSalariesExporter; }
        }

        public ISalariesImporter XmlSalariesImporter
        {
            get { return xmlSalariesImporter; }
        }

        public ISalariesExporter XmlSalariesExporter
        {
            get { return xmlSalariesExporter; }
        }
    }
}
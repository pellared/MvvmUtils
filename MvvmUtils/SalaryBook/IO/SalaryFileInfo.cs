using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.SalaryBook.IO
{
    public enum FileType
    {
        Csv,
        Xml
    }

    public class SalaryFileInfo
    {
        public const string CsvDescription = "csv";
        public const string CsvExtension = "txt";
        public const string XmlDescription = "xml";
        public const string XmlExtension = "xml";

        public SalaryFileInfo(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Csv:
                    Description = CsvDescription;
                    Extension = CsvExtension;
                    FileType = FileType.Csv;
                    break;
                case FileType.Xml:
                    Description = XmlDescription;
                    Extension = XmlExtension;
                    FileType = FileType.Xml;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fileType");
            }
        }

        public SalaryFileInfo(string extension)
        {
            switch (extension.ToLowerInvariant())
            {
                case CsvExtension:
                    Description = CsvDescription;
                    Extension = CsvExtension;
                    FileType = FileType.Csv;
                    break;
                case XmlExtension:
                    Description = XmlDescription;
                    Extension = XmlExtension;
                    FileType = FileType.Xml;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fileType");
            }
        }

        public FileType FileType { get; private set; }

        public string Description { get; private set; }

        public string Extension { get; private set; }
    }
}

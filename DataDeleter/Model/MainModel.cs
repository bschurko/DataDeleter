using System;
using System.Collections.Generic;
using System.Text;

namespace DataDeleter.Model
{
    public class MainModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<string> Extensions { get; set; } = new List<string>();
        public string SelectedExtension { get; set; } = string.Empty;
        public string ExtensionValue { get; set; } = string.Empty;
        public string DirectoryPath { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataDeleter.Model
{
    public class ScannedFile
    {
        public bool IsSelected { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

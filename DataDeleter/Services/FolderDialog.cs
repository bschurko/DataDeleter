using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDeleter.Services
{
    public interface IFolderDialog
    {
        public string? SelectFolder();
    }
    public class FolderDialog : IFolderDialog
    {
        public string? SelectFolder()
        {
            OpenFolderDialog dialog = new OpenFolderDialog
            {
                Title = "Select Folder",
            };

            bool? result = dialog.ShowDialog();
            return result == true ? dialog.FolderName : null;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataDeleter.Model;
using DataDeleter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace DataDeleter.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        private MainModel _mainModel = new();

        [ObservableProperty]
        public string _directoryPath = string.Empty;

        [ObservableProperty]
        public DateTime? _fromDate;

        [ObservableProperty]
        public DateTime? _toDate;

        [ObservableProperty]
        public string _extensionValue = string.Empty;

        public ObservableCollection<ScannedFile> ScannedFiles { get; set; } = new();

        [ObservableProperty]
        public ScannedFile? _selectedScannedFile;

        public ObservableCollection<string> Extensions { get; set; } = new();

        [ObservableProperty]
        public string _selectedExtension = string.Empty;

        public IFolderDialog FolderDialog { get; }
        public IFileService FileService { get; }

        public MainViewModel(IFolderDialog folderDialog,
            IFileService fileService)
        {
            FolderDialog = folderDialog;
            FileService = fileService;
        }

        [RelayCommand]
        public void AddExtension()
        {
            if (string.IsNullOrWhiteSpace(_extensionValue))
                return;

            Extensions.Add(_extensionValue);
        }

        [RelayCommand]
        public void SelectDirectoryPath()
        {

            string? path = FolderDialog.SelectFolder();
            if (!string.IsNullOrEmpty(path))
            {
                DirectoryPath = path;
            }
        }

        [RelayCommand]
        public void ViewImage(string filePath)
        {
            string? selectedFilePath = SelectedScannedFile?.FilePath;
            if (string.IsNullOrEmpty(filePath))
                return;

            // Open dialog window safely on UI thread
            var viewerWindow = new DataDeleter.Views.PopupImage(filePath)
            {
                Owner = System.Windows.Application.Current.MainWindow
            };

            viewerWindow.ShowDialog();
        }


        [RelayCommand]
        public void ScanFiles()
        {
            var files = FileService.GetFiles(DirectoryPath,
                Extensions.ToList(),
                _fromDate, _toDate,
                SearchOption.AllDirectories);

            ScannedFiles.Clear();

            foreach (var f in files)
            {
                ScannedFiles.Add(f);
            }
        }

        [RelayCommand]
        public void DeleteFiles()
        {
            var deletedFiles = FileService.DeleteFiles(ScannedFiles.Where(f => f.IsSelected).ToList());

            foreach (var f in ScannedFiles.Where(f => deletedFiles.Contains(f)).ToList())
            {
                ScannedFiles.Remove(f);
            }
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataDeleter.Model;
using DataDeleter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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

        public MainViewModel(IFolderDialog folderDialog)
        {
            FolderDialog = folderDialog;
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
        public void ScanFiles()
        {

        }

        [RelayCommand]
        public void DeleteFiles()
        {

        }
    }
}

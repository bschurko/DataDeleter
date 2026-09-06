using SecureDelete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataDeleter.Services
{
    public interface IFileService
    {
        List<string> GetFiles(string rootDirectory, List<string> extensions,
            DateTime? fromDate, DateTime? toDate, SearchOption searchOption = SearchOption.AllDirectories);
        void DeleteFiles(List<string> filePaths);
    }
    public class FileService : IFileService
    {
        public void DeleteFiles(List<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                try
                {
                    if (!File.Exists(path))
                        continue;

                    // Preferred: detects HDD vs SSD
                    Delete.DeleteFile(path);

                    // Alternative if you always want to force overwrite:
                    // Delete.DeleteFileWithoutDriveDetection(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to shred {path}: {ex.Message}");
                }
            }
        }

        public List<string> GetFiles(string rootDirectory, List<string> extensions,
            DateTime? fromDate, DateTime? toDate, SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory))
                throw new ArgumentException("Root directory is required.", nameof(rootDirectory));

            if (!Directory.Exists(rootDirectory))
                throw new DirectoryNotFoundException($"Directory not found: {rootDirectory}");

            // Normalize extensions: ensure they start with '.' and are lower-case
            var allowedExtensions = new HashSet<string>(
                extensions
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .Select(e => e.StartsWith('.') ? e.ToLowerInvariant() : "." + e.ToLowerInvariant()),
                StringComparer.OrdinalIgnoreCase);

            //  if (allowedExtensions.Count == 0)
            //    return new List<string>();

            // Ensure fromDate <= toDate
            if (fromDate > toDate)
                (fromDate, toDate) = (toDate, fromDate);

            return Directory.EnumerateFiles(rootDirectory, "*.*", searchOption)
                .Where(path =>
                {
                    // Extension filter
                    if (allowedExtensions.Count > 0)
                    {
                        var ext = Path.GetExtension(path);
                        if (!allowedExtensions.Contains(ext))
                            return false;
                    }

                    if (fromDate != null || toDate != null)
                    {
                        // Date filter (LastWriteTime – change to CreationTime if preferred)
                        DateTime timestamp;
                        try
                        {
                            timestamp = File.GetLastWriteTime(path);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            return false; // skip files we cannot access
                        }
                        catch (IOException)
                        {
                            return false;
                        }

                        if (fromDate != null && toDate != null)
                        {
                            return timestamp >= fromDate && timestamp <= toDate;
                        }
                        else if (fromDate != null)
                        {
                            return timestamp >= fromDate;
                        }
                        else if (toDate != null)
                        {
                            return timestamp <= toDate;
                        }
                    }

                    return true;
                })
                .ToList();

        }
    }
}

using DataDeleter;

namespace DataDeleter.Test
{
    [TestClass]
    public sealed class DataPickerTests
    {

        private void CreateSampleFiles(string directoryPath, int numberOfFiles)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            for (int i = 0; i < numberOfFiles; i++)
            {
                string filePath = Path.Combine(directoryPath, $"SampleFile_{i + 1}.txt");
                File.WriteAllText(filePath, $"This is sample file {i + 1}");
            }
        }

        [TestMethod]
        public void MainViewModel_GetFiles_DeleteFiles_Test()
        {
            Services.FileService service = new();
            var exts = new List<string>()
             {
                 "jpg",
                 "gif",
                 "png",
                 "jpeg",
                 "tiff",
                 "txt"
             };

            DateTime startDate = DateTime.Now.AddDays(-10);
            DateTime endDate = DateTime.Now.AddDays(5);

            CreateSampleFiles("C:\\Users\\brett\\test", 5);

            var files = service.GetFiles(@"C:\Users\brett\test", exts, startDate, endDate);

            Assert.IsNotNull(files);

            service.DeleteFiles(files);

            var newFiles = service.GetFiles(@"C:\Users\brett\test", exts, startDate, endDate);
            Assert.IsEmpty(newFiles);

        }
    }
}

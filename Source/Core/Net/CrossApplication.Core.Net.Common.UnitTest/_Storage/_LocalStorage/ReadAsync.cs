using System;
using System.IO;
using System.Xml.Serialization;
using CrossApplication.Core.Net.Common.Storage;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Net.Common.UnitTest._Storage._LocalStorage
{
    public class ReadAsync
    {
        [Fact]
        public async void Usage()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CrossApplication");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, "MyKey.cas");
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var serializer = new XmlSerializer(typeof(string));
            Stream stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, "test");
            stream.Close();
            var storage = new LocalStorage();

            var myObject = await storage.ReadAsync<string>("MyKey");

            myObject.Should().Be("test");
        }
    }
}
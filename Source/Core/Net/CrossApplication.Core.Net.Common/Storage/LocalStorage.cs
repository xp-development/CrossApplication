﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CrossApplication.Core.Contracts.Common.Storage;

namespace CrossApplication.Core.Net.Common.Storage
{
    public class LocalStorage : IStorage
    {
        static LocalStorage()
        {
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CrossApplication");
        }

        public Task SaveAsync<T>(string key, T value)
            where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            var path = Path.Combine(FolderPath, $"{key}.cas");
            if(File.Exists(path))
                File.Delete(path);

            Stream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, value);
            stream.Close();
            return Task.FromResult(false);
        }

        public Task<T> ReadAsync<T>(string key)
            where T : class
        {
            var filePath = Path.Combine(FolderPath, $"{key}.cas");
            if (!File.Exists(filePath))
                return Task.FromResult<T>(null);

            var serializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var deserializedObject = (T)serializer.Deserialize(stream);
            stream.Close();
            return Task.FromResult(deserializedObject);
        }

        private static readonly string FolderPath;
    }
}
using MealRandomizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xamarin.Forms;

[assembly: Dependency(typeof(MealRandomizer.Droid.SerializationProvider))]
namespace MealRandomizer.Droid
{
    public class SerializationProvider : ISerializationProvider
    {
        public List<T> Deserialize<T>(string fileName = null)
        {
            string path = CreatePath<T>(fileName);
            if (!File.Exists(path))
            {
                throw new Exception($"File {path} does not exist.");
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                return (List<T>)formatter.Deserialize(stream);
            }
        }

        public void Serialize<T>(T serializableObject, string fileName = null)
        {
            string path = CreatePath<T>(fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, serializableObject);
            }
        }

        private static string CreatePath<T>(string fileName)
        {
            return Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath, $"{fileName ?? typeof(T).ToString()}.bin");
        }
    }
}
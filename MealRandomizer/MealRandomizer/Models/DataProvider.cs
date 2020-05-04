using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MealRandomizer.Models
{
    public static class DataProvider
    {
        public static List<T> Deserialize<T>(string path)
        {
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

        public static void Serialize<T>(List<T> list, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, list);
            }
        }
    }
}

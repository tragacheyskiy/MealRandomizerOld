using System;
using System.Collections.Generic;
using System.Text;

namespace MealRandomizer.Models
{
    public interface ISerializationProvider
    {
        List<T> Deserialize<T>(string fileName = null);
        void Serialize<T>(T serializableObject, string fileName = null);
    }
}

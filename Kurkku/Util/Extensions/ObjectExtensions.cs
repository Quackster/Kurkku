using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kurkku.Util.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}

using ConsultoriaSAS.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsultoriaSAS.Common.Helpers
{
    public static class SerializerHelper
    {
        public static string Serialize<T>(T data, SerializeFormat format)
        {
            switch (format)
            {
                case SerializeFormat.XML:
                    return SerializeToXml(data);
                case SerializeFormat.JSON:
                    return SerializeToJson(data);
                default:
                    return String.Empty;
            }
        }

        public static T Deserialize<T>(string data, SerializeFormat format)
        {
            switch (format)
            {
                case SerializeFormat.XML:
                    return DeserializeToXml<T>(data);
                case SerializeFormat.JSON:
                    return DeserializeFromJson<T>(data);
                default:
                    return default(T);
            }
        } 
        private static string SerializeToXml<T>(T data)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, data);
                return stringWriter.ToString();
            }
        } 

        private static T DeserializeToXml<T>(string data)
        {
            var xmlSerializer = new XmlSerializer(data.GetType());
            var stream = GenerateStreamFromString(data);
            var result = xmlSerializer.Deserialize(stream);
            return (T)result;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static string SerializeToJson<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        private static T DeserializeFromJson<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }

    
}

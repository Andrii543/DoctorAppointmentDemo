using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Configuration
{
    public class AppSettings
    {
        public DatabaseConfig Database { get; set; }

        public static string FilePath => "C:\\Users\\andri\\Desktop\\DoctorAppointmentDemo\\DoctorAppointmentDemo.Data\\Configuration\\appsettings.json";

        public static AppSettings Load()
        {
            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<AppSettings>(json)!;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void SaveXml<T>(T obj, string Path) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The object cannot be null");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(Path))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}

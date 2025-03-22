using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Configuration
{
    public class AppSettings
    {
        public DatabaseConfig Database { get; set; }

        public static string FilePath => "appsettings.json";

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
    }
}

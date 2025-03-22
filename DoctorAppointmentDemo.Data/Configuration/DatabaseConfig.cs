using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Configuration
{
    public class DatabaseConfig
    {
        public EntityConfig Doctors { get; set; }
        public EntityConfig Patients { get; set; }
        public EntityConfig Appointments { get; set; }
    }
}

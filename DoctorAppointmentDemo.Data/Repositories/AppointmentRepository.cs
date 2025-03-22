using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public AppointmentRepository()
        {
            var settings = AppSettings.Load();
            Path = settings.Database.Appointments.Path;
            LastId = settings.Database.Appointments.LastId;
        }

        public override void ShowInfo(Appointment appointment)
        {
            Console.WriteLine($"Appointment {appointment.Id} on {appointment.DateTimeFrom}");
        }

        protected override void SaveLastId()
        {
            var settings = AppSettings.Load();
            settings.Database.Appointments.LastId = LastId;
            settings.Save();
        }
    }
}

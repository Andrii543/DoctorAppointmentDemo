using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public AppSettings? appSettings { get; set; }
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public PatientRepository()
        {
            var settings = AppSettings.Load();

            Path = settings.Database.Patients.Path;
            LastId = settings.Database.Patients.LastId;
        }

        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine($"{patient.Id}: {patient.Name} {patient.Surname}, Illness: {patient.IllnessType}");
        }

        protected override void SaveLastId()
        {
            var settings = AppSettings.Load();

            settings.Database.Patients.LastId = LastId;
            settings.Save();
        }
    }
}

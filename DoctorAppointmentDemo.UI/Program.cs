using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using DoctorAppointmentDemo.UI.MenuOptions;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            var doctorService = new DoctorService();
            var patientService = new PatientService();
            var appointmentService = new AppointmentService();

            while (true)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Show Doctors");
                Console.WriteLine("2. Add Doctor");
                Console.WriteLine("3. Show Patients");
                Console.WriteLine("4. Add Patient");
                Console.WriteLine("5. Show Appointments");
                Console.WriteLine("6. Add Appointment");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");

                if (!int.TryParse(Console.ReadLine(), out int input) || !Enum.IsDefined(typeof(MenuOptions), input))
                {
                    Console.WriteLine("Invalid option.");
                    continue;
                }

                var choice = (MenuOptions)input;

                switch (choice)
                {
                    case MenuOptions.ShowDoctors:
                        var doctors = doctorService.GetAll();
                        foreach (var doc in doctors)
                        {
                            Console.WriteLine($"{doc.Id}: {doc.Name} {doc.Surname}, Experience: {doc.Experience}, Type: {doc.DoctorType}");
                        }
                        break;

                    case MenuOptions.AddDoctor:
                        var newDoctor = new Doctor
                        {
                            Name = "Ivan",
                            Surname = "Ivanov",
                            Experience = 5,
                            DoctorType = Domain.Enums.DoctorTypes.Dentist,
                            Salary = 5000
                        };
                        doctorService.Create(newDoctor);
                        Console.WriteLine("Doctor added.");
                        break;

                    case MenuOptions.ShowPatients:
                        var patients = patientService.GetAll();
                        foreach (var p in patients)
                        {
                            Console.WriteLine($"{p.Id}: {p.Name} {p.Surname}, Illness: {p.IllnessType}");
                        }
                        break;

                    case MenuOptions.AddPatient:
                        var newPatient = new Patient
                        {
                            Name = "Petro",
                            Surname = "Petrenko",
                            IllnessType = Domain.Enums.IllnessTypes.DentalDisease,
                            Address = "Kyiv, Main St. 1"
                        };
                        patientService.Create(newPatient);
                        Console.WriteLine("Patient added.");
                        break;

                    case MenuOptions.ShowAppointments:
                        var appointments = appointmentService.GetAll();
                        foreach (var a in appointments)
                        {
                            Console.WriteLine($"Appointment {a.Id}: Doctor {a.Doctor?.Name} {a.Doctor?.Surname}, Patient {a.Patient?.Name} {a.Patient?.Surname}, From {a.DateTimeFrom} To {a.DateTimeTo}");
                        }
                        break;

                    case MenuOptions.AddAppointment:
                        Console.Write("Enter Patient ID: ");
                        int patientId = int.Parse(Console.ReadLine());
                        var patient = patientService.Get(patientId);
                        if (patient == null)
                        {
                            Console.WriteLine("Patient not found.");
                            break;
                        }

                        Console.Write("Enter Doctor ID: ");
                        int doctorId = int.Parse(Console.ReadLine());
                        var doctor = doctorService.Get(doctorId);
                        if (doctor == null)
                        {
                            Console.WriteLine("Doctor not found.");
                            break;
                        }

                        var appointment = new Appointment
                        {
                            Patient = patient,
                            Doctor = doctor,
                            DateTimeFrom = DateTime.Now.AddDays(1),
                            DateTimeTo = DateTime.Now.AddDays(1).AddHours(1),
                            Description = "General Check-up"
                        };
                        appointmentService.Create(appointment);
                        Console.WriteLine("Appointment added.");
                        break;

                    case MenuOptions.Exit:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Unknown option.");
                        break;
                }
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}
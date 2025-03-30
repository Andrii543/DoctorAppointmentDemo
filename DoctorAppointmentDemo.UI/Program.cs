using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using DoctorAppointmentDemo.UI.MenuOptions;
using System.Xml.Serialization;
using System.Xml.Linq;
using MyDoctorAppointment.Data.Configuration;

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

            const string linkXmlSavePatient = "CUsers\\andri\\Desktop\\DoctorAppointmentDemo\\DoctorAppointmentDemo.Data\\MockedDatabase\\patient.xaml";
            const string linkXmlSaveDoctor = "C:\\Users\\andri\\Desktop\\DoctorAppointmentDemo\\DoctorAppointmentDemo.Data\\MockedDatabase\\doctors.xaml";
            const string linkXmlSavaAppointemt = "C:\\Users\\andri\\Desktop\\DoctorAppointmentDemo\\DoctorAppointmentDemo.Data\\MockedDatabase\\appointments.xaml";


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

                        Console.WriteLine("Do you want to save the data? Yes/No");
                        string? doctorSaveData = Console.ReadLine();

                        if (doctorSaveData == "Yes")
                        {

                            var saveDataXml = new AppSettings();

                            saveDataXml.SaveXml(newDoctor, linkXmlSaveDoctor);

                            doctorService.Create(newDoctor);
                        }
                        else
                        {
                            Console.WriteLine("Data not save");
                        }
                        Console.WriteLine("Doctor added and saved");
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
                            Name = "Andrii",
                            Surname = "Rusyn",
                            IllnessType = Domain.Enums.IllnessTypes.DentalDisease,
                            Phone = "14114124214",
                            Email = "Vasya@gmail.com",
                            Address = "Lviv, Olesnytskogo 1"
                        };
                        Console.WriteLine("Do you want to save the data? Yes/No");
                        string? patientSaveData = Console.ReadLine();

                        // Додавання пацієнта до json i xml
                        if(patientSaveData == "Yes")
                        {

                            var saveDataXml = new AppSettings();

                            saveDataXml.SaveXml(newPatient, linkXmlSavePatient);

                            patientService.Create(newPatient);

                            XDocument xdoc = XDocument.Load(linkXmlSavePatient);
                            //XElement? root = xdoc.Element("patients");

                            //if (root != null)
                            //{
                            //    root.Add(new XElement("patient",
                            //         new XAttribute("name", newPatient.Name),
                            //            new XElement("surname", newPatient.Surname),
                            //            new XElement("Phone", newPatient.Phone),
                            //            new XElement("Email", newPatient.Email),
                            //            new XElement("Address", newPatient.Address),
                            //            new XElement("IllnessType", newPatient.IllnessType)));
                            //}
                           //xdoc.Save(linkXmlSavePatient);
                            Console.WriteLine("Patient added and saved.");
                        }
                        else
                        {
                            Console.WriteLine("Data not save");
                        }
                        break ;

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
                        Console.WriteLine("Do you want to save the data? Yes/No");
                        string? appointmentSaveData = Console.ReadLine();

                        if(appointmentSaveData == "Yes")
                        {
                            appointmentService.Create(appointment);

                            var saveDataXml = new AppSettings();

                            saveDataXml.SaveXml(appointment, linkXmlSavaAppointemt);


                        }
                        else
                        {
                            Console.WriteLine("Data not save");
                        }

                        Console.WriteLine("Appointment added and saved");
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


    public class XMLTest
    {

        public void Test()
        {
            var patient = new Patient()
            {
                Name = "Andrii",
                Surname = "Rusyn",
                Phone = "5769210",
                Email = "Andrii@gmail.com",
                Address = "Lviv"       
            };

            XDocument xDoc = new XDocument(new XElement("patients",
                new XElement("patient",
                    new XAttribute("name", patient.Name),
                        new XElement("surname", patient.Surname),
                        new XElement("Phone", patient.Phone),
                        new XElement("Email", patient.Email),
                        new XElement("Address", patient.Address))));
            xDoc.Save("C:\\Users\\andri\\Desktop\\DoctorAppointmentDemo\\DoctorAppointmentDemo.Data\\MockedDatabase\\patient.xaml");
            Console.WriteLine("Saved!!!");
        }
    }

    public static class Program
    {
        public static void Main()
        {
            //var xmltest = new XMLTest();
            //xmltest.Test();

            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();

        }
    }
}
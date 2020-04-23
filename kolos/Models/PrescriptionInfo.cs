using System;

namespace kolos.Models
{
    public class PrescriptionInfo
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string DoctorLastName { get; set; }
        public string PatientLastName { get; set; }
    }
}
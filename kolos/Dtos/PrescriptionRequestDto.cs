using System;
using System.Collections.Generic;
using kolos.Models;

namespace kolos.Dtos
{
    public class PrescriptionRequestDto
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<Medicament> Medicaments { get; set; }

        public int IdDoctor { get; set; }
        public int IdPationt { get; set; }

    }
}
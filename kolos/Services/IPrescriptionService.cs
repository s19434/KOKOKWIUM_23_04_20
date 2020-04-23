using System.Collections.Generic;
using kolos.Dtos;
using kolos.Models;
using Microsoft.AspNetCore.Mvc;

namespace kolos.Services
{
    public interface IPrescriptionService
    {
        List<PrescriptionInfo> GetAll(string sortBy);

       bool AddPrescription(PrescriptionRequestDto prescription);
    }
}
using kolos.Dtos;
using kolos.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class PrescriptionController : ControllerBase
    {
        private IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService animalService)
        {
            _prescriptionService = animalService;
        }


        [HttpGet]
        public IActionResult GetAll(
            [FromQuery(Name = "sortby")] string sortBy = "Date"
            
        )
        {
            var prescriptions = _prescriptionService.GetAll(sortBy);
            return Ok(prescriptions);
        }

        [HttpPost]
        public IActionResult Add(PrescriptionRequestDto prescription)
        {
            if (_prescriptionService.AddPrescription(prescription))
            {
                return Ok("Dodano!");
            }

            return BadRequest();
        }
        
    }
}
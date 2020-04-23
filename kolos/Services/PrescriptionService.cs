using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using kolos.Dtos;
using kolos.Models;
using Microsoft.AspNetCore.Mvc;

namespace kolos.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private string conString =
            "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s19434;Integrated Security=True";

        public List<PrescriptionInfo> GetAll(string sortBy)
        {
            var prescriptionInfo = new List<PrescriptionInfo>();

            using (var connection = new SqlConnection(conString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    "select Name, Date, Date, DueDate, D.LastName as DoctorLastName, P.LastName as PatientLastName" +
                    "from Prescription left join Doctor D on Prescription.IdPrescription = D.IdDoctor " +
                    "Prescription left join Patient P on Prescription.IdPrescription = P.IdPatient " +
                    $"order by {sortBy}";
                connection.Open();
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var info = new PrescriptionInfo
                    {
                        Name = dr["Name"].ToString(),
                        Date = DateTime.Parse(dr["Date"].ToString()),
                        DueDate = DateTime.Parse(dr["DueDate"].ToString()),
                        DoctorLastName = dr["DoctorLastName"].ToString(),
                        PatientLastName = dr["PatientLastName"].ToString()
                    };
                    prescriptionInfo.Add(info);
                }
            }

            return prescriptionInfo;
        }
        
        public bool AddPrescription(PrescriptionRequestDto prescription)
        {
            using (var connection = new SqlConnection(conString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Connection = connection;
                command.CommandText =
                    "insert into Prescription(Date, DueDate, IdDoctor,IdPationt) values (@date,@dueDate @doctor,@patient); SELECT SCOPE_IDENTITY()";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("date", prescription.Date);
                command.Parameters.AddWithValue("dueDate", prescription.DueDate);
                command.Parameters.AddWithValue("doctor", prescription.IdDoctor);
                command.Parameters.AddWithValue("patient", prescription.IdPationt);

                int id = Convert.ToInt32(command.ExecuteScalar());

                Console.WriteLine(id);

                if (prescription.Medicaments == null) return true;


                foreach (var medicament in prescription.Medicaments)
                {
 
                    command.CommandText =
                        $"insert into Prescription_Medicament(idMedicament, idPrescription, dose, details) values({medicament.Id}, {medicament.Name}, {medicament.Description},{medicament.Type})";
                    command.Parameters["name"].Value = medicament.Name;
                    command.Parameters["description"].Value = medicament.Description;
                    command.Parameters["type"].Value = medicament.Type;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            return true;
        }
        
    }
}
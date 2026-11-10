using Data_Hospital_Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Manager.Core.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);  //Delete GET
        Task<Patient?> GetPatientWithDoctorsAsync(int id);  //Details,Edit GET, Edit POST
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient, List<int> doctorIds);
        Task DeletePatientAsync(int id);
    }
}

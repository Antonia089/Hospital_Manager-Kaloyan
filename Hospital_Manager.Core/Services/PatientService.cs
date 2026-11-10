using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Manager.Core.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext context_new;
        public PatientService(HospitalDbContext context)
        {
            context_new = context;
        }
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await context_new.Patients.Include(p => p.DoctorPatients).ThenInclude(dp => dp.Doctor).ToListAsync();
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await context_new.Patients.FindAsync(id);
        }
        public async Task<Patient?> GetPatientWithDoctorsAsync(int id)
        {
            return await context_new.Patients.Include(p => p.DoctorPatients).ThenInclude(dp => dp.Doctor).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddPatientAsync(Patient patient)
        {
            context_new.Patients.Add(patient);
            await context_new.SaveChangesAsync();
        }
        public async Task UpdatePatientAsync(Patient patient, List<int> doctorIds)
        {
            patient.DoctorPatients.Clear();
            foreach(var doctorId in doctorIds.Distinct())
            {
                patient.DoctorPatients.Add(new DoctorPatient
                {
                    DoctorId = doctorId,
                    PatientId = patient.Id
                });
            }
            await context_new.SaveChangesAsync();
        }
        public async Task DeletePatientAsync(int id)
        {
            var patient = await context_new.Patients.Include(p => p.DoctorPatients).FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                context_new.DoctorPatients.RemoveRange(patient.DoctorPatients);
                context_new.Patients.Remove(patient);
                await context_new.SaveChangesAsync();
            }
        }
    }
}

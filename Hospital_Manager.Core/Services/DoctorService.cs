using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Manager.Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext context_new;
        public DoctorService(HospitalDbContext context)
        {
            context_new = context;
        }
        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await context_new.Doctors.Include(d => d.Hospital).ToListAsync();
        }
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await context_new.Doctors.FindAsync(id);
        }
        public async Task<Doctor?> GetDoctorDetailsAsync(int id)
        {
            return await context_new.Doctors.Include(d => d.Hospital).Include(d => d.DoctorPatients).ThenInclude(d => d.Patient).FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            context_new.Doctors.Add(doctor);
            await context_new.SaveChangesAsync();
        }
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await context_new.SaveChangesAsync();
        }
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await context_new.Doctors.Include(d => d.DoctorPatients).FirstOrDefaultAsync(d => d.Id == id);
            if (doctor != null)
            {
                context_new.DoctorPatients.RemoveRange(doctor.DoctorPatients);
                context_new.Doctors.Remove(doctor);
                await context_new.SaveChangesAsync();
            }
        }
    }
}

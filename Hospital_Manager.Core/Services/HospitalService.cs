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
    public class HospitalService : IHospitalService
    {
        private readonly HospitalDbContext context_new;
        public HospitalService(HospitalDbContext context)
        {
            context_new = context;
        }
        public async Task<List<Hospital>> GetAllHospitalsAsync(string? search = null, int? minBeds = null, string? sortOrder = null)
        {
            var query = context_new.Hospitals.Where(h => !h.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(h => h.Name.Contains(search));
            }
            if (minBeds.HasValue)
            {
                query = query.Where(h => h.BedsCount > minBeds.Value);
            }
            switch (sortOrder)
            {
                case "name": 
                    query = query.OrderBy(h => h.Name);
                    break;
                case "beds_asc": 
                    query = query.OrderBy(h => h.BedsCount); 
                    break;
                case "beds_desc":
                    query = query.OrderByDescending(h => h.BedsCount);
                    break;
                default:
                    query = query.OrderBy(h => h.Name);
                    break;
            }
            return await query.ToListAsync();
        }
        public async Task<Hospital?> GetHospitalByIdAsync(int id)
        {
            return await context_new.Hospitals.FindAsync(id);
        }
        public async Task AddHospitalAsync(Hospital hospital)
        {
            context_new.Hospitals.Add(hospital);
            await context_new.SaveChangesAsync();
        }
        public async Task UpdateHospitalAsync(Hospital hospital)
        {
            await context_new.SaveChangesAsync();
        }
        public async Task DeleteHospitalAsync(int id)
        {
            var hospital = await context_new.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                hospital.IsDeleted = true;
                await context_new.SaveChangesAsync();
            }
        }
    }
}

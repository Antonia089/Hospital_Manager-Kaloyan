using Data_Hospital_Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital_Manager.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(
                new Doctor {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Specialty = "Cardiology",
                    Email = "email@mail.com",
                    PhoneNumber = "0888888888",
                    HospitalId = 1                    
                },
                new Doctor {
                    Id = 2,
                    FirstName = "Georgi",
                    LastName = "Ivanov",
                    Specialty = "Neurology",
                    Email = "gmail@gmail.com",
                    PhoneNumber = "0899999999",
                    HospitalId = 2
                }
            );
        }
    }
}

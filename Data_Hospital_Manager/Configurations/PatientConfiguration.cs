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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "Petar",
                    LastName = "Petrov",
                    Email = "email@email.com",
                    PhoneNumber = "0888888888"
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Georgi",
                    LastName = "Georgiev",
                    Email = "gmail@email.com",
                    PhoneNumber = "0899999999"
                }
            );
        }
    }
}

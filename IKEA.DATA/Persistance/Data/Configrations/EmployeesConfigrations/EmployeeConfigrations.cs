using IKEA.DAl.Common.Enums;
using IKEA.DAl.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAl.Persistance.Data.Configrations.EmployeesConfigrations
{
    public class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("varchar(100)");
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");
            builder.Property(e => e.Gender).HasConversion
                (
                (gender)=>gender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(e => e.EmployeeType).HasConversion
               (
               (type) => type.ToString(),
               (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
               );
            //Development Usage 
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");

            builder.Property(D => D.LastModifiedon).HasComputedColumnSql("GetDate()");
        }
    }
}

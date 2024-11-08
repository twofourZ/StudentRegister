using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentRegister.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister.Data
{
    internal class StudentRegisterDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<PhD> PhDs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["StudentRegister"]);
        }

        public void InitializeDefaultData()
        {
            if (Teachers.Count() < 1)
            {
                var teacher1 = new Teacher()
                {
                    FirstName = "Isaac",
                    LastName = "Newton",
                    PhDs = new List<PhD>() {
                    new PhD() { Subject = "Mathematics" },
                    new PhD() { Subject = "Physics" },
                    new PhD() { Subject = "Alchemy" },
                    new PhD() { Subject = "Theology" }
                }
                };

                if (!Courses.Any(x => x.Teacher == teacher1))
                {
                    var mathematics1 = new Course()
                    {
                        Teacher = teacher1,
                        Subject = teacher1.PhDs[0].Subject,
                    };

                    var physics1 = new Course()
                    {
                        Teacher = teacher1,
                        Subject = teacher1.PhDs[1].Subject
                    };

                    var alchemy1 = new Course()
                    {
                        Teacher = teacher1,
                        Subject = teacher1.PhDs[2].Subject
                    };

                    var theology1 = new Course()
                    {
                        Teacher = teacher1,
                        Subject = teacher1.PhDs[3].Subject
                    };

                    Courses.AddRange(mathematics1, physics1, alchemy1, theology1);
                }

                Teachers.Add(teacher1);
                this.SaveChanges();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister
{
    internal class StudentRegisterDbContext : DbContext
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentRegister;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<PhD> PhDs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public void InitializeDefaultData()
        {
            if (this.Teachers.Count() < 1)
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

                if (!this.Courses.Any(x => x.Teacher == teacher1))
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

                    this.Courses.AddRange(mathematics1, physics1, alchemy1, theology1);
                }

                this.Teachers.Add(teacher1);
                this.SaveChanges();
            }
        }
    }
}

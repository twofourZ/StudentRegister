using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister.DbModels
{
    internal class Course
    {
        private string? subject;
        public int CourseId { get; set; }
        public required Teacher Teacher { get; set; }
        public virtual List<Student> Students { get; set; } = new List<Student>();
        public required string? Subject
        {
            get => subject;
            set
            {
                if (Teacher.PhDs.Any(x => x.Subject == value))
                {
                    subject = value;
                }
                else
                {
                    throw new ArgumentException("Teacher is not qualified");
                }
            }
        }

        public Course()
        {

        }
    }
}

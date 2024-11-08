using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister.DbModels
{
    internal class Teacher
    {
        private List<PhD> phDs = new List<PhD>();

        public int TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public virtual List<PhD> PhDs
        {
            get { return phDs; }
            set { phDs = value; }
        }
        public virtual List<Course> Courses { get; set; } = new List<Course>();
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AcademyRegister.DbModels
{
    internal class PhD
    {
        public int PhDId { get; set; }
        public required string Subject { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
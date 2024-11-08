using Microsoft.EntityFrameworkCore;
using AcademyRegister.Data;

namespace AcademyRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new AcademyRegisterDbContext();

            context.InitializeDefaultData();

            var ui = new UserInterface(context);

            ui.MainMenu();
        }
    }
}

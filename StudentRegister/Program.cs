using Microsoft.EntityFrameworkCore;

namespace StudentRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentRegisterDbContext();

            context.InitializeDefaultData();

            var ui = new UserInterface(context);

            ui.MainMenu();
        }
    }
}

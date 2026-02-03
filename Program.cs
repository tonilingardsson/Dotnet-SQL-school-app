using Skola_ER_Application.Models;

namespace Skola_ER_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ErSkolaContext())

                // Build main menu
            {
                var allStaff = context.Staff.ToList();

                foreach (var staff in allStaff) 
                {
                    Console.WriteLine($"{staff.StaffFirstName} {staff.StaffLastName} - {staff.RoleId}");
                }
            }
            Console.ReadKey();
        }

    }
}

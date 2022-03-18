
using Microsoft.EntityFrameworkCore;
using atm_machine_api.Models;

namespace atm_machine_api.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDBContext>());
            }
        }

        private static void SeedData(ApplicationDBContext context)
        {
            // if (isProd)
            // {
            //     Console.WriteLine("--> Attempting to apply migrations...");
            //     try
            //     {
            //         context.Database.Migrate();
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            //     }
            // }

            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Users.AddRange(
                    new Users() { firstName = "Jessie", lastName = "Furigay", cardNo = 12345, balance = 100000, pinNo = 12345 },
                    new Users() { firstName = "Bill", lastName = "Gates", cardNo = 54321, balance = 200000, pinNo = 54321 },
                    new Users() { firstName = "Ben", lastName = "Aflic", cardNo = 24689, balance = 500000, pinNo = 24689 }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
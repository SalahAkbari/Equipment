using System;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.DataAccess.DbContext;
using EquipmentRental.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EquipmentRental.DataAccess
{
    public static class DbSeeder
    {
        #region Public Methods

        public static void Seed(SqlDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager)
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any())
                CreateCustomers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            // create Inventory with auto-generated static data
            if (!dbContext.Inventories.Any()) FillInventory(dbContext);
        }

        #endregion

        #region Seed Methods

        private static async Task CreateCustomers(SqlDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            string role_Administrator = "Administrator";
            string role_RegisteredUser = "RegisteredUser";
            //Create Roles (if they doesn't exist yet)
            if (!await roleManager.RoleExistsAsync(role_Administrator))
            {
                await roleManager.CreateAsync(new
                    IdentityRole(role_Administrator));
            }
            if (!await roleManager.RoleExistsAsync(role_RegisteredUser))
            {
                await roleManager.CreateAsync(new
                    IdentityRole(role_RegisteredUser));
            }
            // Create the "Admin" ApplicationUser account
            var userAdmin = new Customer
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@domain.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            // Insert "Admin" into the Database and assign the "Administrator"
            //and "RegisteredUser" roles to him.
            if (await userManager.FindByNameAsync(userAdmin.UserName) == null)
            {
                await userManager.CreateAsync(userAdmin, "Pass4Admin");
                await userManager.AddToRoleAsync(userAdmin,
                    role_RegisteredUser);
                await userManager.AddToRoleAsync(userAdmin,
                    role_Administrator);
                // Remove Lockout and E-Mail confirmation.
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }
#if DEBUG
            // Create some sample registered user accounts (if they don't exist already)
            var userRyan = new Customer
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@domain.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var userSolice = new Customer
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Solice",
                Email = "solice@domain.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var userVodan = new Customer
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Vodan",
                Email = "vodan@domain.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            // Insert sample registered users into the Database and also assign
            //the "Registered" role to him.
            if (await userManager.FindByNameAsync(userRyan.UserName) == null)
            {
                await userManager.CreateAsync(userRyan, "Pass4Ryan");
                await userManager.AddToRoleAsync(userRyan,
                    role_RegisteredUser);
                // Remove Lockout and E-Mail confirmation.
                userRyan.EmailConfirmed = true;
                userRyan.LockoutEnabled = false;
            }
            if (await userManager.FindByNameAsync(userSolice.UserName) ==
                null)
            {
                await userManager.CreateAsync(userSolice, "Pass4Solice");
                await userManager.AddToRoleAsync(userSolice,
                    role_RegisteredUser);
                // Remove Lockout and E-Mail confirmation.
                userSolice.EmailConfirmed = true;
                userSolice.LockoutEnabled = false;
            }
            if (await userManager.FindByNameAsync(userVodan.UserName) == null)
            {
                await userManager.CreateAsync(userVodan, "Pass4Vodan");
                await userManager.AddToRoleAsync(userVodan,
                    role_RegisteredUser);
                // Remove Lockout and E-Mail confirmation.
                userVodan.EmailConfirmed = true;
                userVodan.LockoutEnabled = false;
            }
#endif
            dbContext.SaveChanges();
        }

        private static void FillInventory(SqlDbContext dbContext)
        {
#if DEBUG
            // create Inventory with auto-generated static data
                
            dbContext.Inventories.Add(new Inventory
            {
                Name = "Caterpillar bulldozer",
                Type = Domain.Enums.EquipmentType.Heavy
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "KamAZ truck",
                Type = Domain.Enums.EquipmentType.Regular
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Komatsu crane",
                Type = Domain.Enums.EquipmentType.Heavy
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Volvo steamroller",
                Type = Domain.Enums.EquipmentType.Regular
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Bosch jackhammer",
                Type = Domain.Enums.EquipmentType.Specialized
            });

            // persist the changes on the Database
            dbContext.SaveChanges();
        }
#endif

        #endregion

    }
}

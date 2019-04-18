using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.DataAccess.DbContext;
using EquipmentRental.Domain.Entities;
using EquipmentRental.Domain.Enums;
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

            //Uncomment the following line if you need to add some Transactions
            //if (!dbContext.Transactions.Any()) AddSomeTransactions(dbContext);
        }

        #endregion

        #region Seed Methods

        private static async Task CreateCustomers(SqlDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager)
        {
            // local variables
            var createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            var lastModifiedDate = DateTime.Now;
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
                Type = EquipmentType.Heavy
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "KamAZ truck",
                Type = EquipmentType.Regular
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Komatsu crane",
                Type =EquipmentType.Heavy
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Volvo steamroller",
                Type = EquipmentType.Regular
            });

            dbContext.Inventories.Add(new Inventory
            {
                Name = "Bosch jackhammer",
                Type = EquipmentType.Specialized
            });

            // persist the changes on the Database
            dbContext.SaveChanges();
        }
#endif

        private static void AddSomeTransactions(SqlDbContext dbContext)
        {
#if DEBUG
            var inventories = dbContext.Inventories.ToList();

            var inventory1 = inventories.FirstOrDefault(c => c.InventoryID.Equals(1));
            if (inventory1 != null)
                dbContext.Transactions.Add(new Transaction
                {
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98",
                    EquipmentId = 1,
                    TransactionDateTime = DateTime.Now,
                    Days = 1,
                    Price = CalculatePrice(1, inventory1.Type),
                    Points = CalculatePoints(inventory1.Type)
                });

            var inventory2 = inventories.FirstOrDefault(c => c.InventoryID.Equals(2));
            if (inventory2 != null)
                dbContext.Transactions.Add(new Transaction
                {
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98",
                    EquipmentId = 2,
                    TransactionDateTime = DateTime.Now,
                    Days = 2,
                    Price = CalculatePrice(2, inventory2.Type),
                    Points = CalculatePoints(inventory2.Type)
                });

            var inventory3 = inventories.FirstOrDefault(c => c.InventoryID.Equals(3));
            if (inventory3 != null)
                dbContext.Transactions.Add(new Transaction
                {
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98",
                    EquipmentId = 3,
                    TransactionDateTime = DateTime.Now,
                    Days = 3,
                    Price = CalculatePrice(3, inventory3.Type),
                    Points = CalculatePoints(inventory3.Type)
                });

            var inventory4 = inventories.FirstOrDefault(c => c.InventoryID.Equals(4));
            if (inventory4 != null)
                dbContext.Transactions.Add(new Transaction
                {
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98",
                    EquipmentId = 4,
                    TransactionDateTime = DateTime.Now,
                    Days = 4,
                    Price = CalculatePrice(4, inventory4.Type),
                    Points = CalculatePoints(inventory4.Type)
                });

            var inventory5 = inventories.FirstOrDefault(c => c.InventoryID.Equals(5));
            if (inventory5 != null)
                dbContext.Transactions.Add(new Transaction
                {
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98",
                    EquipmentId = 5,
                    TransactionDateTime = DateTime.Now,
                    Days = 5,
                    Price = CalculatePrice(5, inventory5.Type),
                    Points = CalculatePoints(inventory5.Type)
                });

            // persist the changes on the Database
            dbContext.SaveChanges();
        }
#endif

        #endregion

        public static decimal CalculatePrice(int days,  EquipmentType equipmentType)
        {
            Dictionary<int, decimal> dictionary = new Dictionary<int, decimal>()
            {
                {1, days * (Fee.Fees["One-Time"] + Fee.Fees["Premium"])},
                {2, days > 2 ?  ((days - 2) * Fee.Fees["Regular"]) + (Fee.Fees["One-Time"] + Fee.Fees["Premium"])
                                         : ( Fee.Fees["One-Time"] + Fee.Fees["Premium"])},
                {3,  days > 3 ?  ((days - 3) * Fee.Fees["Regular"]) + Fee.Fees["Premium"]
                                         : Fee.Fees["Premium"]},

            };

            decimal value;
            dictionary.TryGetValue((int)equipmentType, out value);

            return value;
        }

        public static int CalculatePoints(EquipmentType equipmentType)
        {
            return (int)equipmentType == 1 ? 2 : 1;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PointOfSale.Data;
using PointOfSale.Models;
using PointOfSale.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,  
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
            )
           
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
           

        }
        public void Initialize()
        {
            // Migration if pandeing
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            } catch ( Exception ex )
            {

            }

            //Create Role if Not exits 
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                //Crate init User 

                _userManager.CreateAsync(new ApplicationUser
                {
                    Email = "anwaralrahmaniah.uae@gmail.com",
                    UserName = "anwaralrahmaniah.uae@gmail.com",
                    NormalizedEmail = "ANWARALRAHMANIAH.UAE@GMAIL.COM",
                    PhoneNumber = "1234567890",
                    Name = "Anwar",
                    Address = "UAE",                    
                }, "bAngladesh1@3").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u=>u.Email == "anwaralrahmaniah.uae@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }
            
            return;
        }
    }
}

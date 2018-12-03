using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class DbInitializer
    {
        private readonly EFContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public DbInitializer(
            EFContext db,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {


            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Tài khoản admin"

                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Nhân viên"

                });
                var user = await _userManager.FindByNameAsync("admin1");
                await _userManager.AddToRoleAsync(user, "Staff");
                var role = await _roleManager.FindByNameAsync("Staff");
                await _roleManager.AddClaimAsync(role, new Claim("Home", "View"));
            }
            if (await _userManager.FindByNameAsync("admin1") == null)
            {
                 await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin1",
                    FullName = "Administrator1",
                    Email = "nguyenductoan18@gmail.com",                   
                    Gender = true,
                    Avatar= "/images/user.png",
                   

                 }, "Ptscqng1234");
            }
            
        }
    }
}

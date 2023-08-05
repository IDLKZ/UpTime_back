using IdentityModel;
using IdentiyService.Configuration;
using IdentiyService.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentiyService.Database.Seeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbSeeder> logger, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public async Task SeedAsync()
        {
            try
            {

                if (_roleManager.FindByNameAsync(AppConstants.Admin).Result == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Admin));
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Owner));
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Manager));
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Teacher));
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Student));
                    _logger.LogInformation("Role created successfully");
                }

                if (!_userManager.Users.Any())
                {
                    //Create SuperAdmin
                    ApplicationUser superAdmin = new ApplicationUser
                    {
                        UserName = "superadmin@gmail.com",
                        Name = "Админ",
                        Surname = "Админов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777777",
                        IIN = "000000000000",
                        Status = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(superAdmin, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(superAdmin, AppConstants.Admin).GetAwaiter().GetResult();

                    var admin_claim = _userManager.AddClaimsAsync(superAdmin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Admin),
                        new Claim(JwtClaimTypes.Name,superAdmin.UserName),
                        new Claim(JwtClaimTypes.Email,superAdmin.Email),
                        new Claim("IIN",superAdmin.IIN),
                    }).Result;
                    //Create Methodist
                    ApplicationUser owner = new ApplicationUser
                    {
                        UserName = "owner@gmail.com",
                        Name = "Владелец",
                        Surname = "Владелецев",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "owner@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777778",
                        IIN = "111111111111",
                        Status = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(owner, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(owner, AppConstants.Owner).GetAwaiter().GetResult();

                    var owner_claim = _userManager.AddClaimsAsync(owner, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Owner),
                        new Claim(JwtClaimTypes.Name,owner.UserName),
                        new Claim(JwtClaimTypes.Email,owner.Email),
                        new Claim("IIN",owner.IIN),
                    }).Result;

                    
                    //Create Manager - Manager of the Branch
                    ApplicationUser manager = new ApplicationUser
                    {
                        UserName = "manager@gmail.com",
                        Name = "Менеджер",
                        Surname = "Менеджеров",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "manager@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777780",
                        IIN = "222222222222",
                        Status = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(manager, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(manager, AppConstants.Manager).GetAwaiter().GetResult();

                    var moderator_claim = _userManager.AddClaimsAsync(manager, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Manager),
                        new Claim(JwtClaimTypes.Name,manager.UserName),
                        new Claim(JwtClaimTypes.Email,manager.Email),
                        new Claim("IIN",manager.IIN),
                    }).Result;
                    //Create Teacher
                    ApplicationUser teacher = new ApplicationUser
                    {
                        UserName = "teacher@gmail.com",
                        Name = "Учитель",
                        Surname = "Учителев",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "teacher@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777781",
                        IIN = "444444444444",
                        Status = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(teacher, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(teacher, AppConstants.Teacher).GetAwaiter().GetResult();

                    var teacher_claim = _userManager.AddClaimsAsync(teacher, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Teacher),
                        new Claim(JwtClaimTypes.Name,teacher.UserName),
                        new Claim(JwtClaimTypes.Email,teacher.Email),
                        new Claim("IIN",teacher.IIN),
                    }).Result;

                    //Add Student
                    ApplicationUser student = new ApplicationUser
                    {
                        UserName = "student@gmail.com",
                        Name = "Студент",
                        Surname = "Студентов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "student@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777782",
                        IIN = "555555555555",
                        Status = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(student, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(student, AppConstants.Student).GetAwaiter().GetResult();

                    var student_claim = _userManager.AddClaimsAsync(student, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Student),
                        new Claim(JwtClaimTypes.Name,student.UserName),
                        new Claim(JwtClaimTypes.Email,student.Email),
                        new Claim("IIN",student.IIN),
                    }).Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), ex.ToString());
            }


        }
    }
}

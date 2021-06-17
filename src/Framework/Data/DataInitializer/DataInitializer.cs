using Core.Enums;
using Data.Entities;
using Data.Entities.Identity;
using Date;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.DataInitializer
{
    public class DataInitializer
    {
        #region Fields

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly ApplicationDbContext _dbContext;

        #endregion Fields

        #region Ctor

        public DataInitializer(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        #endregion Ctor

        #region Methods

        public void InstallRequierdData()
        {
            InsertTenants();
            InsertTeams();
            InsertRoles();
            InsertUsers();
            InsertUserRoles();
        }

        private void InsertTenants()
        {
            var tenants = new List<Tenant>
                {
                    new Tenant{
                        Name ="Manhattan Team"
                    },
                    new Tenant{
                        Name ="England Team"
                    },
                };

            foreach (var tenant in tenants)
            {
                if (_dbContext.Set<Tenant>().AsNoTracking().Any(p => p.Name == tenant.Name) is false)
                {
                    _dbContext.Add(tenant);
                }
            }

            _dbContext.SaveChanges();
        }

        private void InsertTeams()
        {
            var teams = new List<Team>
                {
                    new Team {
                        Name ="Manhattan Team",
                        Description = "Manhattan Team of Health",
                        ChildTeams = new List<Team> {
                            new Team { Name ="Broadway Team", Description = "Broadway Team of Health"},
                            new Team { Name ="Canal Team", Description = "Canal Team of Health"},
                        },
                        TenantId = _dbContext.Set<Tenant>().Where(p=>p.Name =="Manhattan Team").Select(p=>p.Id).FirstOrDefault(),
                    },
                    new Team {
                        Name ="England Team",
                        Description = "England Team",
                        ChildTeams = new List<Team> {
                            new Team { Name ="London Team", Description = "London Team"},
                            new Team { Name ="Manchester Team", Description = "Manchester Team" , ChildTeams = new List<Team> {
                                                                                                 new Team { Name ="Cheetham Hill Road Team", Description = "Cheetham Hill Road Team"},
                                                                                                 new Team { Name ="Deansgate Team", Description = "Deansgate Team"},
                                                                                                 new Team { Name ="Corporation Street Team", Description = "Corporation Street Team"},
                                                                                                 new Team { Name ="King Street Team", Description = "King Street Team" , ChildTeams = new List<Team> {
                                                                                                                                                                         new Team { Name ="100 King Street Team", Description = "100 King Street Team"},
                                                                                                                                                                         new Team { Name ="Canal House Team", Description = "Canal House Team"},
                                                                                                                                                                         new Team { Name ="Reform Club Team", Description = "Reform Club Team"},
                                                                                                                                                                         }}
                                                                                                 }},
                        },
                        TenantId = _dbContext.Set<Tenant>().Where(p=>p.Name =="England Team").Select(p=>p.Id).FirstOrDefault(),
                    }
               };

            foreach (var team in teams)
            {
                if (_dbContext.Set<Team>().AsNoTracking().Any(p => p.Name == team.Name) is false)
                {
                    _dbContext.Add(team);
                }
            }

            _dbContext.SaveChanges();
        }

        private void InsertRoles()
        {
            var roles = new List<Role>
                {
                    new Role{Name = "Admin", Description = "This role has superuser privileges" },
                    new Role{Name = "User" , Description = "This role belong to users that work in company or Teams" },
                };

            foreach (var role in roles)
            {
                if (_roleManager.Roles.AsNoTracking().Any(p => p.Name == role.Name) is false)
                {
                    _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }
        }

        private void InsertUsers()
        {
            var users = new List<User>
                {
                    new User{Firstname = "Admin" , Lastname = "Admini" , Gender = GenderType.Male,UserName = "admin" , Email = "admin@site.com" , TeamId = 1},
                    new User{Firstname = "User1" , Lastname = "User1i" , Gender = GenderType.Male,UserName = "user1" , Email = "user1@site.com" , TeamId = 1},
                    new User{Firstname = "User2" , Lastname = "User2i" , Gender = GenderType.Male,UserName = "user2" , Email = "user2@site.com" , TeamId = 1},
                };

            foreach (var user in users)
            {
                if (_userManager.Users.AsNoTracking().Any(p => p.UserName == user.UserName) is false)
                {
                    _userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();
                }
            }
        }

        private void InsertUserRoles()
        {
            var userRoles = new List<UserRole>
                {
                   new UserRole{UserId = _userManager.FindByNameAsync("admin").Result.Id,RoleId = _roleManager.FindByNameAsync("Admin").Result.Id },
                   new UserRole{UserId = _userManager.FindByNameAsync("user1").Result.Id,RoleId = _roleManager.FindByNameAsync("User").Result.Id },
                   new UserRole{UserId = _userManager.FindByNameAsync("user2").Result.Id,RoleId = _roleManager.FindByNameAsync("User").Result.Id },
                };

            foreach (var userRole in userRoles)
            {
                if (_dbContext.Set<UserRole>().AsNoTracking().Any(p => p.UserId == userRole.UserId && p.RoleId == userRole.RoleId) is false)
                {
                    _dbContext.Add(userRole);
                }
            }

            _dbContext.SaveChanges();
        }

        #endregion Methods
    }
}
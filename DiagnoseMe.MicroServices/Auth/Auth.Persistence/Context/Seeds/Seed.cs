using Auth.Application.Common.Interfaces.Services;
using Auth.Domain.Entities;
using Auth.Persistence.Context;
using Auth.Persistence.Context.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class Seed
{
    public static ModelBuilder SeedData(this ModelBuilder modelBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var configuration = builder.Configuration;
        configuration.AddUserSecrets(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Entity<IdentityRole>().HasData(DefaultRoles.IdentityRoleList());
        modelBuilder.Entity<ApplicationUser>().HasData(DefaultAdmin.AdminUser(configuration));
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(DefaultAdminRole.Role);
        return modelBuilder;
    }
}
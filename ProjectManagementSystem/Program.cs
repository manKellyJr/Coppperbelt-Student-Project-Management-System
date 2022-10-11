using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;
using MudBlazor.Services;
using ProjectManagementSystem.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectManagementSystem.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
builder.Services.AddDbContext<ProjectManagementDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddIdentity<User, Role>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;


}).AddEntityFrameworkStores<ProjectManagementDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<SchoolService>();
builder.Services.AddTransient<GroupService>();
builder.Services.AddTransient<StudentService>();
builder.Services.AddTransient<CoordinatorService>();
builder.Services.AddTransient<ProjectService>();
builder.Services.AddTransient<ProgrammeService>();
builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<CourseService>();
builder.Services.AddTransient<SupervisorService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
await app.SeedAdmin();
app.Run();
public static class Extensions
{
    public static async Task<IApplicationBuilder> SeedAdmin(this IApplicationBuilder app)
    {    
        var scope = app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
        if( await userManager.FindByEmailAsync("test@admin.com") is not null)
        {
            // do nothing
        }
        else
        {
            var role = await roleManager.FindByNameAsync("Administrator");
            if (role is null)
            {
                await roleManager.CreateAsync(new Role("Administrator"));
            }
            else { }
            var user = new User
            {
                UserName = "test@admin.com",
                Email = "test@admin.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(user, "test1234");
            await userManager.AddToRoleAsync(user, "Administrator");
        }
        return app;
    }
}
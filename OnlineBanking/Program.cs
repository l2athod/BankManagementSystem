using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineBanking.Data;
using OnlineBanking.DataAccessLayer;
using OnlineBanking.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Authentication Settings
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.AccessDeniedPath = "/denied";
    options.Events = new CookieAuthenticationEvents()
    {
        OnSignedIn = async context =>
        {
            await Task.CompletedTask;
        },
        OnValidatePrincipal = async context =>
        {
            await Task.CompletedTask;
        }
    };
});

//Untuk Melempar Ke Halaman Login jika belum Login
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/User/Login");

builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddScoped<IBpkb, BPKBServices>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "mysession.frontend";
    options.IdleTimeout = TimeSpan.FromMinutes(90);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

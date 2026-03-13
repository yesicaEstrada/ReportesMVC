using ReportesMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<UsuarioDbContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); //habilita la sesion

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Ingreso}/{action=Login}/{id?}");

app.Run();

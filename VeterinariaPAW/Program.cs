using Microsoft.EntityFrameworkCore; // Importar Entity Framework Core
using VeterinariaPAW.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using VeterinariaPAW.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext con la cadena de conexi�n desde appsettings.json
builder.Services.AddDbContext<VeterinariaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Habilitar el uso de sesiones
builder.Services.AddDistributedMemoryCache(); // Necesario para almacenar sesiones en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Asegura que la cookie de sesi�n no sea accesible por scripts
    options.Cookie.IsEssential = true; // Necesario para que funcione en GDPR-compliance
});


//Visualizar Movimientos
builder.Services.AddScoped<BitacoraService>();


//Agrega la API
builder.Services.AddHttpClient<GometaApiService>();

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Habilitar HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Middleware para usar sesiones

app.UseAuthorization();

// Configurar la ruta predeterminada
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

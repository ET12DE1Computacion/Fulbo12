using Fulbo12.Core.Persistencia;
using Fulbo12.Core.Persistencia.EFC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Fulbo12Contexto>();
builder.Services.AddTransient<IUnidad, Unidad>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Listado",
    pattern: "{controller=Home}",
    defaults: new {action="Listado"});

app.MapControllerRoute(
    name: "Detalle",
    pattern: "{controller=Home}/{id:int:required}",
    defaults: new {action="Detalle"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
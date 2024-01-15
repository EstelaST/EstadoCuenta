using CuentasWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region "Servicios"
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


#endregion


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Usuario}/{id?}");
app.Run();

//builder.Services.AddSession(options =>
//{
//    //options.Cookie.Name = ".AdventureWorks.Session";
//    //options.IdleTimeout = TimeSpan.FromSeconds(10);
//    //options.Cookie.IsEssential = true;
//    options.u


//});
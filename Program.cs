using SignalRExample.Business.Concrete;
using SignalRExample.Business.Interface;
using SignalRExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IMyBusiness, MyBusiness>();


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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}/{message?}");

//Client'ýmýzýn baðlantý saðlayacaðý adres
app.MapHub<MyHub>("/myHub");

app.Run();

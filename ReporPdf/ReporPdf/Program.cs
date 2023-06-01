using SunttelTPointReporPdf.Interfaces.Sale;
using SunttelTPointReporPdf.Interfaces.IPayment;
using SunttelTPointReporPdf.Interfaces.TransactionalReport;
using SunttelTPointReporPdf.Services.SaleServices;
using SunttelTPointReporPdf.Services.ReportServices;
using SunttelTPointReporPdf.Services.SaleServices;
using SunttelTPointReporPdf.Services.PaymentServices;
using SunttelTPointReporPdf.Interfaces;
using SunttelTradePointB.Shared.Accounting;
using SunttelTPointReporPdf.Services.CreditServices;
using SunttelTPointReporPdf.Interfaces.CreditReport;
using Microsoft.Extensions.DependencyInjection;
using SunttelTPointReporPdf.Interfaces.IActor;
using SunttelTPointReporPdf.Services.Actor;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdminConnection");


#region Services
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISale, SaleServices>();
builder.Services.AddTransient<IPayment, PaymentServices>();
builder.Services.AddTransient<ITransactionalItem , TransactionalItemServices>();
builder.Services.AddTransient<ICreditDocument, CreditServices>();
builder.Services.AddTransient<IActor, EntityNodesServicescs>();

#endregion Services


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SaleReport}/{action=Home}/{id?}");

IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");


app.Run();

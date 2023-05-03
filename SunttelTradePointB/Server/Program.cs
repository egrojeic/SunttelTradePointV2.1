using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using SunttelTradePointB.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using SunttelTradePointB.Server.Services;
using System.Reflection;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Server.Interfaces.DAS_FilePreOders;
using SunttelTradePointB.Server.Services.DAS_FilePreOders;
using SunttelTradePointB.Server.Interfaces.BL_FilePreOders;
using SunttelTradePointB.Server.Services.BL_FilePreOders;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Server.Services.DAM_FilePreOders;
using SunttelTradePointB.Server.InterfacesMigration;
using SunttelTradePointB.Server.ProcessMigration;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Server.Services.MasterTablesServices;
using SunttelTradePointB.Server.Hubs;
using SunttelTradePointB.Server.Interfaces.Communications;
using SunttelTradePointB.Server.Services.Communications;
using SunttelTradePointB.Server.Interfaces.UserTracking;
using SunttelTradePointB.Server.Services.UserTracking;
using SunttelTradePointB.Shared.Models;
using SunttelTradePointB.Server.Interfaces.SalesBkServices;
using SunttelTradePointB.Server.Services.SalesBkServices;
using SunttelTradePointB.Client.Pages.DirectoryInventory;
using SunttelTradePointB.Server.Services.InventoryBkServices;
using SunttelTradePointB.Server.Interfaces.InventoryBkServices;
using SunttelTradePointB.Server.InterfaceSwagger;
using SunttelTradePointB.Server.Interfaces.PaymentBkServices;
using SunttelTradePointB.Server.Services.PaymentBkServices;
using SunttelTradePointB.Server.Interfaces.CreditBkServices;
using SunttelTradePointB.Server.Services.CreditBkServices;
using SunttelTradePointB.Server.Interfaces.QualityBkServices;
using SunttelTradePointB.Server.Services.QualityBkServices;
using SunttelTradePointB.Server.Interfaces.StandingOrderBkServices;
using SunttelTradePointB.Server.Services.StandingOrderBkServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AdminConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<SunttelDBContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TradePoint - API", Version = "v1.0" });
    c.OperationFilter<HeaderSquadIdFilter>();
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<ISquadBack, SquadService>();
builder.Services.AddTransient<ISerDasFileEDI, SerDasFileEDI>();

builder.Services.AddTransient<ISerBLFileEDI, SerBLFileEDI>();

builder.Services.AddTransient<ISerDamFileEDI, SerDamFileEDI>();



builder.Services.AddTransient<IGeographicPlaces, GeographicPlacesService>();
builder.Services.AddTransient<IActorsNodes, EntityActorNodesService>();
builder.Services.AddTransient<ITransactionalItemsBack, TransactionalItemsService>();
builder.Services.AddTransient<ISelectorDataSource, SelectorsBackService>();
builder.Services.AddTransient<ITransactionalItemsRelatedConceptsBKService, TransactionalItemsRelatedConceptsService>();
builder.Services.AddTransient<IEntitiesRelatedConcepts, EntityActorsRelatedConceptsService>();
builder.Services.AddTransient<IMessagesValet, MessageValet>();
builder.Services.AddTransient<IInventory, InventoryService>();
builder.Services.AddTransient<IPayment, PaymentService>();
builder.Services.AddTransient<ICredit, CreditService>();
builder.Services.AddTransient<IQuality, QualityService>();
builder.Services.AddTransient<IStandingOrder, StandingOrderService>();

builder.Services.AddTransient<ICommercialDocument, CommercialDocumentsService>();


builder.Services.AddTransient<IUserTracking, UserTrackingService>();


/*Inyeccion de depencias Proceso migracion*/
builder.Services.AddTransient<ISerDBMigration, SerDBMigration>();


/*Inyeccion Depencias Mongo*/
builder.Services.AddTransient<ISerDamCustomer, SerDamCustomer>();





builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<UserHub>("/hubs/userHub");
});

app.Run();

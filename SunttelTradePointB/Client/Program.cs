using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SunttelTradePointB.Client;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Interfaces.SquadInterfaces;
using SunttelTradePointB.Client.Services;
using SunttelTradePointB.Client.Services.IAServices;
using SunttelTradePointB.Client.Services.MasterTablesServices;
using SunttelTradePointB.Client.Services.SalesServices;
using SunttelTradePointB.Client.Services.SquadServices;
using Syncfusion.Blazor;
using SunttelTradePointB.Client.Services.InventoryServices;
using SunttelTradePointB.Client.Services.PaymentServices;
using SunttelTradePointB.Client.Services.CreditDocumentServices;
using SunttelTradePointB.Client.Services.ShippingServices;
using SunttelTradePointB.Client.Services.StandingOrderServices;
using SunttelTradePointB.Shared.Quality;
using SunttelTradePointB.Client.Services.QualityEvaluationServices;
using SunttelTradePointB.Client.Services.AccountsReceivableServices;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9iS31TdkxhX39XdHddTw==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xSdEZjWXZcdnRcQmhYWA==;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0dgWX9XcnJVT2VYVk0=;ODQ1MzMxQDMyMzAyZTM0MmUzMERCWWpHN2NtYnFnSUthT1ByUUhHUkNyaW8rTFAzMWdsN0JjdXNzSjJ1V0E9;ODQ1MzMyQDMyMzAyZTM0MmUzMEV3MlgxYlJGZmdSVHd3TEk0VDBuQ21OeGVZQ1dlK255dlpyZDdqakt1YWM9;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdERhWn9eeXZTRmlVWEx/;ODQ1MzM0QDMyMzAyZTM0MmUzMEJvZnBKakp5VXZORGlselFuVHNiTDdwUzIrbHFUSWRGeCtPMGcwNElub0U9;ODQ1MzM1QDMyMzAyZTM0MmUzMENpR204T2k1cThJb25EMm1BTkZUR1Y0MHRUWVJUSGpSeWRVSnJHY2xPVlk9;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0dgWX9XcnJURmZfWEM=;ODQ1MzM3QDMyMzAyZTM0MmUzME1TcXg4NU9KMG5jZFhZaS9Fb3FaUVJhclA0T0pVemFGaVFvS0dnbGxaNW89;ODQ1MzM4QDMyMzAyZTM0MmUzMGZtbk44K3g3aWlqemFhYUZmK3kzTGNXRU00d3NhODFBKzQzZGYxOExiaWM9;ODQ1MzM5QDMyMzAyZTM0MmUzMEJvZnBKakp5VXZORGlselFuVHNiTDdwUzIrbHFUSWRGeCtPMGcwNElub0U9");


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

// Adding Business services
builder.Services.AddScoped<SQuadService>();
builder.Services.AddScoped<GeographicPlacesService>();
builder.Services.AddScoped<ActorsNodeService>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<CommunicationService>();
builder.Services.AddScoped<TransactionalItemsService>();
builder.Services.AddScoped<SalesDocuments>();
builder.Services.AddScoped<IARecognition>();
builder.Services.AddScoped<Inventory>();
builder.Services.AddScoped<PaymentServices>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<CreditDocumentServices>();
builder.Services.AddScoped<StandingOrderServices>();
builder.Services.AddScoped<ShippingServices>();
builder.Services.AddScoped<QualityEvaluationServices>();
builder.Services.AddScoped<AccountsReceivableServices>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSyncfusionBlazor();



await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CloudResumeSWA;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

#if DEBUG
    baseAddress = new Uri("http://localhost:7071");
#endif

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });

var host = builder.Build();

await host.Services.GetRequiredService<HttpClient>().PostAsync("api/Visit", null);

await host.RunAsync();
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Miles95APIV2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registrerer en HttpClient service med en "scoped lifetime"(En ny instans af servicen bliver oprettet for hver request)
//  (lamba expression "=>") tager sp(IServiceProvider) som et input og returnerer en ny instance af HttpClient
// BaseAddress for den nye HttpClient instans bliver sat til den faste base URL for API-kald

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://magsapi.onrender.com") });

await builder.Build().RunAsync();
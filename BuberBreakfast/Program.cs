// Create a new instance of the WebApplication builder and pass in the command line arguments
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add the controllers service to the builder's service collection
{
  builder.Services.AddControllers();
  builder.Services.AddScoped<IBreakfastService, BreakfastService>();
  builder.Services.AddScoped<IAPICommunicationMapping, APICommunicationMapping>();
}

// Build the application using the builder
var app = builder.Build();

// Enable HTTPS redirection middleware
{
  app.UseExceptionHandler("/error");
  app.UseHttpsRedirection();

  // Map the controllers to routes
  app.MapControllers();

  // Start the application
  app.Run();
}

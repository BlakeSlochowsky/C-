var webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddScoped<IClockAngleService, ClockAngleService>();

var webApplication = webApplicationBuilder.Build();
webApplication.MapControllers();
webApplication.Run();

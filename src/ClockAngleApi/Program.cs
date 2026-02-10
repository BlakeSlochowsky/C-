using ClockAngleApi.Services;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddEndpointsApiExplorer();
webApplicationBuilder.Services.AddSwaggerGen();
webApplicationBuilder.Services.AddScoped<IClockAngleService, ClockAngleService>();

var webApplication = webApplicationBuilder.Build();

if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseSwagger();
    webApplication.UseSwaggerUI();
}

webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();

webApplication.Run();

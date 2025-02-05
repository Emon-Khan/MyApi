using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    try{
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }catch(Exception ex){
        Console.WriteLine($"An error occured while migrating the database: {ex.Message}");
    }
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
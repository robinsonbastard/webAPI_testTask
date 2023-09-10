using webAPI_testTask;
using webAPI_testTask.Repositories.Implementations;
using webAPI_testTask.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("DataBaseSettings"));
builder.Services.AddSingleton<DataBaseConnection>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<EntityContext>();
builder.Services.AddScoped<IEmployeeRopository, EmployeeRopository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

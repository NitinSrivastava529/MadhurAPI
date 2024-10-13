using MadhurAPI.Data;
using MadhurAPI.Services.Interface;
using MadhurAPI.Services.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IAuthenticationRepository,AuthenticationRepository>();
builder.Services.AddCors(option => option.AddPolicy("MadhurCorsPolicy", policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MadhurCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

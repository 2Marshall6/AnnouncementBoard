using AnnouncementBoard.BLL.Interfaces;
using AnnouncementBoard.BLL.Services;
using AnnouncementBoard.DAL;
using AnnouncementBoard.DAL.Interfaces;
using AnnouncementBoard.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

    builder.Services.AddDbContextPool<AnnouncementBoardDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
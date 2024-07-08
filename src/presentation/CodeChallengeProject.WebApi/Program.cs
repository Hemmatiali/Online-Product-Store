using CodeChallengeProject.Application.Features.Product.Handlers;
using CodeChallengeProject.Application.Mapping;
using CodeChallengeProject.Persistence.Data.Contexts;
using CodeChallengeProject.Persistence.Data.Repositories;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
// DbContext services 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppSQLServerConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

#region IOC

// Application services
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProductCommandHandler)));
builder.Services.AddMemoryCache();


// Infrastructure services
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();


// Domain services

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
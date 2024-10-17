using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    // Data Protection setup
    services.AddDataProtection();
}
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarketPlace.Areas.Identity.Data;

[assembly: HostingStartup(typeof(OnlineMarketPlace.Areas.Identity.IdentityHostingStartup))]
namespace OnlineMarketPlace.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OnlineMarketContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OnlineMarketContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<OnlineMarketContext>();
            });
        }
    }
}
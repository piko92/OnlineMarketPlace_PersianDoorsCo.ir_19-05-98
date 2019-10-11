using Microsoft.EntityFrameworkCore;
using OnlineMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.DataSeed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    LatinName = "Rial",
                    Name = "ریال",
                    Status = true,
                    Symbol = "ريال"
                },
                new Currency
                {
                    Id = 2,
                    LatinName = "Toman",
                    Name = "تومان",
                    Status = true,
                    Symbol = "تومان"
                },
                new Currency
                {
                    Id = 3,
                    LatinName = "Dollar",
                    Name = "دلار",
                    Status = true,
                    Symbol = "$"
                },
                new Currency
                {
                    Id = 4,
                    LatinName = "Euro",
                    Name = "یورو",
                    Status = true,
                    Symbol = "€"
                },
                new Currency
                {
                    Id = 5,
                    LatinName = "Yuan",
                    Name = "یوان",
                    Status = true,
                    Symbol = "¥"
                }
            );
        }
    }
}

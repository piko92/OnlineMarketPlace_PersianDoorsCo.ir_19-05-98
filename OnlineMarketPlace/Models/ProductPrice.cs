using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductPrice : IEntity<int>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PriceLevelOne { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PriceLevelTwo { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PriceLevelThree { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PriceLevelFour { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PriceLevelFive { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool AutoSync { get; set; }
        public bool Status { get; set; }
        public int? CurrencyId { get; set; }
        [Column(TypeName = "Money")]
        public decimal? BasedOnCurrencyPrice { get; set; }
        public bool BasedOnForeignCurrency { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductFeature Product { get; set; }
    }
}

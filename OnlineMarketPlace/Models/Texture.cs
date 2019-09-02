using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Texture
    {
        //public Texture()
        //{
        //    ProductFeature = new HashSet<ProductFeature>();
        //}

        public int Id { get; set; }
        public string TextureName { get; set; }
        public string TextureLatinName { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public DateTime? RegDateTime { get; set; }

        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}

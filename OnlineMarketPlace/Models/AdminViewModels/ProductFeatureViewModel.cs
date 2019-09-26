using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class ProductFeatureViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductFeatureId { get; set; }
        public List<string> FeatureData { get; set; }
        public List<int> AdditionalFeatureId { get; set; }
    }
}

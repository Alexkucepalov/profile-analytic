using System.Collections.Generic;

namespace Horizons.DTO
{
    public class ContrpartnerDto
    {
        public string ContrpartnerName { get; set; }
        public long? ContrpartnerInn { get; set; }
        public string ContrpartnerType { get; set; }
        public string ContrpartnerStatus { get; set; }
        public string LevelSale { get; set; }

        public virtual ICollection<long> SaleDocuments { get; set; }
    }
}

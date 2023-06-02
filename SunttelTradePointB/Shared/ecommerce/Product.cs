using SunttelTradePointB.Shared.Common;

namespace SunttelTradePointB.Shared.ecommerce
{
    public class Product : TransactionalItem
    {
        public List<Category> categories { get; set; }

        public EntityActor Buyer { get; set; }

        public string Description { get; set; }
    }
}

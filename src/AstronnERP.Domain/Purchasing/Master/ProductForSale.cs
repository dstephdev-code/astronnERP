using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master
{
    /* Вроде как отдельное Entity. */
    public class ProductForSale
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public Guid SellerId { get; init; }

        public Price Price { get; private set; }

        public int StockQuantity { get; private set; }

        public DateTimeOffset UpdatedLastTime { get; private set; }

        public String? Comment { get; private set; }
    }
}

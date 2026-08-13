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

        public Quantity StockQuantity { get; private set; }

        public DateTimeOffset UpdatedLastTime { get; private set; }

        public string? Comment { get; private set; }

        private void UpdateData() => UpdatedLastTime = DateTimeOffset.UtcNow;
    }
}

using AstronnERP.Domain.SharedObjects.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class ProductForSale
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public Guid SellerId { get; init; }

        public Price Price { get; private set; }

        public Quantity StockQuantity { get; private set; }

        public DateTimeOffset? UpdatedLastTime { get; private set; }

        public string? Comment { get; private set; }

        private ProductForSale(Guid productId, Guid sellerId, Price price, Quantity stockQuantity, string? comment)
        {
            Id = Guid.CreateVersion7();
            ProductId = productId;
            SellerId = sellerId;
            Price = price;
            StockQuantity = stockQuantity;
            if (comment is not null) Comment = comment;
        }

        public static Result<ProductForSale> Create(Product product, Counterparty seller, Price price, Quantity quantity, string? comment)
        {
            if (product.IsService && quantity.Type != QuantityType.Hours)
                return Result.Fail("Service can only be measured in hours.");

            return Result.Ok(new ProductForSale(product.Id, seller.Id, price, quantity, comment));
        }

        public Result UpdatePriceValue(decimal newPriceValue)
        {
            if (Price.Value == newPriceValue)
                return Result.Fail("Price is already the same.");

            var newPriceResult = Price.Create(newPriceValue, Price.Currency);

            if (!newPriceResult.IsSuccess)
                return newPriceResult.ToResult();

            Price = newPriceResult.Value;
            UpdateData();
            return Result.Ok();
        }

        public Result ChangePriceCurrency(Currency newCurrency)
        {
            if (Price.Currency == newCurrency)
                return Result.Fail("Currency is already the same.");

            var newPriceResult = Price.Create(Price.Value, newCurrency);

            if (!newPriceResult.IsSuccess)
                return newPriceResult.ToResult();

            Price = newPriceResult.Value;
            UpdateData();
            return Result.Ok();
        }
        public Result UpdateStockQuantity(float newQuantityValue)
        {
            var newQuantityResult = Quantity.Create(newQuantityValue, StockQuantity.Type);

            if (!newQuantityResult.IsSuccess)
                return newQuantityResult.ToResult();

            StockQuantity = newQuantityResult.Value;
            UpdateData();
            return Result.Ok();
        }

        public Result UpdateStockQuantity(int newQuantityValue)
        {
            if (StockQuantity.Value == newQuantityValue)
                return Result.Fail("Stock quantity is already the same.");

            var newQuantityResult = Quantity.Create(newQuantityValue, StockQuantity.Type);

            if (!newQuantityResult.IsSuccess)
                return newQuantityResult.ToResult();

            StockQuantity = newQuantityResult.Value;
            UpdateData();
            return Result.Ok();
        }

        public Result ChangeQuantityType(QuantityType newType)
        {
            if (StockQuantity.Type == newType)
                return Result.Fail("Stock quantity type is the same.");

            if (StockQuantity.Type == QuantityType.Hours)
                return Result.Fail("This product is a service. Any other stock quantity type than hours is wrong.");

            var newQuantityResult = Quantity.Create(StockQuantity.Value, newType);

            if (!newQuantityResult.IsSuccess)
                return newQuantityResult.ToResult();

            StockQuantity = newQuantityResult.Value;
            UpdateData();
            return Result.Ok();
        }

        /* TODO Check on constraints, rights and abuse-cases like SQL-injection */
        public Result AddComment(string comment)
        {
            Comment = string.Concat(Comment, comment);
            UpdateData();
            return Result.Ok();
        }

        public Result RewriteComment(string comment)
        {
            if (Comment is not null && Comment.Equals(comment))
                return Result.Fail("New comment is the same as old one.");

            Comment = comment;
            UpdateData();
            return Result.Ok();
        }
        /* -------------------------------------------------------------- */

        private void UpdateData()
        {
            // TODO Fetch data methods here?
            UpdatedLastTime = DateTimeOffset.UtcNow;
        }
    }
}

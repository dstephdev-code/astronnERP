using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.SharedObjects.ValueObjects
{
    public sealed record Price
    {
        public decimal Value { get; init; }
        public Currency Currency { get; init; }

        private Price(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public static (Price?, string?) Create(decimal value, Currency currency)
        {
            if (value <= 0)
                return (null, "Value error!");

            if (Enum.IsDefined<Currency>(currency))
                return (null, "Currency error!");

            return (new Price(value, currency), null);
        }
    }
}

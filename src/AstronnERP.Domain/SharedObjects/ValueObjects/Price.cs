using AstronnERP.Domain.SharedObjects.Enums;
using FluentResults;

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

        public static Result<Price> Create(decimal value, Currency currency)
        {
            var failures = new List<Result>
            {
                Result.FailIf(value <= 0, "Price must be greater than zero."),
                Result.FailIf(!Enum.IsDefined<Currency>(currency), "Currency must be of expected list."),
            };

            Result isFailed = failures.Merge();

            if (isFailed.IsFailed)
                return isFailed;
            else
                return Result.Ok<Price>(new Price(value, currency));
        }
    }
}

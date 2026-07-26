using AstronnERP.Domain.SharedObjects.Enums;
using FluentResults;

namespace AstronnERP.Domain.SharedObjects.ValueObjects
{
    public readonly record struct Price
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

            Result failureCheck = failures.Merge();

            if (failureCheck.IsFailed)
                return failureCheck;
            else
                return Result.Ok(new Price(value, currency));
        }
    }
}

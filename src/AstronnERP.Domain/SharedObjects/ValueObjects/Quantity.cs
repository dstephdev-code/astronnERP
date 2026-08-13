using AstronnERP.Domain.SharedObjects.Enums;
using FluentResults;

namespace AstronnERP.Domain.SharedObjects.ValueObjects
{
    public readonly record struct Quantity
    {
        private static readonly float RATIONAL_FINITY_BORDER = 0.00001f;
        public float Value { get; init; }
        public QuantityType Type { get; init; }

        private Quantity(float value, QuantityType type)
        {
            Value = value;
            Type = type;
        }

        public static Result<Quantity> Create(float value, QuantityType type)
        {
            var failures = new List<Result>
            {
                Result.FailIf(!float.IsFinite(value), "Quantity should be finite number."),
                Result.FailIf(Math.Abs(value) < RATIONAL_FINITY_BORDER, $"Quantity value must be greater than {RATIONAL_FINITY_BORDER:F8}."),
                Result.FailIf(value < 0, "Quantity must be greater than zero."),
                Result.FailIf(!Enum.IsDefined(type), "Type must be of expected list."),
            };

            Result failureCheck = failures.Merge();

            if (failureCheck.IsFailed)
                return failureCheck;
            else
                return Result.Ok(new Quantity(value, type));
        }

        public static Result<Quantity> Create(int value, QuantityType type)
        {

            var failures = new List<Result>
            {
                Result.FailIf(value <= 0, "Quantity must be greater than zero."),
                Result.FailIf(!Enum.IsDefined(type), "Type must be of expected list."),
            };

            Result failureCheck = failures.Merge();

            if (failureCheck.IsFailed)
                return failureCheck;
            else
                return Result.Ok(new Quantity(value, type));
        }
    }
}

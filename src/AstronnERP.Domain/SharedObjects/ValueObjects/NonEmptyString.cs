using AstronnERP.Domain.SharedObjects.Errors;
using FluentResults;

namespace AstronnERP.Domain.SharedObjects.ValueObjects
{
    public readonly record struct NonEmptyString
    {
        public string Value { get; init; }

        private NonEmptyString(string value) => Value = value;

        public static Result<NonEmptyString> Create(string value, string propertyName)
        {
            var trimedValue = value?.Trim();

            if (string.IsNullOrEmpty(trimedValue))
                return Result.Fail(new PropertyIsEmpty(propertyName));

            return Result.Ok(new NonEmptyString(trimedValue));
        }

        public static implicit operator string(NonEmptyString nonEmptyString) => nonEmptyString.Value;

        public override string ToString() => Value;
    }
}

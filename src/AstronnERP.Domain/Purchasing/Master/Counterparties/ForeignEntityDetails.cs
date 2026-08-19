using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.Purchasing.Master.Services;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master.Counterparties
{
    public record ForeignEntityDetails : CounterpartyDetails
    {
        public NonEmptyString FullNameEnglish { get; private set; }
        public NonEmptyString TaxNumber { get; private set; }
        public override CountryCode CountryCode { get; init; }
        public override CounterpartyType Type { get; init; }

        private ForeignEntityDetails(NonEmptyString fullNameEnglish, NonEmptyString taxNumber, CountryCode countryCode, CounterpartyType type)
        {
            FullNameEnglish = fullNameEnglish;
            TaxNumber = taxNumber;
            CountryCode = countryCode;
            Type = type;
        }

        public static Result<ForeignEntityDetails> Create(string name, string taxNumber, CountryCode countryCode, CounterpartyType type)
        {
            var nameNES = NonEmptyString.Create(name, nameof(FullNameEnglish));
            var taxNumberNES = NonEmptyString.Create(taxNumber, nameof(TaxNumber));
            var countryValidationResult = Result.FailIf(!Enum.IsDefined(countryCode), "Country code must be of expected list.");
            var typeValidationResult = Result.FailIf(!Enum.IsDefined(type), "Counterparty type must be of expected list.");

            var failCheck = Result.Merge(nameNES, taxNumberNES, countryValidationResult, typeValidationResult);

            if (!failCheck.IsSuccess)
                return failCheck.ToResult();

            var nameValid = Result.FailIf(!AccountValidator.IsNameOnlyEnglishLetters(nameNES.Value), "Name should contain only latin alphabet letters.");
            var taxNumberValid = Result.FailIf(!AccountValidator.IsThisTaxNumber(taxNumberNES.Value), "Tax number should contain only digits.");

            failCheck = Result.Merge(nameValid, taxNumberValid);

            if (!failCheck.IsSuccess)
                return failCheck.ToResult();

            return Result.Ok(new ForeignEntityDetails(nameNES.Value, taxNumberNES.Value, countryCode, type));
        }
        public Result ChangeName(string newName)
        {
            var newNameValidationResult = NonEmptyString.Create(newName, nameof(FullNameEnglish));
            var isSameValue = newNameValidationResult.IsSuccess && string.Equals(newNameValidationResult.Value.Value, FullNameEnglish.Value);

            var failureCheck = Result.Merge(
                newNameValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(FullNameEnglish)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            failureCheck = Result.FailIf(!AccountValidator.IsNameOnlyEnglishLetters(newNameValidationResult.Value), "Name should contain only latin alphabet letters.");

            if (failureCheck.IsSuccess)
                FullNameEnglish = newNameValidationResult.Value;

            return failureCheck.ToResult();
        }
        public Result ChangeTaxNumber(string newTaxNumber)
        {
            var newTaxNumberValidationResult = NonEmptyString.Create(newTaxNumber, nameof(TaxNumber));
            var isSameValue = newTaxNumberValidationResult.IsSuccess && string.Equals(newTaxNumberValidationResult.Value.Value, TaxNumber.Value);

            var failureCheck = Result.Merge(
                newTaxNumberValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(TaxNumber)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            failureCheck = Result.FailIf(!AccountValidator.IsThisTaxNumber(newTaxNumberValidationResult.Value), "Tax number is invalid.");

            if (failureCheck.IsSuccess)
                TaxNumber = newTaxNumberValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}

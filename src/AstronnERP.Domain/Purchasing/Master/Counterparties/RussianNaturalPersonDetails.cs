using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.Purchasing.Master.Services;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master.Counterparties
{
    public record RussianNaturalPersonDetails : CounterpartyDetails
    {
        public NonEmptyString TaxNumber { get; private set; }
        public override CountryCode CountryCode { get; init; }
        public override CounterpartyType Type { get; init; }

        private RussianNaturalPersonDetails(NonEmptyString taxNumber)
        {
            TaxNumber = taxNumber;
            CountryCode = CountryCode.RUS;
            Type = CounterpartyType.NaturalPerson;
        }

        public Result<RussianNaturalPersonDetails> Create(string taxNumber)
        {
            var taxNumberNES = NonEmptyString.Create(taxNumber, nameof(TaxNumber));

            if (!taxNumberNES.IsSuccess)
                return taxNumberNES.ToResult();

            var taxNumberValid = Result.FailIf(!AccountValidator.IsThisPersonalINN(taxNumberNES.Value), "INN is invalid.");

            if (!taxNumberValid.IsSuccess)
                return taxNumberValid;

            return Result.Ok(new RussianNaturalPersonDetails(taxNumberNES.Value));
        }
        public Result ChangeTaxNumber(string newTaxNumber)
        {
            var newTaxNumberValidationResult = NonEmptyString.Create(newTaxNumber, nameof(TaxNumber));
            var isSameValue = newTaxNumberValidationResult.IsSuccess && string.Equals(newTaxNumberValidationResult.Value.Value, TaxNumber.Value);

            var failureCheck = Result.Merge(
                newTaxNumberValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(TaxNumber))),
                Result.FailIf(!AccountValidator.IsThisPersonalINN(newTaxNumberValidationResult.Value), "INN is invalid.")
            );

            if (failureCheck.IsSuccess)
                TaxNumber = newTaxNumberValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}

using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.Purchasing.Master.Services;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master.Counterparties
{
    public record RussianGovernmentDetails : CounterpartyDetails
    {
        public NonEmptyString TaxNumber { get; private set; }
        public NonEmptyString KPP { get; private set; }
        public override CountryCode CountryCode { get; init; }
        public override CounterpartyType Type { get; init; }
        private RussianGovernmentDetails(NonEmptyString taxNumber, NonEmptyString kpp)
        {
            TaxNumber = taxNumber;
            KPP = kpp;
            CountryCode = CountryCode.RUS;
            Type = CounterpartyType.Government;
        }
        public Result<RussianGovernmentDetails> Create(string taxNumber, string kpp)
        {
            var taxNumberNES = NonEmptyString.Create(taxNumber, nameof(TaxNumber));
            var kppNumberNES = NonEmptyString.Create(kpp, nameof(KPP));

            var failCheck = Result.Merge(taxNumberNES, kppNumberNES);

            if (!failCheck.IsSuccess)
                return failCheck.ToResult();

            var taxNumberValid = Result.FailIf(!AccountValidator.IsThisGovernmentINN(taxNumberNES.Value), "INN is invalid.");
            var kppNumberValid = Result.FailIf(!AccountValidator.IsThisKPP(kppNumberNES.Value), "KPP is invalid.");

            failCheck = Result.Merge(taxNumberValid, kppNumberValid);

            if (!failCheck.IsSuccess)
                return failCheck.ToResult();

            return Result.Ok(new RussianGovernmentDetails(taxNumberNES.Value, kppNumberNES.Value));
        }
        public Result ChangeTaxNumber(string newTaxNumber)
        {
            var newTaxNumberValidationResult = NonEmptyString.Create(newTaxNumber, nameof(TaxNumber));
            var isSameValue = newTaxNumberValidationResult.IsSuccess && string.Equals(newTaxNumberValidationResult.Value.Value, TaxNumber.Value);

            var failureCheck = Result.Merge(
                newTaxNumberValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(TaxNumber))),
                Result.FailIf(!AccountValidator.IsThisGovernmentINN(newTaxNumberValidationResult.Value), "INN is invalid.")
            );

            if (failureCheck.IsSuccess)
                TaxNumber = newTaxNumberValidationResult.Value;

            return failureCheck.ToResult();
        }
        public Result ChangeKPPNumber(string newKPPNumber)
        {
            var newKPPNumberValidationResult = NonEmptyString.Create(newKPPNumber, nameof(KPP));
            var isSameValue = newKPPNumberValidationResult.IsSuccess && string.Equals(newKPPNumberValidationResult.Value.Value, KPP.Value);

            var failureCheck = Result.Merge(
                newKPPNumberValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(KPP))),
                Result.FailIf(!AccountValidator.IsThisKPP(newKPPNumberValidationResult.Value), "KPP is invalid.")
            );

            if (failureCheck.IsSuccess)
                KPP = newKPPNumberValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}

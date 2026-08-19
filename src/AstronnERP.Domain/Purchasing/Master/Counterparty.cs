using AstronnERP.Domain.Purchasing.Master.Counterparties;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Counterparty
    {
        public Guid Id { get; init; }

        public NonEmptyString FullName { get; private set; }

        public CounterpartyDetails Details { get; private set; }

        private Counterparty(NonEmptyString fullName, CounterpartyDetails details)
        {
            Id = Guid.CreateVersion7();
            FullName = fullName;
            Details = details;
        }

        public Result<Counterparty> Register(string name, CounterpartyDetails details) 
        {
            var nameValidationResult = NonEmptyString.Create(name, nameof(FullName));

            if (!nameValidationResult.IsSuccess)
                return nameValidationResult.ToResult();

            return Result.Ok(new Counterparty(nameValidationResult.Value, details));
        }
        public Result ChangeName(string newName)
        {
            var newNameValidationResult = NonEmptyString.Create(newName, nameof(FullName));
            var isSameValue = newNameValidationResult.IsSuccess && string.Equals(newNameValidationResult.Value.Value, FullName.Value);

            var failureCheck = Result.Merge(
                newNameValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(FullName)))
            );

            if (failureCheck.IsSuccess)
                FullName = newNameValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}

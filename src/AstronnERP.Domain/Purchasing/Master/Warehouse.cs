using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Warehouse
    {
        public Guid Id { get; init; }

        public NonEmptyString Name { get; private set; }

        private Warehouse(NonEmptyString name)
        {
            Id = Guid.CreateVersion7();
            Name = name;
        }
        
        public static Result<Warehouse> CreateNew(string name)
        {
            var nameValidationResult = NonEmptyString.Create(name, nameof(Name));

            if (nameValidationResult.IsFailed)
                return nameValidationResult.ToResult();

            return Result.Ok(new Warehouse(nameValidationResult.Value));
        }

        public Result ChangeName(string newName)
        {
            var newNameValidationResult = NonEmptyString.Create(newName, nameof(Name));
            var isSameValue = newNameValidationResult.IsSuccess && string.Equals(newNameValidationResult.Value.Value, Name.Value);

            var failureCheck = Result.Merge(
                newNameValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Name)))
            );

            if (failureCheck.IsSuccess)
                Name = newNameValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}

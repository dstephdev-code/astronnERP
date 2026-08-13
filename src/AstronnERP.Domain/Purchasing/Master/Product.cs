using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Product
    {
        public Guid Id { get; init; }
        public NonEmptyString Name { get; private set; }
        public NonEmptyString Code { get; private set; }
        public bool IsService { get; init; }
        public bool HasSerialNumber { get; init; }

        private Product(NonEmptyString name, NonEmptyString code, bool isService, bool hasSerialNumber)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Code = code;
            IsService = isService;
            HasSerialNumber = hasSerialNumber;
        }

        public static Result<Product> CreateNew(string name, string code, bool isService, bool hasSerialNumber)
        {
            var nameValidationResult = NonEmptyString.Create(name, nameof(Name));
            var codeValidationResult = NonEmptyString.Create(code, nameof(Code));

            var serviceDontHaveSerialRule = 
                Result.FailIf(isService && hasSerialNumber, "Services dont have serial number.");

            var failureCheck = Result.Merge(nameValidationResult, codeValidationResult, serviceDontHaveSerialRule);

            if (failureCheck.IsFailed)
                return failureCheck.ToResult();

            return Result.Ok(new Product(nameValidationResult.Value, codeValidationResult.Value, isService, hasSerialNumber));
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
        public Result ChangeCode(string newCode)
        {
            var newCodeValidationResult = NonEmptyString.Create(newCode, nameof(Code));
            var isSameValue = newCodeValidationResult.IsSuccess && string.Equals(newCodeValidationResult.Value.Value, Code.Value);

            var failureCheck = Result.Merge(
                newCodeValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Code)))
            );

            if (failureCheck.IsSuccess)
                Code = newCodeValidationResult.Value;

            return failureCheck.ToResult();
        }
    }
}
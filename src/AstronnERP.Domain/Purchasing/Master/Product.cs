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
            var nameResult = NonEmptyString.Create(name, nameof(Name));
            var codeResult = NonEmptyString.Create(code, nameof(Code));

            var failureCheck = Result.Merge(nameResult, codeResult);

            if (failureCheck.IsFailed)
                return failureCheck.ToResult();

            return Result.Ok(new Product(nameResult.Value, codeResult.Value, isService, hasSerialNumber));
        }
        public Result ChangeName(string newName)
        {
            var newNameResult = NonEmptyString.Create(newName, nameof(Name));
            var isSameValue = newNameResult.IsSuccess && string.Equals(newNameResult.Value.Value, Name.Value);

            var failureCheck = Result.Merge(
                newNameResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Name)))
            );

            if (failureCheck.IsSuccess)
                Name = newNameResult.Value;

            return failureCheck.ToResult();
        }
        public Result ChangeCode(string newCode)
        {
            var newCodeResult = NonEmptyString.Create(newCode, nameof(Code));
            var isSameValue = newCodeResult.IsSuccess && string.Equals(newCodeResult.Value.Value, Code.Value);

            var failureCheck = Result.Merge(
                newCodeResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Code)))
            );

            if (failureCheck.IsSuccess)
                Code = newCodeResult.Value;

            return failureCheck.ToResult();
        }
    }
}
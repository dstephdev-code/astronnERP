using AstronnERP.Domain.Purchasing.Master.Services;
using AstronnERP.Domain.SharedObjects.Enums;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class BankAccount
    {
        public Guid Id { get; init; }

        public Guid OwnerId { get; init; }

        public NonEmptyString AccountNumber { get; private set; }

        public Currency Currency { get; init; }

        public NonEmptyString BankLegalName { get; private set; }

        public NonEmptyString? BIK { get; private set; }

        public NonEmptyString? SWIFT { get; private set; }

        private BankAccount(Guid ownerId, NonEmptyString accountNumber, Currency currency, NonEmptyString bankLegalName)
        {
            Id = Guid.CreateVersion7();
            OwnerId = ownerId;
            AccountNumber = accountNumber;
            Currency = currency;
            BankLegalName = bankLegalName;
        }

        public static Result<BankAccount> Create(Counterparty owner, string accountNumber, Currency currency, string legalName, string? bikORswiftValue)
        {
            var accountNumberResult = NonEmptyString.Create(accountNumber, nameof(AccountNumber));
            var legalNameResult = NonEmptyString.Create(legalName, nameof(BankLegalName));

            var failCheck = Result.Merge(accountNumberResult, legalNameResult);

            if (!failCheck.IsSuccess)
                return failCheck.ToResult();

            var accountNumberValidationSuccessed = AccountValidator.IsThisBankAccountNumber(accountNumberResult.Value);

            if (!accountNumberValidationSuccessed)
                return Result.Fail("Account number is invalid.");

            var bankAccount = new BankAccount(owner.Id, accountNumberResult.Value, currency, legalNameResult.Value);

            if (bikORswiftValue is null)
                return Result.Ok(bankAccount);

            var additionalAccountDataAddResult = bankAccount.AddAccountDataBasedOnOwnerCountry(owner, bikORswiftValue);

            if (!additionalAccountDataAddResult.IsSuccess)
                return additionalAccountDataAddResult;

            return Result.Ok(bankAccount);
        }

        public Result ChangeAccountNumber(string newAccountNumber)
        {
            var newAccountNumberResult = NonEmptyString.Create(newAccountNumber, nameof(AccountNumber));
            var isSameValue = newAccountNumberResult.IsSuccess && string.Equals(newAccountNumberResult.Value.Value, AccountNumber.Value);

            var failureCheck = Result.Merge(
                newAccountNumberResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(AccountNumber)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            var accountNumberValidationSuccessed = AccountValidator.IsThisBankAccountNumber(newAccountNumberResult.Value);

            if (!accountNumberValidationSuccessed)
                return Result.Fail("New account number validation failed.");

            AccountNumber = newAccountNumberResult.Value;
            return failureCheck.ToResult();
        }
        public Result ChangeBankLegalName(string newName)
        {
            var newNameResult = NonEmptyString.Create(newName, nameof(BankLegalName));
            var isSameValue = newNameResult.IsSuccess && string.Equals(newNameResult.Value.Value, BankLegalName.Value);

            var failureCheck = Result.Merge(
                newNameResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(BankLegalName)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            BankLegalName = newNameResult.Value;
            return failureCheck.ToResult();
        }
        public Result ChangeBIK(string newBIK)
        {
            if (BIK is null)
                return Result.Fail("BIK is null. So either this bank account is not from Russian market, either there were some techincal issue.");

            var newBIKResult = NonEmptyString.Create(newBIK, nameof(BIK));
            var isSameValue = newBIKResult.IsSuccess && string.Equals(newBIKResult.Value.Value, BIK.Value);

            var failureCheck = Result.Merge(
                newBIKResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(BIK)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            BIK = newBIKResult.Value;
            return failureCheck.ToResult();
        }
        public Result ChangeSWIFT(string newSWIFT)
        {
            if (SWIFT is null)
                return Result.Fail("SWIFT is null. So either this bank account is from Russian market, either there were some techincal issue.");

            var newSWIFTResult = NonEmptyString.Create(newSWIFT, nameof(SWIFT));
            var isSameValue = newSWIFTResult.IsSuccess && string.Equals(newSWIFTResult.Value.Value, SWIFT.Value);

            var failureCheck = Result.Merge(
                newSWIFTResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(SWIFT)))
            );

            if (!failureCheck.IsSuccess)
                return failureCheck.ToResult();

            SWIFT = newSWIFTResult.Value;
            return failureCheck.ToResult();
        }

        private Result AddAccountDataBasedOnOwnerCountry(Counterparty owner, string data)
        {
            Result operationResult;
            if (owner.Details.CountryCode == Enums.CountryCode.RUS)
                operationResult = AddBIK(data);
            else
                operationResult = AddSWIFT(data);

            return operationResult;
        }

        private Result AddBIK(string bik)
        {
            var bikResult = NonEmptyString.Create(bik, nameof(BIK));
            if (!bikResult.IsSuccess)
                return bikResult.ToResult();

            BIK = bikResult.Value;
            return Result.Ok();
        }
        private Result AddSWIFT(string swift)
        {
            var swiftResult = NonEmptyString.Create(swift, nameof(SWIFT));
            if (!swiftResult.IsSuccess)
                return swiftResult.ToResult();

            SWIFT = swiftResult.Value;
            return Result.Ok();
        }
    }
}

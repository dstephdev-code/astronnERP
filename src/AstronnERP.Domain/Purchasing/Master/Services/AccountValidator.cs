using System.Text.RegularExpressions;
using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master.Services
{
    public static class AccountValidator
    {
        public static bool IsThisKPP(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]{4}[A-Za-z0-9]{2}[0-9]{3}$");
        public static bool IsThisCompanyINN(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]{10}$");
        public static bool IsThisGovernmentINN(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]{10}$");
        public static bool IsThisPersonalINN(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]{12}$");
        public static bool IsThisTaxNumber(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]$");
        public static bool IsNameOnlyEnglishLetters(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[a-zA-Z]$");
        public static bool IsThisBankAccountNumber(NonEmptyString validatingProperty) => Regex.IsMatch(validatingProperty.Value, @"^[0-9]$");
    }
}

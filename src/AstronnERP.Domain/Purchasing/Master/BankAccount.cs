using AstronnERP.Domain.SharedObjects.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;

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
    }
}

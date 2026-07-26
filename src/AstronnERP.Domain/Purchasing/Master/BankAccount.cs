using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class BankAccount
    {
        public Guid Id { get; init; }

        public Guid OwnerId { get; init; }

        public string AccountNumber { get; private set; }

        public Currency Currency { get; init; }

        public string BankLegalName { get; private set; }

        public string? BIK { get; private set; }

        public string? SWIFT { get; private set; }
    }
}

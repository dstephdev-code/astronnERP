using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class BankAccount
    {
        public Guid Id { get; init; }

        public Guid OwnerId { get; init; }

        public String AccountNumber { get; private set; }

        public Currency Currency { get; init; }

        public String BankLegalName { get; private set; }

        public String? BIK { get; private set; }

        public String? SWIFT { get; private set; }
    }
}

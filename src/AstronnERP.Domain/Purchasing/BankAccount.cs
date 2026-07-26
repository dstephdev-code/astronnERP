using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.Purchasing
{
    public class BankAccount
    {
        public Guid Id { get; init; }

        public Guid OwnerId { get; init; }

        public String AccountNumber { get; private set; }

        public Currency Currency { get; private set; }

        public String BankLegalName { get; private set; }

        public String? BIK { get; private set; }

        public String? SWIFT { get; private set; }
    }
}

using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Counterparty
    {
        public Guid Id { get; init; }

        public CounterpartyType Type { get; init; }

        public CountryCode CountryCode { get; init; }

        public NonEmptyString FullName { get; private set; }

        public NonEmptyString? FullNameEnglish { get; private set; }

        public NonEmptyString? TaxNumber { get; private set; }

        public NonEmptyString? KPP { get; private set; }

        // IEnumerable for product list
    }
}

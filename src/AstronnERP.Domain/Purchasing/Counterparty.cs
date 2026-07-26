using AstronnERP.Domain.Purchasing.Enums;

namespace AstronnERP.Domain.Purchasing
{
    public class Counterparty
    {
        public Guid Id { get; init; }

        public CounterpartyType Type { get; init; }

        public CountryCode CountryCode { get; init; }

        public String FullName { get; init; } = String.Empty;

        public String? FullNameEnglish { get; init; }

        public String? TaxNumber { get; init; }

        public String? KPP { get; init; }

        // IEnumerable for product list
    }
}

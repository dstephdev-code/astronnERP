using AstronnERP.Domain.Purchasing.Master.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Counterparty
    {
        public Guid Id { get; init; }

        public CounterpartyType Type { get; init; }

        public CountryCode CountryCode { get; init; }

        public string FullName { get; private set; } = String.Empty;

        public string? FullNameEnglish { get; private set; }

        public string? TaxNumber { get; private set; }

        public string? KPP { get; private set; }

        // IEnumerable for product list
    }
}

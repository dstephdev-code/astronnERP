using AstronnERP.Domain.Purchasing.Master.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Counterparty
    {
        public Guid Id { get; init; }

        public CounterpartyType Type { get; init; }

        public CountryCode CountryCode { get; init; }

        public String FullName { get; private set; } = String.Empty;

        public String? FullNameEnglish { get; private set; }

        public String? TaxNumber { get; private set; }

        public String? KPP { get; private set; }

        // IEnumerable for product list
    }
}

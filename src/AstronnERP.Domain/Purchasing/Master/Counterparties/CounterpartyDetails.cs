using AstronnERP.Domain.Purchasing.Master.Enums;

namespace AstronnERP.Domain.Purchasing.Master.Counterparties
{
    public abstract record CounterpartyDetails
    {
        public abstract CountryCode CountryCode { get; init; }
        public abstract CounterpartyType Type { get; init; }
    }
}

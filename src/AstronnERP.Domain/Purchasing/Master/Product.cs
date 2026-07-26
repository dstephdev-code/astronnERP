namespace AstronnERP.Domain.Purchasing.Master
{
    public class Product
    {
        public Guid Id { get; init; }

        public String Name { get; private set; } = String.Empty;

        public String Code { get; private set; } = String.Empty;

        public bool IsService { get; init; }

        public bool HasSerialNumber { get; init; }
    }
}

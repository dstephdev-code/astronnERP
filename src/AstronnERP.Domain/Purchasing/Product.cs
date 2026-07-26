namespace AstronnERP.Domain.Purchasing
{
    public class Product
    {
        public Guid Id { get; init; }

        public String Name { get; init; } = String.Empty;

        public String Code { get; init; } = String.Empty;

        public bool IsService { get; init; }

        public bool HasSerialNumber { get; init; }
    }
}

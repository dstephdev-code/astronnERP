namespace AstronnERP.Domain.Purchasing.Master
{
    public class Product
    {
        public Guid Id { get; init; }

        public string Name { get; private set; } = String.Empty;

        public string Code { get; private set; } = String.Empty;

        public bool IsService { get; init; }

        public bool HasSerialNumber { get; init; }
    }
}

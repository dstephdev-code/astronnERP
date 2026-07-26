using AstronnERP.Domain.Purchasing.Enums;

namespace AstronnERP.Domain.Purchasing
{
    public class InstanceProduct
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public String SerialNumber { get; init; } = String.Empty;

        public InstanceStatus Status { get; init; }

        public Guid? WarehouseId { get; init; }

        public Guid? IssuedToId { get; init; }

        public Guid? ParentComponent { get; init; }
    }
}

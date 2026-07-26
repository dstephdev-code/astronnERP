using AstronnERP.Domain.Purchasing.Master.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class InstanceProduct
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public string SerialNumber { get; init; } = String.Empty;

        public InstanceStatus Status { get; private set; }

        public Guid? WarehouseId { get; private set; }

        public Guid? IssuedToId { get; private set; }

        public Guid? ParentComponent { get; private set; }
    }
}

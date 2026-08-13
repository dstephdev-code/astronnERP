using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class InstanceProduct
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public NonEmptyString SerialNumber { get; init; }

        public InstanceStatus Status { get; private set; }

        public Guid? WarehouseId { get; private set; }

        public Guid? IssuedToId { get; private set; }

        public Guid? ParentComponent { get; private set; }
    }
}

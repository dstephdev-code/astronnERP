using AstronnERP.Domain.Purchasing.Master.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

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

        private InstanceProduct(Guid productId, NonEmptyString serialNumber, Guid warehouseId)
        {
            Id = Guid.CreateVersion7();
            ProductId = productId;
            SerialNumber = serialNumber;
            Status = InstanceStatus.InReceiving;
            WarehouseId = warehouseId;
            IssuedToId = null;
        }

        public static Result<InstanceProduct> RegisterNew(Product product, string serialNumber, Warehouse warehouse)
        {
            var isSerialProduct = Result.FailIf(!product.HasSerialNumber, "To register serial product it should have valid property.");
            var serialNumberValidationResult = NonEmptyString.Create(serialNumber, nameof(SerialNumber));

            var failureCheck = Result.Merge(isSerialProduct, serialNumberValidationResult);

            if (failureCheck.IsFailed)
                return failureCheck.ToResult();

            return Result.Ok(new InstanceProduct(product.Id, serialNumberValidationResult.Value, warehouse.Id));
        }

        public Result AddToStock()
        {
            if (Status != InstanceStatus.InReceiving)
                return Result.Fail("You can manually add product to the stock only after registration.");

            Status = InstanceStatus.InStock;
            return Result.Ok();
        }

        public Result TransferTo(Warehouse warehouse)
        {
            if (Status != InstanceStatus.InStock)
                return Result.Fail("Can not transfer product if it is not in stock.");

            if (warehouse.Id == WarehouseId)
                return Result.Fail("Can not transfer product to the same warehouse.");

            WarehouseId = warehouse.Id;
            return Result.Ok();
        }

        public Result IssueTo(Employee employee)
        {
            if (Status != InstanceStatus.InStock)
                return Result.Fail("Product should be in stock to issue it.");

            IssuedToId = employee.Id;
            WarehouseId = null;
            return Result.Ok();
        }

        public Result ReturnToStock(Warehouse warehouse)
        {
            if (Status != InstanceStatus.Issued)
                return Result.Fail("You can only return to stock if it is issued.");

            IssuedToId = null;
            WarehouseId = warehouse.Id;
            return Result.Ok();
        }

        public Result ReturnToVendor()
        {
            if (Status != InstanceStatus.InStock)
                return Result.Fail("To return product back to vendor it should be in stock.");

            WarehouseId = null;
            IssuedToId = null;
            Status = InstanceStatus.ReturnedToVendor;
            return Result.Ok();
        }
    }
}

using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master
{
    /* Bounded Context - потом будем подгружать сюда из модуля склада только то что нужно для закупок. */
    public class Warehouse
    {
        public Guid Id { get; init; }

        public NonEmptyString Name { get; private set; }
    }
}

namespace AstronnERP.Domain.Purchasing.Master
{
    /* Bounded Context - потом будем подгружать сюда из модуля склада только то что нужно для закупок. */
    public class Warehouse
    {
        public Guid Id { get; init; }

        public String Name { get; private set; } = String.Empty;
    }
}

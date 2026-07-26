using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.Purchasing.Master
{
    /* Тоже самое. Потом будем подгружать только то что нужно из модуля по отделу кадров. */
    public class Employee
    {
        public Guid Id { get; init; }

        public string Name { get; private set; } = String.Empty;

        public Department Department { get; private set; }
    }
}

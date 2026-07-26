using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.Purchasing
{
    /* Тоже самое. Потом будем подгружать только то что нужно из модуля по отделу кадров. */
    public class Employee
    {
        public Guid Id { get; init; }

        public String Name { get; init; } = String.Empty;

        public Department Department { get; init; }
    }
}

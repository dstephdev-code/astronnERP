using AstronnERP.Domain.SharedObjects.Enums;
using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.Purchasing.Master
{
    /* Тоже самое. Потом будем подгружать только то что нужно из модуля по отделу кадров. */
    public class Employee
    {
        public Guid Id { get; init; }

        public NonEmptyString Name { get; private set; }

        public Department Department { get; private set; }
    }
}

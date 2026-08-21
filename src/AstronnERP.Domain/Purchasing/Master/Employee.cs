using AstronnERP.Domain.SharedObjects.Enums;
using AstronnERP.Domain.SharedObjects.Errors;
using AstronnERP.Domain.SharedObjects.ValueObjects;
using FluentResults;

namespace AstronnERP.Domain.Purchasing.Master
{
    public class Employee
    {
        public Guid Id { get; init; }

        public NonEmptyString Name { get; private set; }

        public Department Department { get; private set; }

        private Employee(NonEmptyString name, Department department)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Department = department;
        }

        public static Result<Employee> AddNew(string name, Department department)
        {
            var nameValidationResult = NonEmptyString.Create(name, nameof(Name));
            var departmentValidationResult = Result.FailIf(!Enum.IsDefined(department), "Department must be of expected list.");

            var failureCheck = Result.Merge(nameValidationResult, departmentValidationResult);

            if (failureCheck.IsFailed)
                return failureCheck.ToResult();

            return Result.Ok(new Employee(nameValidationResult.Value, department));
        }

        public Result ChangeName(string newName)
        {
            var newNameValidationResult = NonEmptyString.Create(newName, nameof(Name));
            var isSameValue = newNameValidationResult.IsSuccess && string.Equals(newNameValidationResult.Value.Value, Name.Value);

            var failureCheck = Result.Merge(
                newNameValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Name)))
            );

            if (failureCheck.IsSuccess)
                Name = newNameValidationResult.Value;

            return failureCheck.ToResult();
        }

        // TODO TransferTo()?
        public Result ChangeDepartment(Department newDepartment) 
        {
            var newDepartmentValidationResult = Result.FailIf(!Enum.IsDefined(newDepartment), "Department must be of expected list.");
            var isSameValue = Department == newDepartment;

            var failureCheck = Result.Merge(
                newDepartmentValidationResult,
                Result.FailIf(isSameValue, new PropertyValueIsTheSame(nameof(Department)))
            );

            if (failureCheck.IsSuccess)
                Department = newDepartment;

            return failureCheck;
        }
    }
}

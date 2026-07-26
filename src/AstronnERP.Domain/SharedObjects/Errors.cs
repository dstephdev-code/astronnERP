using FluentResults;

namespace AstronnERP.Domain.SharedObjects.Errors
{
    public class PropertyIsEmpty : Error
    {
        public PropertyIsEmpty(string propertyName)
            : base($"{propertyName} property must contain something.")
        {
            Metadata.Add("ErrorCode", "COMMON_PROPERTY_EMPTY");
        }
    }

    public class PropertyValueIsTheSame: Error
    {
        public PropertyValueIsTheSame(string propertyName)
            : base($"New {propertyName} is the same as old.")
        {
            Metadata.Add("ErrorCode", "COMMON_PROPERTY_SAME");
        }
    }
}

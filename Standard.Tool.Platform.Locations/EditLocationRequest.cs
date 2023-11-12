using FluentValidation.Results;
using Standard.Tool.Platform.Locations.Validations;
using System.Collections.Generic;

namespace Standard.Tool.Platform.Locations
{
    public class EditLocationRequest
    {
        public EditLocationRequest(IEnumerable<Location> materialDataList)
        {
            LocationDataList = materialDataList;
        }

        public IEnumerable<Location> LocationDataList { get; private set; }


        public ValidationResult ValidationResult { get; set; }

        public bool IsValid()
        {
            ValidationResult = new CreateLocationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}

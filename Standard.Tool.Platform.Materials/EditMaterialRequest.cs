using FluentValidation.Results;
using Standard.Tool.Platform.Materials.Validations;
using System.Collections.Generic;

namespace Standard.Tool.Platform.Materials;

public class EditMaterialRequest
{
    public EditMaterialRequest(IEnumerable<Material> materialDataList) 
    {
        MaterialDataList = materialDataList;
    }

    public IEnumerable<Material> MaterialDataList { get; private set; }


    public ValidationResult ValidationResult { get; set; }

    public bool IsValid()
    {
        ValidationResult = new CreateMaterialValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

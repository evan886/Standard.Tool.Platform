using System;
using FluentValidation.Results;
using Standard.Tool.Platform.Auth.PermissionFeature.Validations;

namespace Standard.Tool.Platform.Auth.AccountFeature;

public class EditAccountPermissionRequest
{
    public EditAccountPermissionRequest(Guid[] selectedPermissionIds)
    {
        SelectedPermissionIds = selectedPermissionIds;
    }

    public Guid[] SelectedPermissionIds { get; set; }


    public ValidationResult ValidationResult { get; set; }

    public bool IsValid()
    {
        ValidationResult = new UpdateAccountPermissionValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

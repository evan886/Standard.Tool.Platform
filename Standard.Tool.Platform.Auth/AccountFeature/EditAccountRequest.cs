using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Standard.Tool.Platform.Auth.AccountFeature.Validations;

namespace Standard.Tool.Platform.Auth.AccountFeature;

public class EditAccountRequest
{
    public string UserName { get;private set; }

    public string LoginName { get; private set; }


    public ValidationResult ValidationResult { get; set; }

    public bool IsValid()
    {
        ValidationResult = new CreateAccountValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

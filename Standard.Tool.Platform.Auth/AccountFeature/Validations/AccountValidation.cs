using FluentValidation;
using Standard.Tool.Platform.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.AccountFeature.Validations
{
    public abstract class AccountValidation<T> : AbstractValidator<T> where T : EditAccountRequest
    {
        /// <summary>
        /// Account Name Validate
        /// </summary>
        protected void ValidateAccountUserName()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("UserName Is Not Empty")
                .Must(NotHaveSpecialName).WithMessage("The UserName Cannot Contain Special Characters")
                .Length(2, 31).WithMessage("The UserName Contains 2 to 30 characters");
        }

        /// <summary>
        /// Login Name Validate
        /// </summary>
        protected void ValidateAccountLoginName()
        {
            RuleFor(c => c.LoginName)
                .NotEmpty().WithMessage("LoginName Is Not Empty")
                .Must(NotHaveSpecialName).WithMessage("The LoginName Cannot Contain Special Characters")
                .Length(2, 31).WithMessage("The LoginName Contains 2 to 30 characters");
        }

        private static bool NotHaveSpecialName(string name)
        {
            return ValidateHelper.isContainRegx(name);
        }
    }
}

using FluentValidation;
using Standard.Tool.Platform.Auth.AccountFeature;
using Standard.Tool.Platform.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.PermissionFeature.Validations
{
    public abstract class AccountPermissionValidation<T> : AbstractValidator<T> where T : EditAccountPermissionRequest
    {

        //验证集合数量
        protected void ValidateCollectionCount()
        {
            RuleFor(c => c.SelectedPermissionIds)
                .NotEmpty()
                .Must(HaveCollectionCount)//Must 表示必须满足某一个条件，参数是一个bool类型的方法，更像是一个委托事件
                .WithMessage("集合数量必须大于0");
        }

        // 表达式
        protected static bool HaveCollectionCount(Guid[] accountPermission)
        {
            return accountPermission.Length > 0;
        }
    }
}

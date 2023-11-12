using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Locations.Validations
{
    public abstract class LocationValidation<T> : AbstractValidator<T> where T : EditLocationRequest
    {
        //验证集合数量
        protected void ValidateCollectionCount()
        {
            RuleFor(c => c.LocationDataList)
                .NotEmpty()
                .Must(HaveCollectionCount)//Must 表示必须满足某一个条件，参数是一个bool类型的方法，更像是一个委托事件
                .WithMessage("集合数量必须大于0");
        }

        // 表达式
        protected static bool HaveCollectionCount(IEnumerable<Location> locationData)
        {
            return locationData.Count() > 0;
        }
    }
}

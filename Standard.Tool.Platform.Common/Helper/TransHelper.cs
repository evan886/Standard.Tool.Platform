using Standard.Tool.Platform.Common.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Common.Helper
{
    public class TransHelper<TIn, TOut>
    {
        #region 私有
        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            string name = string.Empty;
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                {
                    continue;
                }
                var columnProperty = item.GetCustomAttribute<PropertyAttribute>(true);
                if (columnProperty != null && columnProperty.IsOpenAliasCheck)
                    name = columnProperty.Alias;
                else
                    name = item.Name;
                if (string.IsNullOrEmpty(name))
                    continue;
                if (typeof(TIn).GetProperty(name) == null)
                    continue;
                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
            return lambda.Compile();
        }
        #endregion

        #region 01，对象的转化（并且进行属性值的复制）+static TOut Trans(TIn tIn)
        /// <summary>
        /// 对象的转化
        /// </summary>
        /// <param name="tIn">输入对象</param>
        /// <returns></returns>
        public static TOut TransAutoCopy(TIn tIn)
        {
            return cache(tIn);
        }
        #endregion

        #region 02，集合的转化（并且进行属性值的复制）+static IEnumerable<TOut> TransAutoCopyList(List<TIn> tIn)
        /// <summary>
        /// 集合的转化（并且进行属性值的复制）
        /// </summary>
        /// <param name="tIn">输入集合</param>
        /// <returns></returns>
        public static IEnumerable<TOut> TransAutoCopyList(List<TIn> tIn)
        {
            foreach (var item in tIn)
            {
                yield return TransAutoCopy(item);
            }
        }
        #endregion
    }
}

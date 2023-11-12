using System;
using System.Text.RegularExpressions;

namespace Standard.Tool.Platform.Common.Helper;

public class ValidateHelper
{
    /// <summary>
    ///   不能含有特殊字符(只能为数字,字母和中文)，不能为空
    /// </summary>
    public static bool isContainRegx(String controlString)
    {
        Regex notWholePattern = new Regex(@"^[0-9a-zA-Z\u4e00-\u9fa5]+$");
        return notWholePattern.IsMatch(controlString, 0);
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Entities.Project
{
    [SugarTable("XTAB_DEMO")]
    public class Demo
    {
        [SugarColumn(IsPrimaryKey = true)] //设置主键
        public string DemoName { get; set; }

        public string DemoValue { get; set; }
    }
}

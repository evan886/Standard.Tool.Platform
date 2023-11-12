using System;
using System.ComponentModel;

namespace Standard.Tool.Platform.Locations
{
    public class Location
    {
        [Description("序号")]
        public int No { get; set; }


        [Description("是否选中")]
        public bool IsSelected { get; set; }

        [Description("编码")]
        public string CodeEmpl { get; set; }

        [Description("名称")]
        public string LibEmpl { get; set; }

        public string EntitesStockees { get; set; }

        [Description("总容量: --散货中没有 0")]   
        public string NbEntites { get; set; }
        
        [Description("存储类型: 散货中没有 ''")]
        public string TypEntites { get; set; }
       
        public string CapaTotaleEnP1 { get; set; }
        
        public string CapaRestanteEnP1 { get; set; }
        
        public string CapaReserveeEnP1 { get; set; } = "0";

        [Description("是否散货")]
        public string StockMasse { get; set; }

        [Description("冷藏")]
        public string ChambreFroide { get; set; }

        [Description("安全")]
        public string Stupefiant { get; set; }
        //'0', 高度
        public string HauteurSol { get; set; }
        //' ', 列表
        public string Liste { get; set; } = " ";
        //'Y', 位置签署
        public string Affectable { get; set; }
        //'100', 重量约束 --散货中没有   -1
        public string MasseMax { get; set; }
        //'100', 高度限制 --散货中没有    -1  
        public string HautMax { get; set; }
        //'Y', 单批次     --散货中没有 N
        public string MonoLot { get; set; }

        //'N')状态 N正常 
        public string Type { get; set; } = "N";
    }
}

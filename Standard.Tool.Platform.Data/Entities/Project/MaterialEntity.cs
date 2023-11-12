using SqlSugar;
using Standard.Tool.Platform.Common.AttributeEx;

namespace Standard.Tool.Platform.Data.Entities.Project;

[SugarTable("XFP_ARTICLES")]
public class MaterialEntity
{
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)] //设置主键
    public string CODEART { get; set; }

    /// <summary>
    /// 物料名称
    /// </summary>
    public string? DESIGNPRINCIPALE { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? DESIGNLONGUE { get; internal set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int CLASSEARTICLE { get; set; }

    /// <summary>
    /// 库存单位
    /// </summary>
    public string? UNITESTOCK { get; set; }

    //类型组
    public string? FAMILLE { get; set; }

    //配送工作中心 Y/N
    public string? PESABLECDP { get; set; }

    //配送秤量偏差
    public int? TOLERANCEPESEE { get; set; }
    //制造 Y/N
    public string? PESABLEATELIER { get; set; }
    //制造秤量偏差
    public int? TOLERANCEPESEEATELIER { get; set; }
    //批次混合 Y/N
    public string? MELANGELOT { get; set; }
    //供应商批次混合 Y/N
    public string? MELANGELOTFOURN { get; set; }
    //接收状态Q 检疫/ A 批准/ 条件批准 C/ 测试 E
    public string? STATUTENTREE { get; set; }

    //有效期, 由接口写入
    public int DUREEVALIDITE { get; set; }
    //有效期单位 J代表天,M代表月由接口写入
    public string? UNITEVALIDITE { get; set; }

    //有效期警告
    public int? ALARMEAVPEREMP { get; set; }
    //DLU警告
    public int? ALARMEAVDLU { get; set; }
    //批次结束百分比
    public double? SEUILFINLOT { get; set; }

    //开启后重新计算有效期 Y/N
    public string? COMPUTE_PEREMPT_OPEN_CONT { get; set; }

    //开启有效期
    public int? PEREMPT_DELAY { get; set; }

    //开启有效期单位 D代表天,M代表月
    public string? PEREMPT_UNIT { get; set; }

    //默认抽样数
    public int? QTEPRELEVEE { get; set; }
    //默认抽数单位
    public string? UNITEPRELEVEE { get; set; }

    //起始位置
    public string? EMPLINIT { get; set; }

    //标准工作中心
    public string? POSTECHARGESTD { get; set; }

    [PropertyAttribute(Alias = "Alpha1", IsOpenAliasCheck =true)]
    public string RUBALPHA1 { get; set; }

    [PropertyAttribute(Alias = "Alpha2", IsOpenAliasCheck = true)]
    public string RUBALPHA2 { get; set; }

    [PropertyAttribute(Alias = "Alpha3", IsOpenAliasCheck = true)]
    public string RUBALPHA3 { get; set; }

    [PropertyAttribute(Alias = "Alpha4", IsOpenAliasCheck = true)]
    public string RUBALPHA4 { get; set; }

    [PropertyAttribute(Alias = "Alpha5", IsOpenAliasCheck = true)]
    public string RUBALPHA5 { get; set; }

    [PropertyAttribute(Alias = "Alpha6", IsOpenAliasCheck = true)]
    public string RUBALPHA6 { get; set; }

    [PropertyAttribute(Alias = "Alpha7", IsOpenAliasCheck = true)]
    public string RUBALPHA7 { get; set; }

    [PropertyAttribute(Alias = "Alpha8", IsOpenAliasCheck = true)]
    public string RUBALPHA8 { get; set; }


    [PropertyAttribute(Alias = "Number1", IsOpenAliasCheck = true)]
    public double RUBNUM1 { get; set; }

    [PropertyAttribute(Alias = "Number2", IsOpenAliasCheck = true)]
    public double RUBNUM2 { get; set; }

    [PropertyAttribute(Alias = "Number3", IsOpenAliasCheck = true)]
    public double RUBNUM3 { get; set; }

    [PropertyAttribute(Alias = "Number4", IsOpenAliasCheck = true)]
    public double RUBNUM4 { get; set; }

    [PropertyAttribute(Alias = "Number5", IsOpenAliasCheck = true)]
    public double RUBNUM5 { get; set; }
    [PropertyAttribute(Alias = "Number6", IsOpenAliasCheck = true)]
    public double RUBNUM6 { get; set; }

    [PropertyAttribute(Alias = "Number7", IsOpenAliasCheck = true)]
    public double RUBNUM7 { get; set; }

    [PropertyAttribute(Alias = "Number8", IsOpenAliasCheck = true)]
    public double RUBNUM8 { get; set; }
}

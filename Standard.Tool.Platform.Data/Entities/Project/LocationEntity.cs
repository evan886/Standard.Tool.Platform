
using SqlSugar;
using System.ComponentModel;

namespace Standard.Tool.Platform.Data.Entities.Project;

[SugarTable("XFP_EMPLACEMENTS")]
public class LocationEntity
{
    public string CodeEmpl { get; set; }

    public string LibEmpl { get; set; }

    public string EntitesStockees { get; set; }

    public string NbEntites { get; set; }

    public string TypEntites { get; set; }

    public string CapaTotaleEnP1 { get; set; }

    public string CapaRestanteEnP1 { get; set; }

    public string CapaReserveeEnP1 { get; set; }

    public string StockMasse { get; set; }

    public string ChambreFroide { get; set; }

    public string Stupefiant { get; set; }

    public string HauteurSol { get; set; }

    public string Liste { get; set; }

    public string Affectable { get; set; }

    public string MasseMax { get; set; }

    public string HautMax { get; set; }

    public string MonoLot { get; set; }

    //'N')状态 N正常 
    public string Type { get; set; }
}

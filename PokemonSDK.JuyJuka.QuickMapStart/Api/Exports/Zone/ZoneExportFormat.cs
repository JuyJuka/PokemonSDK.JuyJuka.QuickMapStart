namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Zone
{
  using System;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public class ZoneExportFormat : SingleAssetMapExportFormat
  {
    /*
export const MAP_NAME_TEXT_ID = 200002;
export const MAP_DESCRIPTION_TEXT_ID = 200003;
export const ZONE_DESCRIPTION_TEXT_ID = 100064;
export const ZONE_NAME_TEXT_ID = 100010;
     */
    protected TxtMapExportFormat Description { get; set; } = new TxtMapExportFormat("100064", m => m.Name);
    protected TxtMapExportFormat Name { get; set; } = new TxtMapExportFormat("100010", m => m.ContigousName);

    protected class TxtMapExportFormat : SingleAssetMapExportFormat
    {
      private string fileId = string.Empty;
      private Func<Map, string> toString = m => m.Name;
      public TxtMapExportFormat(string id, Func<Map, string> toString) : base("Data", "Text", "Dialogs", ".csv")
      {
        this.fileId = id;
        this.toString = toString ?? this.toString;
      }

      public override string ModifyTargetFile(Map map, string folder, string file)
      {
        string re = Path.Combine(folder, this.fileId + this.FileExtendsion);
        string[] txtContent = File.ReadAllLines(re);
        this._lid = txtContent.Length
          - Map._1 // 0 based index
          + Map._1 // header, languages
          ;
        return re;
      }

      public override string Export(Map map, string folder, string file, string asset, string config)
      {
        List<string> txtContent = new List<string>(File.ReadAllLines(file));
        char c = config[Map._0];
        string plus = string.Empty + c;
        string plus_ = string.Empty + c;
        foreach (char cc in txtContent[0]) if (cc == c)
          {
            plus += this.toString(map) + c;
            plus_ += c;
          }
        plus = plus.Substring(Map._1);
        plus_ = plus_.Substring(Map._1);
        while (txtContent.Count <= this._lid) txtContent.Add(string.Empty);
        txtContent[this._lid] = plus;
        File.WriteAllLines(file, txtContent);
        return string.Empty;
      }

      public int _lid = Map._0;
    }

    private static string[] _pathAndExtension = ["Data", "Studio", "zones", ".json"];
    public ZoneExportFormat() : base(_pathAndExtension) { }

    public override string ModifyTargetFile(Map map, string folder, string file)
    {
      return Path.Combine(folder, "zone_" + map.Id + this.FileExtendsion);
    }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      string folder2 = file;
      for (int i = Map._0; i < _pathAndExtension.Length; i++) folder2 = Path.GetDirectoryName(folder2) ?? ".";
      string n_d = this.Name.ModifyTargetFolder(map, new PokemonStudioFolder() { Folder = folder2 });
      string n_f = this.Name.ModifyTargetFile(map, n_d, file);
      string d_d = this.Description.ModifyTargetFolder(map, new PokemonStudioFolder() { Folder = folder2 });
      string d_f = this.Description.ModifyTargetFile(map, d_d, file);
      this.Description._lid = this.Name._lid = Math.Max(this.Name._lid, this.Description._lid);
      this.Name.Export(map, n_d, n_f, asset, config);
      this.Description.Export(map, d_d, d_f, asset, config);

      asset = asset.Replace("{{lid}}", string.Empty + this.Name._lid);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      asset = asset.Replace("{{panel}}", map.DefinitivColor.Panel ?? "0");
      return asset;
    }
  }
}
namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.MapLinks
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;

  public class MapLinksExportFormat : SingleAssetMapExportFormat
  {
    public MapLinksExportFormat() : base("Data", "Studio", "maplinks", ".json") { }

    public override string ModifyTargetFile(Map map, string folder, string file) { return Path.Combine(folder, "maplink_" + map.Id + this.FileExtendsion); }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      asset = asset.Replace("{{lid}}", string.Empty + map.Id);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{neid}}", string.Empty + map.IdNorthEast);
      asset = asset.Replace("{{nwid}}", string.Empty + map.IdNorthWest);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{swid}}", string.Empty + map.IdSouthWest);
      asset = asset.Replace("{{seid}}", string.Empty + map.IdSouthEast);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      return asset;
    }
  }
}
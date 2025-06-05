namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Zone
{
  using System;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public class ZoneExportFormat : SingleAssetMapExportFormat
  {
    public ZoneExportFormat() : base("Data", "Studio", "zones", ".json") { }

    public override string ModifyTargetFile(Map map, IPokemonStudioFolder project, string folder, string file)
    {
      return Path.Combine(folder, "zone_" + map.Id + this.FileExtendsion);
    }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, string asset, string config)
    {
      int _lid = Math.Max(StaticId.ZoneName.GuessFor(project, map, true), StaticId.ZoneDescription.GuessFor(project, map, true));
      PokemonStudioId.Cache[StaticId.ZoneName.Id][map] = _lid;
      PokemonStudioId.Cache[StaticId.ZoneDescription.Id][map] = _lid;
      StaticId.ZoneName.WriteText(project, map, true, map.ContigousName);
      StaticId.ZoneDescription.WriteText(project, map, true, map.Name + map.Description);
      asset = asset.Replace("{{lid}}", string.Empty + _lid);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      asset = asset.Replace("{{panel}}", map.DefinitivColor.Panel ?? (string.Empty + Knowen.Nothing));
      asset = asset.Replace("{{g-0}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_Surfing_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-1}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_Surfing_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-2}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_OldRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-3}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_OldRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-4}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_GoodRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-5}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_GoodRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-6}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_SuperRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-7}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_SuperRod_Ocean_GroupExport>(map), true));
      asset = asset.Replace("{{g-8}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_Grass_GroupExport>(map), true));
      asset = asset.Replace("{{g-9}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_Grass_GroupExport>(map), true));
      asset = asset.Replace("{{g-a}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Day_Sand_GroupExport>(map), true));
      asset = asset.Replace("{{g-b}}", string.Empty + StaticId.GroupName.GuessFor(project, GroupExport.GuessObject<Night_Sand_GroupExport>(map), true));
      return asset;
    }
  }
}
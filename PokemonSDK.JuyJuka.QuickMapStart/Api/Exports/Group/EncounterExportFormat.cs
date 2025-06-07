namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Group
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public class EncounterExportFormat : SingleAssetMapExportFormat
  {
    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, string asset, string config)
    {
      string re = string.Empty;
      if (map?.Specis != null)
        foreach (KeyValuePair<Habitats.Habitat, string[]> o in map.Specis)
          if (o.Value != null)
            foreach (string p in o.Value)
            {
              string pp = asset;
              pp = pp.Replace("{{p}}", p);
              pp = pp.Replace("[ [ \"minimumLevel\" ] ]", string.Empty + Math.Max(1, Math.Min(100, map.DifficultyMin)));
              pp = pp.Replace("[ [ \"maximumLevel\" ] ]", string.Empty + Math.Max(1, Math.Min(100, map.DifficultyMax)));
              pp = pp.Replace("[ [ \"randomEncounterChance\" ] ]", string.Empty + 100);
              re += (re.Length < Map._1 ? string.Empty : config) + pp;
            }
      return re;
    }
  }
}
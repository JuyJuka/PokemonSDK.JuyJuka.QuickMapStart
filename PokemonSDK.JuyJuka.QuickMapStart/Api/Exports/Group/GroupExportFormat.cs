namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Group
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  using Habitat = Habitats.Habitat;

  public abstract class GroupExport : SingleAssetMapExportFormat
  {
    public static object GuessObject<T>(Map map)
    {
      return GroupExport.GuessObject(map, typeof(T), null);
    }

    private static object GuessObject(Map map, Type t, object o)
    {
      return map.Id + (o == null ? t.Name : o.GetType().Name);
    }

    protected GroupExport(Habitat filter, params Habitat[] filters) : base("Data", "Studio", "groups", ".json")
    {
      this._Style = this.GetType().Name.Replace(typeof(GroupExport).Name, string.Empty);
      this._Filter.Add(filter);
      foreach (Habitat h in filters ?? new Habitat[] { }) this._Filter.Add(h);
    }
    protected GroupExport(string style, Habitat filter, params Habitat[] filters) : this(filter, filters) { this._Style = style; }

    public virtual EncounterExportFormat EncounterExportFormat { get; set; } = new EncounterExportFormat();

    private string _Style;
    private List<Habitat> _Filter = new List<Habitat>();

    public override string ModifyTargetFile(Map map, IPokemonStudioFolder project, string folder, string file)
    {
      object _lid_key = GroupExport.GuessObject(map, null, this);
      int _lid = StaticId.GroupName.GuessFor(project, _lid_key, true);
      StaticId.GroupName.WriteText(project, _lid_key, true, string.Format("{0}-{1}", map.ContigousName, this._Style));
      return Path.Combine(folder, "group_" + _lid + this.FileExtendsion);
    }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      return base.Export(map, new Cheat() { real = project, readAsset = readAsset }, folder, file, readAsset);
    }

    private class Cheat : IPokemonStudioFolder
    {
      public IPokemonStudioFolder real;
      public Func<string, Tuple<string, string>> readAsset;

      public string Folder
      {
        get
        {
          return this.real.Folder;
        }

        set
        {
          this.real.Folder = value;
        }
      }
    }

    public virtual IDictionary<Habitat, string[]> FilterSpecies(Map map, IPokemonStudioFolder project, string folder, string file, string asset, string config)
    {
      Dictionary<Habitat, string[]> re = new Dictionary<Habitat, string[]>();
      var s = map?.Specis;
      if (s != null)
        foreach (Habitat h in this._Filter)
          if (s.TryGetValue(h, out string[]? species))
            re.Add(h, species);
      return re;
    }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, string asset, string config)
    {
      Func<string, Tuple<string, string>> readAsset = null;
      Cheat cheat = project as Cheat;
      if (cheat != null)
      {
        readAsset = cheat.readAsset;
        project = cheat.real;
      }
      object _lid_key = GroupExport.GuessObject(map, null, this);
      int _lid = StaticId.GroupName.GuessFor(project, _lid_key, true);
      var org = map.Specis;
      string enc = string.Empty;
      try
      {
        map.Specis = this.FilterSpecies(map, project, folder, file, asset, config);
        if (readAsset != null) enc = this.EncounterExportFormat.Export(map, project, folder, file, readAsset);
      }
      finally
      {
        map.Specis = org;
      }
      asset = asset.Replace("{{lid}}", string.Empty + _lid);
      asset = asset.Replace("[ [ \"lid\" ] ]", string.Empty + _lid);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("[ [ \"mid\" ] ]", string.Empty + map.Id);
      asset = asset.Replace("{{enc}}", enc);
      asset = asset.Replace("[ [ \"enc\" ] ]", enc);
      asset = asset.Replace("{{systemTag}}", string.Empty + (map.DefinitivColor?.SpecialicedSystemTag ?? "Grass"));
      asset = asset.Replace("[ [ \"systemTag\" ] ]", string.Empty + (map.DefinitivColor?.SpecialicedSystemTag ?? "Grass"));
      return asset;
    }
  }

  public class Day_Surfing_Ocean_GroupExport : GroupExport { public Day_Surfing_Ocean_GroupExport() : base("??-Surf", Habitat.Sea) { } }
  public class Night_Surfing_Ocean_GroupExport : GroupExport { public Night_Surfing_Ocean_GroupExport() : base("??-Surf", Habitat.Sea) { } }
  public class Day_OldRod_Ocean_GroupExport : GroupExport { public Day_OldRod_Ocean_GroupExport() : base("??-OldR.",Habitat.WatersEdge) { } }
  public class Night_OldRod_Ocean_GroupExport : GroupExport { public Night_OldRod_Ocean_GroupExport() : base("??-OldR.", Habitat.WatersEdge) { } }
  public class Day_GoodRod_Ocean_GroupExport : GroupExport { public Day_GoodRod_Ocean_GroupExport() : base("??-GoodR.", Habitat.WatersEdge) { } }
  public class Night_GoodRod_Ocean_GroupExport : GroupExport { public Night_GoodRod_Ocean_GroupExport() : base("??-GoodR.",Habitat.WatersEdge) { } }
  public class Day_SuperRod_Ocean_GroupExport : GroupExport { public Day_SuperRod_Ocean_GroupExport() : base("??-SuperR.", Habitat.WatersEdge) { } }
  public class Night_SuperRod_Ocean_GroupExport : GroupExport { public Night_SuperRod_Ocean_GroupExport() : base("??-SuperR.", Habitat.WatersEdge) { } }
  public class Day_Grass_GroupExport : GroupExport { public Day_Grass_GroupExport() : base("??", Habitat.Grassland, Habitat.Forest, Habitat.Mountain, Habitat.RoughTerrain) { } }
  public class Night_Grass_GroupExport : GroupExport { public Night_Grass_GroupExport() : base("??", Habitat.Cave, Habitat.Rare, Habitat.Urban) { } }
}

/*
 
      string folder = @"C:\Users\nicolasb\Downloads\PSDK\T2\PokemonSDK.JuyJuka.QuickMapStart\tocopy";
      string _json1 = @"{
  ""klass"": ""Group"",
  ""id"": ""{{lid}}"",
  ""dbSymbol"": ""group_{{lid}}"",
  ""encounters"": [ [ [] ] ],
  ""isDoubleBattle"": false,
  ""systemTag"": """; string _json2 = @""",
  ""terrainTag"": 0,
  ""customConditions"": [
    {
      ""type"": ""enabledSwitch"",
      ""value"": "; string _json3 = @",
      ""relationWithPreviousCondition"": ""AND""
    }
  ],
  ""tool"": "; string _json4 = @",
  ""isHordeBattle"": false,
  ""stepsAverage"": 30
}";
      foreach (var tpl in new[] {
 new Tuple<Type,string,string?>(typeof(Day_Surfing_Ocean_GroupExport),"Ocean",null),
 new Tuple<Type,string,string?>(typeof(Night_Surfing_Ocean_GroupExport),"Ocean",null),
 new Tuple<Type,string,string?>(typeof(Day_OldRod_Ocean_GroupExport),"Ocean","OldRod"),
 new Tuple<Type,string,string?>(typeof(Night_OldRod_Ocean_GroupExport),"Ocean","OldRod"),
 new Tuple<Type,string,string?>(typeof(Day_GoodRod_Ocean_GroupExport),"Ocean","GoodRod"),
 new Tuple<Type,string,string?>(typeof(Night_GoodRod_Ocean_GroupExport),"Ocean","GoodRod"),
 new Tuple<Type,string,string?>(typeof(Day_SuperRod_Ocean_GroupExport),"Ocean","SuperRod"),
 new Tuple<Type,string,string?>(typeof(Night_SuperRod_Ocean_GroupExport),"Ocean","SuperRod"),
 new Tuple<Type,string,string?>(typeof(Day_Grass_GroupExport),"Grass",null),
 new Tuple<Type,string,string?>(typeof(Night_Grass_GroupExport),"Grass",null),
 new Tuple<Type,string,string?>(typeof(Day_Sand_GroupExport),"Sand",null),
 new Tuple<Type,string,string?>(typeof(Night_Sand_GroupExport),"Sand",null),
      })
        System.IO.File.WriteAllText(Path.Combine(folder, tpl.Item1.Name), string.Empty
          + _json1 + tpl.Item2
          + _json2 + (tpl.Item1.Name.StartsWith("Night") ? "12" : "11")
          + _json3 + (tpl.Item3 == null ? "null" : "\"" + tpl.Item3 + "\"")
          + _json4);


 */

//public class GroupStyle
//{
//  /*  
//systemTag: GROUP_SYSTEM_TAG_VALIDATOR,
//terrainTag: POSITIVE_OR_ZERO_INT,
//tool: GROUP_TOOL_VALIDATOR.default(null),
//   */
//  /* GROUP_SYSTEM_TAG_VALIDATOR = z.union([
//z.literal('RegularGround'),
//z.literal('Grass'),
//z.literal('TallGrass'),
//z.literal('Cave'),
//z.literal('Mountain'),
//z.literal('Sand'),
//z.literal('Pond'),
//z.literal('Ocean'),
//z.literal('UnderWater'),
//z.literal('Snow'),
//z.literal('Ice'),
//z.literal('HeadButt'),
//  */
//  /*
//  // NACHT
//     "customConditions": [
//  {
//    "type": "enabledSwitch",
//    "value": 12,
//    "relationWithPreviousCondition": "AND"
//  }
//],
//  // TAG
//      {
//    "type": "enabledSwitch",
//    "value": 11,
//    "relationWithPreviousCondition": "AND"
//  }
//   */
//  /*
//   GROUP_TOOL_VALIDATOR = z.union([z.null(), z.literal('OldRod'), z.literal('GoodRod'), z.literal('SuperRod'), z.literal('RockSmash')]);
//   */

//  private static string SystemTag_Grass = "Grass";
//  private static string SystemTag_Sand = "Sand";
//  private static string SystemTag_Sea = "Ocean";
//  private static string?[] Time = ["Day", "Night"];
//  private static string?[] Tool = ["Surfing", "OldRod", "GoodRod", "SuperRod"];

//  private static IEnumerable<string?[]> For(int i, string systemTag)
//  {
//    string[] re = new string[i + Map._1];
//    re[i] = systemTag;
//    yield return re;
//  }
//  private static IEnumerable<string?[]> ForEach(IEnumerable<string?> others, int i, IEnumerable<string?[]> re)
//  {
//    foreach (string?[] o in re)
//      foreach (string? s in others)
//      {
//        o[i] = s;
//        yield return o;
//      }
//  }
//  private static int AddRange(List<GroupStyle>? s, IEnumerable<string?[]>? re)
//  {
//    if (s != null && re != null) foreach (string?[] ss in re) s.Add(new GroupStyle(ss));
//    return Map._0;
//  }

//  static GroupStyle()
//  {
//    List<GroupStyle> list = new List<GroupStyle>();
//    int i = AddRange(null, null);
//    i = AddRange(list, ForEach(Time, i++, ForEach(Tool, i++, For(i++, SystemTag_Sea))));
//    i = AddRange(list, ForEach(Time, i++, For(i++, SystemTag_Grass)));
//    i = AddRange(list, ForEach(Time, i++, For(i++, SystemTag_Sand)));
//    // List<string> l = GroupStyle.Styles.ConvertAll(x => x.ToString()+"_"+nameof(GroupExport));
//    System.Console.WriteLine(list);
//    GroupStyle.Styles = list;
//  }

//  public static readonly List<GroupStyle> Styles;

//  public GroupStyle(string name) { this._name = name; }
//  public GroupStyle(params string?[] name) : this(string.Join('_', name)) { }
//  // public GroupStyle(string systemTag, string dayNight, string tool) : this([systemTag, dayNight, tool]) { }

//  private string _name;

//  public override string ToString()
//  {
//    return this._name;
//  }
//}

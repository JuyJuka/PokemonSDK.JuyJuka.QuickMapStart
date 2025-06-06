namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  using System;
  using System.Collections.Generic;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public class Assignment : IAssignment
  {
    public virtual IDexReader DexReader { get; set; } = new DexReader();
    public virtual IHabitatDataBase HabitatData { get; set; } = new PokemonEssentialsListHabitatDataBase();
    public virtual decimal CommonPerOneUncommon { get; set; } = 0.25m;
    public virtual decimal UncommenPerOneRare { get; set; } = 3.5m;

    private static readonly Habitat[] _Habitats = Enum.GetValues<Habitat>();

    public virtual IEnumerable<Tuple<Map, IDictionary<Habitat, IEnumerable<string>>>> Read(IEnumerable<Map> maps, string dexFile)
    {
      maps = new HashSet<Map>(maps);
      Dictionary<Map, Dictionary<Habitat, List<Tuple<string, Habitat>>>> re2 = new Dictionary<Map, Dictionary<Habitat, List<Tuple<string, Habitat>>>>();
      Dictionary<WorldMap, Dictionary<IDefinitivMapColor, List<Map>>> grouping = new Dictionary<WorldMap, Dictionary<IDefinitivMapColor, List<Map>>>();
      List<Tuple<string, Habitat>> dex = this.ReadDex(dexFile);
      #region grouping
      foreach (Map map in maps)
      {
        if (map?.World == null) continue;
        if (map?.DefinitivColor == null) continue;
        if (!grouping.ContainsKey(map.World)) grouping.Add(map.World, new Dictionary<IDefinitivMapColor, List<Map>>());
        if (!grouping[map.World].ContainsKey(map.DefinitivColor)) grouping[map.World].Add(map.DefinitivColor, new List<Map>());
        grouping[map.World][map.DefinitivColor].Add(map);
        re2.Add(map, new Dictionary<Habitat, List<Tuple<string, Habitat>>>());
        foreach (Habitat h in Assignment._Habitats) re2[map].Add(h, new List<Tuple<string, Habitat>>());
      }
      #endregion grouping
      foreach (var wold in grouping) this.ReadWorld(wold.Value, re2, dex);
      foreach (var kvp in re2)
      {
        Dictionary<Habitat, IEnumerable<string>> re = new Dictionary<Habitat, IEnumerable<string>>();
        foreach (var kvp2 in kvp.Value) re.Add(kvp2.Key, kvp2.Value.ConvertAll(p => p.Item1));
        yield return new Tuple<Map, IDictionary<Habitat, IEnumerable<string>>>(kvp.Key, re);
      }
    }

    private List<Tuple<string, Habitat>> ReadDex(string dexFile)
    {
      List<Tuple<string, Habitat>> dex = new List<Tuple<string, Habitat>>();
      foreach (string p in this.DexReader.Read(dexFile) ?? new string[] { })
      {
        if (this.DexReader.IsBaseForm(dexFile, p) == true)
        {
          dex.Add(new Tuple<string, Habitat>(p, this.HabitatData[p]));
        }
      }
      return dex;
    }

    private decimal MakeSure(decimal input, decimal fallback)
    {
      input = Math.Abs(input);
      if (input <= decimal.Zero) input = fallback;
      return input;
    }

    private decimal ToPercentCount(decimal input, decimal fallback, int total, Assignment forFallback)
    {
      decimal sum = Map._1
        + this.MakeSure(this.UncommenPerOneRare, forFallback.UncommenPerOneRare)
        + this.MakeSure(this.CommonPerOneUncommon, forFallback.CommonPerOneUncommon)
        ;
      return total * (this.MakeSure(input, fallback) / sum);
    }

    private IEnumerable<Map> SortMaps(IEnumerable<Map> maps)
    {
      // more dangerous maps further down
      return maps;
    }

    private void ReadWorld(
      Dictionary<IDefinitivMapColor, List<Map>> grouping,
      Dictionary<Map, Dictionary<Habitat, List<Tuple<string, Habitat>>>> re,
      List<Tuple<string, Habitat>> dex
      )
    {
      // common% duplicated into all maps
      // uncommon% spread over all maps
      // rare% single map
      Assignment forFallback = new Assignment();
      decimal rareCount = this.ToPercentCount(Map._1, Map._1, dex.Count, forFallback);
      decimal uncommonCount = this.ToPercentCount(this.UncommenPerOneRare, forFallback.UncommenPerOneRare, dex.Count, forFallback);
      decimal commonCount = this.ToPercentCount(this.CommonPerOneUncommon, forFallback.CommonPerOneUncommon, dex.Count, forFallback);
      List<Tuple<string, Habitat>> common = new List<Tuple<string, Habitat>>();
      List<Tuple<string, Habitat>> uncommon = new List<Tuple<string, Habitat>>();
      List<Tuple<string, Habitat>> rare = new List<Tuple<string, Habitat>>();
      foreach (var tpl in dex)
        if (common.Count < commonCount)
          common.Add(tpl);
        else if (uncommon.Count < uncommonCount)
          uncommon.Add(tpl);
        else
          rare.Add(tpl);

      // move Habitat.Rare into rare, if possible
      for (int i = Map._0; i < rare.Count; i++)
        if (rare[i].Item2 != Habitat.Rare)
        {
          var tpl = rare[i];
          int replace = common.FindIndex(tpl => tpl.Item2 == Habitat.Rare);
          if (replace >= Map._0)
          {
            rare[i] = common[replace];
            common[replace] = tpl;
          }
          else if ((replace = uncommon.FindIndex(tpl => tpl.Item2 == Habitat.Rare)) >= Map._0)
          {
            rare[i] = common[replace];
            common[replace] = tpl;
          }
        }

      // common% duplicated into all maps
      foreach (var l in grouping.Values)
        foreach (var map in l)
          foreach (Habitat h in Assignment._Habitats)
            re[map][h].AddRange(common.FindAll(p => p.Item2 == h));

      // uncommon% spread over all maps
      int uncommonIndex = Map._0;
      int uncommonIndexMax = Map._0;
      if (uncommon.Count > Map._0)
        while (uncommonIndexMax < uncommon.Count) // make sure that uncommon is traversed at least once
          foreach (var l in grouping.Values)
            foreach (var map in this.SortMaps(l))
              foreach (Habitat h in Assignment._Habitats)
              {
                re[map][uncommon[uncommonIndex].Item2].Add(uncommon[uncommonIndex]);
                uncommonIndexMax++;
                uncommonIndex++;
                if (uncommonIndex >= uncommon.Count) uncommonIndex = Map._0; // here uncommon will be repeated
              }

      //  rare% single map
      int rareIndex = Map._0;
      if (rare.Count > Map._0)
        while (uncommonIndexMax < uncommon.Count)// make sure that rare is traversed at least once
          foreach (var l in grouping.Values)
            foreach (var map in this.SortMaps(l))
              foreach (Habitat h in Assignment._Habitats)
              {
                re[map][rare[rareIndex].Item2].Add(rare[rareIndex]);
                rareIndex++;
              }

      // correction for prefered/important habitats per color?
    }
  }
}
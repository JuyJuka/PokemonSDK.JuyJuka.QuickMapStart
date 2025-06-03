namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;

  public class PokemonStudioId : IPokemonStudioId
  {
    public static Dictionary<string, Dictionary<object, int?>> Cache { get; private set; } = new Dictionary<string, Dictionary<object, int?>>();

    public virtual string Name { get; set; } = string.Empty;

    public virtual string Id { get; set; } = string.Empty;

    public virtual int GuessFor(string folder, object obj, bool withCache)
    {
      int re = Map._0;
      if (!withCache && this.AC(null, this.Id, obj, out re)) return re;

      AC(re,this.Id, obj, out re);
      return re;
    }

    private bool AC(int? set, string id, object obj, out int get)
    {
      get = Map._0;
      if (!PokemonStudioId.Cache.ContainsKey(id)) PokemonStudioId.Cache.Add(id, new Dictionary<object, int?>());
      if (PokemonStudioId.Cache[id] == null) PokemonStudioId.Cache[id] = new Dictionary<object, int?>();
      if (!PokemonStudioId.Cache[id].ContainsKey(obj)) PokemonStudioId.Cache[id].Add(obj, null);
      if (set != null && set.HasValue) PokemonStudioId.Cache[id][obj] = set.Value;
      if (PokemonStudioId.Cache[id][obj] != null && PokemonStudioId.Cache[id][obj].HasValue)
      {
        get = PokemonStudioId.Cache[id][obj].Value;
        return true;
      }
      return false;
    }
  }

}

namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  using System.IO;
  public class MapIdShenanigangsPokemonStudioId : IPokemonStudioId
  {
    public virtual string[] Path { get; private set; }
    public virtual IPokemonStudioId Base { get; private set; }

    public virtual string Name { get { return this.Base.Name; } set { this.Base.Name = value; } }

    public virtual string Id { get { return this.Base.Id; } set { this.Base.Id = value; } }

    public MapIdShenanigangsPokemonStudioId(PokemonStudioId pokemonStudioId, params string[] path)
    {
      if (pokemonStudioId == null) throw new ArgumentNullException(nameof(pokemonStudioId));
      this.Base = pokemonStudioId;
      this.Path = path ?? [];
    }

    private List<string> countedAndIgnored = new List<string>();
    private List<string> countedAndCounted = new List<string>();
    public virtual int GuessFor(IPokemonStudioFolder folder, object obj, bool withCache)
    {
      int re = this.Base.GuessFor(folder, obj, withCache);
      if (re != decimal.Zero) return re;
      // I have no freaking idea how to generate a 
      re = Map._1;
      string f = System.IO.Path.Combine(folder?.Folder ?? PokemonStudioFolder.Fallback, System.IO.Path.Combine(this.Path));
      if (Directory.Exists(f))
        foreach (string file in Directory.GetFiles(f))
        {
          if (countedAndIgnored.Contains(file)) continue;
          if (countedAndCounted.Contains(file)) re++;
          if (countedAndCounted.Contains(file)) continue;
          if (File.ReadAllText(file).Contains("\"tiledFilename\": \"E_X"))
          {
            countedAndIgnored.Add(file);
          }
          else
          {
            countedAndCounted.Add(file);
            re++;
          }
        }
      return re;
    }

    public virtual void WriteText(IPokemonStudioFolder folder, object obj, bool withCache, string text)
    {
      this.Base.WriteText(folder, obj, withCache, text);
    }

    public virtual void WriteText(IPokemonStudioFolder folder, int id, string text)
    {
      this.Base.WriteText(folder, id, text);
    }
  }
}

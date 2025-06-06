namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  using System.IO;
  using System.Text.RegularExpressions;

  public class DexReader : IDexReader
  {
    public virtual string Pattern { get; set; } = "(?<=\"dbSymbol\": \").*(?=\\\")";
    public virtual string BabyPattern { get; set; } = "(?<=\"babyDbSymbol\": \").*(?=\\\")";
    protected string _Folder = "pokemon";
    protected string _FileExtension = ".json";
    public virtual int SkipCount { get; set; } = 1;
    public virtual int StarterCount { get; set; } = 9;

    protected virtual int AllSkips() { return this.SkipCount + this.StarterCount; }

    public virtual IEnumerable<string> Read(string dexFile)
    {
      int index = Map._0 - this.AllSkips();
      foreach (Match m in Regex.Matches(File.ReadAllText(dexFile), this.Pattern))
      {
        index++;
        if (index >= Map._0) yield return m.Value;
      }
    }

    public virtual bool? IsBaseForm(string dexFile, string pokemon)
    {
      string file = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(dexFile)), this._Folder, pokemon + this._FileExtension);
      if (!File.Exists(file)) return null;
      return Regex.Match(File.ReadAllText(file), this.BabyPattern)?.Value == pokemon;
    }
  }
}
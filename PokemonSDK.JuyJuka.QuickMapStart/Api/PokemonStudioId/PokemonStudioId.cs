namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  public class PokemonStudioId : IPokemonStudioId
  {
    public static char Seperator = ',';
    public static string[] FilePath = ["Data", "Text", "Dialogs"];
    public static string FileExtendsion { get; set; } = ".csv";
    public static Dictionary<string, Dictionary<object, int?>> Cache { get; private set; } = new Dictionary<string, Dictionary<object, int?>>();

    public virtual string Name { get; set; } = string.Empty;

    public virtual string Id { get; set; } = string.Empty;

    protected virtual string ToFile(IPokemonStudioFolder folder)
    {
      return Path.Combine(
        folder?.Folder ?? PokemonStudioFolder.Fallback,
        Path.Combine(PokemonStudioId.FilePath),
        this.Id + PokemonStudioId.FileExtendsion
        );
    }

    public virtual int GuessFor(IPokemonStudioFolder folder, object obj, bool withCache)
    {
      int re = Map._0;
      if (withCache && this.AC(null, this.Id, obj, out re)) return re;

      string file = this.ToFile(folder);
      string[] txtContent = [];
      if (File.Exists(file)) txtContent = File.ReadAllLines(file);
      // I have no idea how IDs are created :-(
      re = txtContent.Length - Map._1;
      AC(re, this.Id, obj, out re);
      return re;
    }

    public virtual void WriteText(IPokemonStudioFolder folder, object obj, bool withCache, string text)
    {
      this.WriteText(folder, this.GuessFor(folder, obj, withCache), text);
    }

    public virtual void WriteText(IPokemonStudioFolder folder, int id, string text)
    {
      id++;// I have fucking no idea how IDs work! Fuck it!;
      List<string> txtContent = new List<string>();
      string file = this.ToFile(folder);
      if (File.Exists(file)) txtContent.AddRange(File.ReadAllLines(file));
      char c = PokemonStudioId.Seperator;
      string plus = string.Empty + c;
      string plus_ = string.Empty + c;
      foreach (char cc in txtContent[0]) if (cc == c)
        {
          plus += text + c;
          plus_ += c;
        }
      plus = plus.Substring(Map._1);
      plus_ = plus_.Substring(Map._1);
      while (txtContent.Count <= id) txtContent.Add(plus_);
      txtContent[id] = plus;
      File.WriteAllLines(file, txtContent);
    }

    private bool AC(int? set, string id, object obj, out int get)
    {
      get = Map._0;
      if (!PokemonStudioId.Cache.ContainsKey(id)) PokemonStudioId.Cache.Add(id, new Dictionary<object, int?>());
      if (PokemonStudioId.Cache[id] == null) PokemonStudioId.Cache[id] = new Dictionary<object, int?>();
      if (!PokemonStudioId.Cache[id].ContainsKey(obj)) PokemonStudioId.Cache[id].Add(obj, null);
      if (set != null && set.HasValue) PokemonStudioId.Cache[id][obj] = set.Value;
      set = PokemonStudioId.Cache[id][obj];
      if (set != null && set.HasValue)
      {
        get = set.Value;
        return true;
      }
      return false;
    }
  }

}

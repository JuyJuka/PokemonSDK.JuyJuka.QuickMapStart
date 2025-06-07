namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public class HabitatDataBase : IHabitatDataBase
  {
    public virtual string Configuration { get; set; } = ".\\HabitatDataBase.txt";
    private string? _Configuration = null;
    private Dictionary<string, Habitat> _Data = new Dictionary<string, Habitat>();
    public virtual Habitat this[string pokemon]
    {
      get
      {
        if (this._Configuration != this.Configuration)
        {
          string f = this.Configuration;
          this._Configuration = f;
          string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
          if (!string.IsNullOrEmpty(f) && !Path.IsPathRooted(f)) f = Path.Combine(myFolder, f);
          this._Data.Clear();
          foreach (string line in File.ReadLines(f))
            try
            {
              string[] e = line.Split(";");
              this._Data.Add(e[Map._0], Enum.Parse<Habitat>(e[Map._1]));
            }
            catch
            {
            }
        }
        if (!this._Data.ContainsKey(pokemon)) return Habitat.Rare;
        return this._Data[pokemon];
      }
    }
  }
}
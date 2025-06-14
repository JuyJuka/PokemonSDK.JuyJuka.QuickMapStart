﻿namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public class PokemonEssentialsListHabitatDataBase : IHabitatDataBase
  {
    private Dictionary<string, Habitat> result = null;
    private string _Configuration = ".\\pokemon.txt";

    private List<string> _log_unknownenHabitat = new List<string>();
    private List<string> _log_noHabitat = new List<string>();

    public virtual Habitat this[string pokemon]
    {
      get
      {
        if (this.result == null) this.Configuration = this.Configuration;
        pokemon = (_P1 + (pokemon ?? string.Empty) + _P2).ToLower();
        if (!this.result.ContainsKey(pokemon))
        {
          this._log_unknownenHabitat.Add(pokemon);
          return Habitat.Rare;
        }
        return this.result[pokemon];
      }
    }

    private static string _P1 = "[";
    private static string _P2 = "]";
    private static string _H = "Habitat = ";

    public virtual string Configuration
    {
      get
      {
        return this._Configuration;
      }
      set
      {
        string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
        if (!string.IsNullOrEmpty(value) && !Path.IsPathRooted(value)) value = Path.Combine(myFolder, value);
        this.result = this.result ?? new Dictionary<string, Habitat>();
        bool read = this._Configuration != null;
        this._Configuration = value;
        if (read) this.result.Clear();
        if (read && File.Exists(value))
        {
          string? pokemon = null;
          foreach (string line in File.ReadAllLines(value))
          {
            if (line == null) continue;
            if (line.StartsWith(_P1))
            {
              if (pokemon != null) this._log_noHabitat.Add(pokemon);
              pokemon = line.ToLower();
            }
            if (line.StartsWith(_H) && !string.IsNullOrEmpty(pokemon))
            {
              if (!this.result.ContainsKey(pokemon)) this.result.Add(pokemon, Habitat.Rare);
              if (Enum.TryParse(line.Substring(_H.Length), out Habitat habitat)) this.result[pokemon] = habitat;
              pokemon = null;
            }
          }
        }
      }
    }
  }
}

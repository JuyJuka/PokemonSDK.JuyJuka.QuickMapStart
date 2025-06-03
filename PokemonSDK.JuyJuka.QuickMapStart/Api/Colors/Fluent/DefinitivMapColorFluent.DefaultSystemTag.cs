namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  public sealed partial class DefinitivMapColorFluent
  {
    private DefinitivMapColorFluent DefaultSystemTag(string tag)
    {
      if (!this.DefinitivMapColor._Defaults.ContainsKey(TmxMapExportFormat.LayerS)) this.DefinitivMapColor._Defaults.Add(TmxMapExportFormat.LayerS, tag);
      this.DefinitivMapColor._Defaults[TmxMapExportFormat.LayerS] = tag;
      return this;
    }
    public DefinitivMapColorFluent DefaultSystemTagSea() { return this.DefaultSystemTag(string.Empty + Knowen.SystemTagSea); }
    public DefinitivMapColorFluent DefaultSystemTagSand() { return this.DefaultSystemTag(string.Empty + Knowen.SystemTagSand); }
  }
}
namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  public sealed class DefinitivMapColorFluent
  {
    public static DefinitivMapColorFluent New(string name, Color color, object background)
    {
      return new DefinitivMapColorFluent(name, color, background);
    }
    private DefinitivMapColorFluent(string name, Color color, object background) { this.DefinitivMapColor = new DefinitivMapColor(name, color, background); }

    public DefinitivMapColor DefinitivMapColor { get; private set; }

    private DefinitivMapColorFluent DefaultSystemTag(string tag)
    {
      if (!this.DefinitivMapColor._Defaults.ContainsKey(TmxMapExportFormat.LayerS)) this.DefinitivMapColor._Defaults.Add(TmxMapExportFormat.LayerS, tag);
      this.DefinitivMapColor._Defaults[TmxMapExportFormat.LayerS] = tag;
      return this;
    }
    public DefinitivMapColorFluent DefaultSystemTagSea() { return this.DefaultSystemTag(string.Empty + Knowen.SystemTagSea); }
    public DefinitivMapColorFluent DefaultSystemTagSand() { return this.DefaultSystemTag(string.Empty + Knowen.SystemTagSand); }

    public DefinitivMapColorFluent Border(string differentLayer, Knowen.Border border)
    {
      return this.Border(border, differentLayer);
    }

    public DefinitivMapColorFluent Border(Knowen.Border border, string? differentLayer = null)
    {
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderN, a, border.North, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderNE, a, border.NorthEast, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderNW, a, border.NorthWest, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderS, a, border.South, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderSE, a, border.SouthEast, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderSW, a, border.SouthWest, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderE, a, border.East, differentLayer));
      this.DefinitivMapColor._Functions.Add((a) => this.Border(this.BorderW, a, border.West, differentLayer));
      return this;
    }

    private bool EqualDefinitivColorName(Map p, Func<Map, string> pp)
    {
      string next = pp(p);
      Map? next_ = p?.World?.Maps?.Find(x => x.Name == next);
      return Equals(p?.DefinitivColor, next_?.DefinitivColor);
    }

    public bool BorderN(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X != Map._0
        && functionParameters.Point.X < functionParameters.Map.World.Size.Width - Map._1
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X >= functionParameters.Map.World.Size.Width - Map._1
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        
        ;
    }

    public bool BorderNW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == Map._0
        ;
    }

    public bool BorderNE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        ;
    }

    public bool BorderSW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X == Map._0
        ;
    }

    public bool BorderSE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        ;
    }

    public bool BorderS(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X != Map._0
        && functionParameters.Point.X < functionParameters.Map.World.Size.Width - Map._1
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X >= functionParameters.Map.World.Size.Width - Map._1
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        
        ;
    }

    public bool BorderW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y != Map._0
        && functionParameters.Point.Y < functionParameters.Map.World.Size.Height - Map._1
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y >= functionParameters.Map.World.Size.Height - Map._1
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        
        ;
    }

    public bool BorderE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false ||
        true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        && functionParameters.Point.Y != Map._0
        && functionParameters.Point.Y < functionParameters.Map.World.Size.Height - Map._1
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        && functionParameters.Point.Y == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        
        || true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        && functionParameters.Point.Y >= functionParameters.Map.World.Size.Height - Map._1
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        
        ;
      ;
    }

    private string Border(Func<DefinitivMapColor.FunctionParameters, bool> test, DefinitivMapColor.FunctionParameters a, object? value, string? differentLayer)
    {
      if (!test(a)) return string.Empty;
      if (differentLayer != null && a.LayerName == differentLayer) return string.Empty + value;
      if (differentLayer != null) return string.Empty;
      if (a.LayerName == TmxMapExportFormat.LayerS) return string.Empty + Knowen.Nothing;
      if (a.LayerName != TmxMapExportFormat.Layer1.Item1) return string.Empty;
      return string.Empty + value;
    }

    public DefinitivMapColorFluent Panel(object panel)
    {
      string? tag = string.Empty + panel;
      if (string.IsNullOrWhiteSpace(tag)) tag = null;
      this.DefinitivMapColor.Panel = tag ?? string.Empty + Knowen.Nothing;
      return this;
    }

    public DefinitivMapColorFluent Music(string name)
    {
      this.DefinitivMapColor.MusicName = name;
      return this;
    }
  }
}
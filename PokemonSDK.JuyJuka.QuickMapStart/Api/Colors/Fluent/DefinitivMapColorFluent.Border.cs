namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using System;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  partial class DefinitivMapColorFluent
  {
    public DefinitivMapColorFluent WithBorder(string differentLayer, Knowen.Border border)
    {
      return this.WithBorder(border, differentLayer);
    }

    public DefinitivMapColorFluent WithBorder(Knowen.Border border, string? differentLayer = null)
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

    private bool BorderN(DefinitivMapColor.FunctionParameters functionParameters)
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

    private bool BorderNW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == Map._0
        ;
    }

    private bool BorderNE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        ;
    }

    private bool BorderSW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X == Map._0
        ;
    }

    private bool BorderSE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.Y == functionParameters.Map.World.Size.Height - Map._1
        && functionParameters.Point.X == functionParameters.Map.World.Size.Width - Map._1
        ;
    }

    private bool BorderS(DefinitivMapColor.FunctionParameters functionParameters)
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

    private bool BorderW(DefinitivMapColor.FunctionParameters functionParameters)
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

    private bool BorderE(DefinitivMapColor.FunctionParameters functionParameters)
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
  }
}
namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  public class DefinitivMapColor : IDefinitivMapColor
  {
    public virtual string Panel { get; set; } = string.Empty + Knowen.Nothing;
    public virtual string Name { get; set; } = "";
    public virtual string MusicName { get; set; } = "";
    public virtual Color Color { get; set; } = Color.Transparent;
    public PointF? WorldMapCoordinatsPercentageMin { get; set; } = null;
    public PointF? WorldMapCoordinatsPercentageMax { get; set; } = null;
    public virtual string ColorRGB
    {
      get
      {
        return string.Format("R={0:000} G={1:000} B={2:000}", this.Color.R, this.Color.G, this.Color.B);
      }
    }

    public override string ToString()
    {
      return this.Name ?? base.ToString();
    }

    public DefinitivMapColor(string name, Color color
      , object? default11 = null
      , object? defaultS = null

      , object? default1D1 = null
      , object? defaultP = null
      , object? defaultTT = null
      , object? default3D3 = null
      , object? default33 = null
      , object? default2D2 = null
      , object? default22 = null
      )
    {
      string[] layers = new string[]{
        TmxMapExportFormat.LayerP,
        TmxMapExportFormat.LayerS,
        TmxMapExportFormat.LayerTT,
        TmxMapExportFormat.Layer3.Item2,
        TmxMapExportFormat.Layer3.Item1,
        TmxMapExportFormat.Layer2.Item2,
        TmxMapExportFormat.Layer2.Item1,
        TmxMapExportFormat.Layer1.Item2,
        TmxMapExportFormat.Layer1.Item1,
      };
      string[] values = new string[] {
        string.Empty+defaultP,
        string.Empty+defaultS,
        string.Empty+defaultTT,
        string.Empty+default3D3,
        string.Empty+default33,
        string.Empty+default2D2,
        string.Empty+default22,
        string.Empty+default1D1,
        string.Empty+default11,
      };
      for (int i = Map._0; i < values.Length; i++)
        if (!string.IsNullOrEmpty(values[i]))
          this._Defaults.Add(layers[i], values[i]);
      this.Name = name;
      this.Color = color;
    }

    public class FunctionParameters
    {
      public FunctionParameters(Map p, int layerIndex, string layerName, Point point)
      {
        this.Map = p;
        this.Point = point;
        this.LayerIndex = layerIndex;
        this.LayerName = layerName;
      }
      public Map Map { get; set; }
      public int LayerIndex { get; set; }
      public string LayerName { get; set; }
      public Point Point { get; set; }
    }

    internal protected readonly Dictionary<string, string> _Defaults = new Dictionary<string, string>();
    internal protected readonly List<Func<FunctionParameters, string>> _Functions = new List<Func<FunctionParameters, string>>();
    public virtual string ToLayer(Map m, int layerIndex, string layerName, Point point)
    {
      string re = string.Empty;
      if (this._Defaults.ContainsKey(layerName)) re = this._Defaults[layerName];
      this._Functions.ForEach(x =>
      {
        if (x == null) return;
        string re2 = x(new FunctionParameters(m, layerIndex, layerName, point));
        if (!string.IsNullOrEmpty(re2)) re = re2;
      });
      return string.IsNullOrEmpty(re) ? string.Empty + Knowen.Nothing : re;
    }

    public virtual object Clone()
    {
      DefinitivMapColor re = new DefinitivMapColor(this.Name, this.Color);
      foreach (string k in this._Defaults.Keys) re._Defaults.Add(k, this._Defaults[k]);
      re._Functions.Clear();
      re._Functions.AddRange(this._Functions);
      return re;
    }
  }
}
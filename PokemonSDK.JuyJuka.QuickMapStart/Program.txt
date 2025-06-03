namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;

  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      Application.Run(new Form1());
    }

    public static void CopyFilesRecursively(string sourcePath, string targetPath, ILogger logger)
    {
      //Now Create all of the directories
      foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
      {
        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
      }

      //Copy all the files & Replaces any files with the same name
      foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
      {
        logger.Write(Path.GetFileName(newPath));
        File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
      }
    }
  }

  public interface ILogger
  {
    void Write(string message);
  }

  public class Logger : ILogger
  {
    public virtual void Write(string message)
    {
      System.Console.WriteLine(message);
    }
  }

  public class WorldMap
  {
    public List<Tuple<string, string>> ContigousNames { get; protected set; } = new List<Tuple<string, string>>();
    public ILogger Logger { get; set; } = new Logger();
    public BitMapExportFormat BitMapExportFormat { get; set; } = new BitMapExportFormat();
    public Point Max { get; protected set; } = new Point(16, 16);
    public Size Size { get; protected set; } = new Size(40, 30);
    public int IdOffset { get; set; } = 22;
    public List<Map> Maps { get; protected set; } = new List<Map>();
    public IMapExportFormat[] Formats
    {
      get
      {
        return new IMapExportFormat[] {
          new TmxMapExportFormat(),
          new MapLinksExportFormat(),
          new ZoneExportFormat(),
          this.BitMapExportFormat,
          new MusicMapExportFormat(),
        };
      }
    }

    private List<DefinitivMapColor>? _DefinitivMapColors = null;
    public List<DefinitivMapColor> DefinitivMapColors
    {
      get
      {
        return (this._DefinitivMapColors = (this._DefinitivMapColors ??
          [
            DefinitivMapColor.DefinitivMapColors_Grassland,
            DefinitivMapColor.DefinitivMapColors_Forest,
            // DefinitivMapColor.DefinitivMapColors_WatersEdge,
            DefinitivMapColor.DefinitivMapColors_Sea,
            // DefinitivMapColor.DefinitivMapColors_Cave,
            DefinitivMapColor.DefinitivMapColors_Mountain,
            DefinitivMapColor.DefinitivMapColors_RoughTerrain,
            DefinitivMapColor.DefinitivMapColors_Urban,
            // DefinitivMapColor.DefinitivMapColors_Rare,
          ]
        ));
      }
    }

    public void SkaleImage(string fileName, Point? max = null, Size? size = null)
    {
      this.Logger.Write("Reading world...");
      this.Maps.Clear();
      this.Logger.Write("Sizing map...");
      if (max != null && max.HasValue) this.Max = new Point(Math.Abs(max.Value.X), Math.Abs(max.Value.Y));
      if (size != null && size.HasValue) this.Size = new Size(Math.Abs(size.Value.Width), Math.Abs(size.Value.Height));
      Bitmap world = new Bitmap(Image.FromFile(fileName));
      this.BitMapExportFormat.TinnyImage = BitMapExportFormat.ResizeImage1(world, this.Max.X, this.Max.Y, this.Max.X, this.Max.Y);
      this.BitMapExportFormat.FullImage = BitMapExportFormat.ResizeImage1(world, this.Max.X * this.Size.Width, this.Max.Y * this.Size.Height, this.Max.X * this.Size.Width, this.Max.Y * this.Size.Height);
      this.BitMapExportFormat.OriginalImage = world;
      this.Logger.Write("Sorting maps...");
      foreach (Point p in Map.ForEach(BitMapExportFormat.TinnyImage.Size))
      {
        var m = new Map(this)
        {
          Color = BitMapExportFormat.TinnyImage.GetPixel(p.X, p.Y),
          WorldMapCoordinates = new Point(p.X, p.Y),
        };
        m.Image = this.BitMapExportFormat.Export2(m);
        this.Maps.Add(m);
      }
    }

    public void Expor(string folder)
    {
      foreach (Map map in this.Maps)
      {
        map.World = this;
        map.Export(folder);
      }
    }
  }

  public class Knowen
  {
    public static readonly string MusicGrass = "route 30.ogg";
    public static readonly string MusicUraban = "43 camphrier town.ogg";

    public static readonly object Nothing = 0;
    public static readonly object Grass = 1;
    public static readonly object Water = 151;
    public static readonly object Mointain = 690;
    public static readonly object RoughTerrain = 93;
    public static readonly object Uraban = 239;

    public static readonly object SystemTagSea = 4257 + 21;
    public static readonly object SystemTagGrass = 4257 + 5;
    public static readonly object SystemTagSand = 4257 + 14;
    public static readonly object SystemTagClimb = 4257 + 62;


    public static readonly object PassagesX = 4385 + 15;

    public static readonly object PanelGrass = 1;
    public static readonly object PanelForest = 6;
    public static readonly object PanelSea = 8;
    public static readonly object PanelMointain = 4;
    public static readonly object PanelRoughTerrain = 5;
    public static readonly object PanelUrban = 3;

    /*
    N NE NW
    S SE SW
    E
    W
     */
    public static readonly Knowen.Border BorderSea = new Knowen.Border(
      1 + 148, 1 + 142, 1 + 143,
      1 + 132, 1 + 134, 1 + 135,
      1 + 139,
      1 + 141);
    public static readonly Knowen.Border BorderUrban = new Knowen.Border(
      1 + 230, 1 + 231, 1 + 229,
      1 + 246, 1 + 247, 1 + 245,
      1 + 239,
      1 + 237);
    public static readonly Knowen.Border BorderMountain = new Knowen.Border(
      1 + 681, 1 + 682, 1 + 680,
      1 + 697, 1 + 698, 1 + 696,
      1 + 690,
      1 + 688);

    public class Border
    {
      public Border(params object[] objects)
      {
        int i = Map._0;
        if (objects != null && objects.Length > i) this.North = objects[i++];
        if (objects != null && objects.Length > i) this.NorthEast = objects[i++];
        if (objects != null && objects.Length > i) this.NorthWest = objects[i++];
        if (objects != null && objects.Length > i) this.South = objects[i++];
        if (objects != null && objects.Length > i) this.SouthEast = objects[i++];
        if (objects != null && objects.Length > i) this.SouthWest = objects[i++];
        if (objects != null && objects.Length > i) this.East = objects[i++];
        if (objects != null && objects.Length > i) this.West = objects[i++];
      }

      public readonly object? North = Knowen.Nothing;
      public readonly object? NorthEast = Knowen.Nothing;
      public readonly object? NorthWest = Knowen.Nothing;
      public readonly object? South = Knowen.Nothing;
      public readonly object? SouthEast = Knowen.Nothing;
      public readonly object? SouthWest = Knowen.Nothing;
      public readonly object? East = Knowen.Nothing;
      public readonly object? West = Knowen.Nothing;
    }
  }

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
      return object.Equals(p?.DefinitivColor, next_?.DefinitivColor);
    }

    public bool BorderN(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X != Map._0
        && functionParameters.Point.X < (functionParameters.Map.World.Size.Width - Map._1)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        && functionParameters.Point.Y == Map._0
        && functionParameters.Point.X >= (functionParameters.Map.World.Size.Width - Map._1)
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        )
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
        && functionParameters.Point.X == (functionParameters.Map.World.Size.Width - Map._1)
        ;
    }

    public bool BorderSW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.Y == (functionParameters.Map.World.Size.Height - Map._1)
        && functionParameters.Point.X == Map._0
        ;
    }

    public bool BorderSE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.Y == (functionParameters.Map.World.Size.Height - Map._1)
        && functionParameters.Point.X == (functionParameters.Map.World.Size.Width - Map._1)
        ;
    }

    public bool BorderS(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == (functionParameters.Map.World.Size.Height - Map._1)
        && functionParameters.Point.X != Map._0
        && functionParameters.Point.X < (functionParameters.Map.World.Size.Width - Map._1)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == (functionParameters.Map.World.Size.Height - Map._1)
        && functionParameters.Point.X == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        && functionParameters.Point.Y == (functionParameters.Map.World.Size.Height - Map._1)
        && functionParameters.Point.X >= (functionParameters.Map.World.Size.Width - Map._1)
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        )
        ;
    }

    public bool BorderW(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y != Map._0
        && functionParameters.Point.Y < (functionParameters.Map.World.Size.Height - Map._1)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameWest)
        && functionParameters.Point.X == Map._0
        && functionParameters.Point.Y >= (functionParameters.Map.World.Size.Height - Map._1)
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        )
        ;
    }

    public bool BorderE(DefinitivMapColor.FunctionParameters functionParameters)
    {
      return false ||
        (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == (functionParameters.Map.World.Size.Width - Map._1)
        && functionParameters.Point.Y != Map._0
        && functionParameters.Point.Y < (functionParameters.Map.World.Size.Height - Map._1)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == (functionParameters.Map.World.Size.Width - Map._1)
        && functionParameters.Point.Y == Map._0
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameNorth)
        )
        || (true
        && !this.EqualDefinitivColorName(functionParameters.Map, x => x.NameEast)
        && functionParameters.Point.X == (functionParameters.Map.World.Size.Width - Map._1)
        && functionParameters.Point.Y >= (functionParameters.Map.World.Size.Height - Map._1)
        && this.EqualDefinitivColorName(functionParameters.Map, x => x.NameSouth)
        )
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
      this.DefinitivMapColor.Panel = tag ?? (string.Empty + Knowen.Nothing);
      return this;
    }

    public DefinitivMapColorFluent Music(string name)
    {
      this.DefinitivMapColor.MusicName = name;
      return this;
    }
  }

  public class DefinitivMapColor : ICloneable
  {
    /*
     https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_habitat
1.1	Grassland Pokémon
1.2	Forest Pokémon
1.3	Water's-edge Pokémon
1.4	Sea Pokémon
1.5	Cave Pokémon
1.6	Mountain Pokémon
1.7	Rough-terrain Pokémon
1.8	Urban Pokémon
1.9	Rare Pokémon
     */
    public static DefinitivMapColor DefinitivMapColors_Grassland =
      DefinitivMapColorFluent.New("Grassland", Color.Green, Knowen.Grass)
      .Panel(Knowen.PanelGrass)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    public static DefinitivMapColor DefinitivMapColors_Forest =
      DefinitivMapColorFluent.New("Forest", Color.FromArgb(0, 99, 20), Knowen.Grass)
      .Panel(Knowen.PanelForest)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    // public static DefinitivMapColor DefinitivMapColors_WatersEdge = new DefinitivMapColor("WatersEdge",Color.Blue);
    public static DefinitivMapColor DefinitivMapColors_Sea =
      DefinitivMapColorFluent.New("Sea", Color.FromArgb(77, 105, 245), Knowen.Water)
      .DefaultSystemTagSea()
      .Panel(Knowen.PanelSea)
      .Border(Knowen.BorderSea)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    // public static DefinitivMapColor DefinitivMapColors_Cave = new DefinitivMapColor("Cave",Color.Black);
    public static DefinitivMapColor DefinitivMapColors_Mountain =
      DefinitivMapColorFluent.New("Mountain", Color.FromArgb(125, 125, 140), Knowen.Mointain)
      .Panel(Knowen.PanelMointain)
      .Border(TmxMapExportFormat.Layer1.Item2,Knowen.BorderMountain)
      .Music(Knowen.MusicGrass)
      .Border(TmxMapExportFormat.LayerS, new Knowen.Border(
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb,
        Knowen.SystemTagClimb))
      .Border(TmxMapExportFormat.LayerP, new Knowen.Border(
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX,
        Knowen.PassagesX))
      .DefinitivMapColor;
    public static DefinitivMapColor DefinitivMapColors_RoughTerrain =
      DefinitivMapColorFluent.New("RoughTerrain", Color.Yellow, Knowen.RoughTerrain)
      .DefaultSystemTagSand()
      .Music(Knowen.MusicGrass)
      .Panel(Knowen.PanelRoughTerrain)
      .DefinitivMapColor;
    public static DefinitivMapColor DefinitivMapColors_Urban =
      DefinitivMapColorFluent.New("Urban", Color.LightGray, Knowen.Uraban)
      .Panel(Knowen.PanelUrban)
      .Border(Knowen.BorderUrban)
      .Music(Knowen.MusicUraban)
      .DefinitivMapColor;
    // public static DefinitivMapColor DefinitivMapColors_Rare = new DefinitivMapColor(Color.Red);

    public virtual string Panel { get; set; } = string.Empty + Knowen.Nothing;
    public virtual string Name { get; set; } = "";
    public virtual string MusicName { get; set; } = "";
    public virtual Color Color { get; set; } = Color.Transparent;
    public virtual string ColorRGB
    {
      get
      {
        return string.Format("R={0:000} G={0:000} B={0:000}", this.Color.R, this.Color.G, this.Color.B);
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
      return string.IsNullOrEmpty(re) ? (string.Empty + Knowen.Nothing) : re;
    }

    public object Clone()
    {
      DefinitivMapColor re = new DefinitivMapColor(this.Name, this.Color);
      foreach (string k in this._Defaults.Keys) re._Defaults.Add(k, this._Defaults[k]);
      return re;
    }
  }

  public class Map
  {
    public override string ToString()
    {
      return this.Name + "\t" + this.DefinitivColor?.Name;
    }

    #region Static
    public static readonly int _0 = (int)decimal.Zero;
    public static readonly int _1 = (int)decimal.One;

    public static IEnumerable<Point> ForEach(Size max)
    {
      return Map.ForEach(new Point(max.Width, max.Height));
    }
    public static IEnumerable<Point> ForEach(Point max)
    {
      return Map.ForEach(max.X, max.Y);
    }
    public static IEnumerable<Point> ForEach(int maxx, int maxy)
    {
      for (int x = Map._0; x < maxx; x++)
      {
        for (int y = Map._0; y < maxy; y++)
        {
          yield return new Point(x, y);
        }
      }
    }

    private static int OverflowX(WorldMap w, int x)
    {
      if (x < Map._0) return w.Max.X - Map._1;
      if (x >= w.Max.X) return Map._0;
      return x;
    }
    private static int OverflowY(WorldMap w, int y)
    {
      if (y < Map._0) return w.Max.Y - Map._1;
      if (y >= w.Max.Y) return Map._0;
      return y;
    }
    #endregion Statis

    #region Id
    private int MakeId(WorldMap w, int x, int y)
    {
      return w.IdOffset
        + (Map.OverflowX(w, x) * w.Max.X/*does it need to be Max.Y ?*/)
        + Map.OverflowY(w, y)
        ;
    }

    public virtual int Id { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual int IdNorth { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdNorthEast { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdNorthWest { get { return this.MakeId(this.World, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdSouth { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdSouthEast { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdSouthWest { get { return this.MakeId(this.World, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdWest { get { return this.MakeId(this.World, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual int IdEast { get { return this.MakeId(this.World, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    #endregion Id

    #region Name
    private static string MakeLength(int max, int value)
    {
      max = (string.Empty + max).Length;
      string re = value.ToString();
      while (max > re.Length) re = "0" + re;
      return re;
    }
    private static string MakeName(WorldMap w, bool exterior, int x, int y)
    {
      return string.Empty
        + "" + (exterior ? "E_" : "I_")
        + "X" + Map.MakeLength(w.Max.X, Map.OverflowX(w, x))
        + "Y" + Map.MakeLength(w.Max.Y, Map.OverflowY(w, y))
        ;
    }

    public virtual string Name { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual string NameNorth { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameNorthEast { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameNorthWest { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameSouth { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameSouthEast { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameSouthWest { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameWest { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual string NameEast { get { return Map.MakeName(this.World, this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._0); } }

    public virtual string ContigousName
    {
      get
      {
        // Name List
        List<string> names = new List<string>();
        foreach (var line in this.World.ContigousNames)
          if (!string.IsNullOrEmpty(line.Item2) && line?.Item1 == this.DefinitivColor?.Name)
            names.Add(line.Item2);
        if (names.Count <= Map._0) return this.Name;
        // Contigues
        Map map = this;
        Map next = null;
        do
        {
          next = this.World.Maps.Find(x => x.Name == map.NameNorth);
          if (next != null && next.DefinitivColor == map.DefinitivColor) map = next;
          else next = null;
        } while (next != null && next != this);
        do
        {
          next = this.World.Maps.Find(x => x.Name == map.NameWest);
          if (next != null && next.DefinitivColor == map.DefinitivColor) map = next;
          else next = null;
        } while (next != null && next != this);
        // Name Index
        int index = Map._0;
        foreach (Point p in Map.ForEach(this.World.Max))
        {
          Map point = this.World.Maps.Find(x => x.WorldMapCoordinates == p);
          if (point == map) break;
          if (this.World.Maps.Find(x => x.Name == point.NameNorth).DefinitivColor.Name == point.DefinitivColor.Name) continue;
          if (this.World.Maps.Find(x => x.Name == point.NameWest).DefinitivColor.Name == point.DefinitivColor.Name) continue;
          if (point?.DefinitivColor == map.DefinitivColor) index++;
        }
        // not enough
        if (names.Count <= index) return this.Name;
        else return names[index];
      }
    }
    #endregion Name

    #region Property - World
    public Map(WorldMap world)
    {
      this.World = world;
    }
    private WorldMap _World;
    public virtual WorldMap World
    {
      get { return _World; }
      set
      {
        if (value == null) throw new ArgumentNullException(nameof(this.World));
        this._World = value;
      }
    }
    #endregion Property - World

    public virtual string Description { get; set; } = "{{}}";
    public virtual bool IsExterior { get; protected set; } = true;
    public virtual Point WorldMapCoordinates { get; set; } = new Point();
    public virtual Bitmap? Image { get; set; } = null;

    public Color? Color { get; set; } = null;
    private Color? _color = null;
    private DefinitivMapColor _DefinitivColor = null;
    public virtual DefinitivMapColor DefinitivColor
    {
      get
      {
        if (this._color != this.Color) this._DefinitivColor = this.EstimateColor(this.Color);
        return this._DefinitivColor;
      }
    }

    private DefinitivMapColor EstimateColor(Color? color)
    {
      DefinitivMapColor? fallback = null;
      DefinitivMapColor? re = null;
      foreach (DefinitivMapColor dColor in this.World.DefinitivMapColors) fallback = fallback ?? dColor;
      if (color != null && color.HasValue && re == null)
        foreach (DefinitivMapColor dColor in this.World.DefinitivMapColors)
          if (dColor?.Color != null && dColor.Color.R == color.Value.R && dColor.Color.G == color.Value.G && dColor.Color.B == color.Value.B)
            re = dColor;
      if (color != null && color.HasValue && re == null)
      {
        float hueDiff = float.MaxValue;
        DefinitivMapColor? reTollerenace = null;
        foreach (DefinitivMapColor dColor in this.World.DefinitivMapColors)
        {
          float dif = Math.Abs(dColor.Color.GetHue() - color.Value.GetHue());
          if (hueDiff > dif)
          {
            hueDiff = dif;
            reTollerenace = dColor;
          }
        }
        float tollerance = 100;
        if (hueDiff <= tollerance) re = reTollerenace;
      }
      // how to estimate a color?
      return re ?? fallback;
    }

    public virtual void Export(string folder)
    {
      this.World.Logger.Write(this.Name + "...");
      string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
      foreach (IMapExportFormat format in this.World.Formats)
      {
        this.World.Logger.Write(this.Name + ".." + format.FileExtendsion);
        // keep order ModifyTargetFolder -> ModifyTargetFile -> Export
        string f2 = format.ModifyTargetFolder(this, folder);
        string f3 = format.ModifyTargetFile(this, f2, Path.Combine(f2, this.Name + format.FileExtendsion));
        string content = format.Export(this, f2, f3, s => this.ExportStaticsReadAsset(myFolder, s));
        if (string.IsNullOrEmpty(content)) continue;
        if (!Directory.Exists(f2)) Directory.CreateDirectory(f2);
        if (!string.IsNullOrEmpty(format.StaticFilter)) this.ExportStatics(myFolder, format.StaticFilter, f2);
        if (File.Exists(f3)) File.Delete(f3);
        File.WriteAllText(f3, content);
      }
    }

    private Tuple<string, string> ExportStaticsReadAsset(string myFolder, string name)
    {
      foreach (string file in Directory.GetFiles(myFolder))
      {
        string re = Path.GetFileNameWithoutExtension(file);
        if (!re.StartsWith(name)) continue;
        string re2 = File.ReadAllText(file);
        re = re.Replace(name, string.Empty);
        return Tuple.Create(re, re2);
      }
      return new Tuple<string, string>(string.Empty, string.Empty);
    }

    private void ExportStatics(string myFolder, string filter, string folder)
    {
      foreach (string file in Directory.GetFiles(myFolder))
      {
        string name = Path.GetFileName(file);
        if (!name.StartsWith(filter)) continue;
        name = name.Substring(filter.Length);
        name = Path.Combine(folder, name);
        if (File.Exists(name)) File.Delete(name);
        File.Copy(file, name);
      }
    }
  }

  public interface IMapExportFormat
  {
    string FileExtendsion { get; }
    string StaticFilter { get; }
    string ModifyTargetFolder(Map map, string folder);
    string ModifyTargetFile(Map map, string folder, string file);
    string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset);
  }

  public abstract class MapExportFormat : IMapExportFormat
  {
    public MapExportFormat() { }
    public MapExportFormat(params string[] pathAndExtension)
    {
      if (pathAndExtension != null && pathAndExtension.Length > Map._0)
      {
        this.FileExtendsion = pathAndExtension[pathAndExtension.Length - Map._1];
      }
      if (pathAndExtension != null && pathAndExtension.Length > Map._1)
      {
        this._mod = new string[pathAndExtension.Length - Map._1];
        for (int i = 0; i < this._mod.Length; i++) this._mod[i] = pathAndExtension[i];
      }
    }

    public virtual string FileExtendsion { get; set; } = ".txt";
    public virtual string StaticFilter { get; set; } = string.Empty;
    private string[] _mod = null;
    public virtual string ModifyTargetFolder(Map map, string folder)
    {
      if (this._mod != null && this._mod.Length > Map._0)
      {
        return Path.Combine(folder, Path.Combine(this._mod));
      }
      else
      {
        return folder;
      }
    }
    public virtual string ModifyTargetFile(Map map, string folder, string file) { return file; }

    public abstract string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset);
  }

  public abstract class SingleAssetMapExportFormat : MapExportFormat
  {
    public SingleAssetMapExportFormat() : base() { }
    public SingleAssetMapExportFormat(params string[] pathAndExtension) : base(pathAndExtension) { }

    public override string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      Tuple<string, string> a = readAsset(this.GetType().Name);
      return this.Export(map, folder, file, a.Item2, a.Item1);
    }

    public abstract string Export(Map map, string folder, string file, string asset, string config);
  }

  public class TmxMapExportFormat : SingleAssetMapExportFormat
  {
    private static Tuple<string, string> X(string a, string b) { return new Tuple<string, string>(a, b); }
    public static readonly string LayerP = "{{p}}";
    public static readonly string LayerS = "{{s}}";
    public static readonly string LayerTT = "{{tt}}";
    public static readonly Tuple<string, string> Layer3 = TmxMapExportFormat.X("{{3_3}}", "{{3_d_3}}");
    public static readonly Tuple<string, string> Layer2 = TmxMapExportFormat.X("{{2_2}}", "{{2_d_2}}");
    public static readonly Tuple<string, string>  Layer1 = TmxMapExportFormat.X("{{1_1}}", "{{1_d_1}}");

    public TmxMapExportFormat() : base(".tmx") { this.StaticFilter = "_tiled"; }

    public override string ModifyTargetFolder(Map map, string folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder), Path.GetFileName(folder) + this.StaticFilter); }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      //Draw(map, folder, file, asset, config);
      string re = asset;
      string[] layers = [
        TmxMapExportFormat.LayerP,
        TmxMapExportFormat.LayerS,
        TmxMapExportFormat.LayerTT,
        TmxMapExportFormat.Layer3.Item2,
        TmxMapExportFormat.Layer3.Item1,
        TmxMapExportFormat.Layer2.Item2,
        TmxMapExportFormat.Layer2.Item1,
        TmxMapExportFormat.Layer1.Item2,
        TmxMapExportFormat.Layer1.Item1,
      ];
      string[] csvs = new string[layers.Length];

      for (int y = 0; y < map.World.Size.Height; y++)
      {
        for (int x = 0; x < map.World.Size.Width; x++)
        {
          for (int i = 0; i < layers.Length; i++)
          {
            csvs[i] += (config + map.DefinitivColor.ToLayer(map, i, layers[i], new Point(x, y)));
            if (x == map.World.Size.Width - Map._1) csvs[i] += Environment.NewLine;
          }
        }
      }
      for (int i = 0; i < layers.Length; i++)
      {
        re = re.Replace(layers[i], (csvs[i].Length > Map._0 ? (csvs[i].Substring(Map._1)) : (csvs[i])));
      }
      return re;
    }
  }

  public class MapLinksExportFormat : SingleAssetMapExportFormat
  {
    public MapLinksExportFormat() : base("Data", "Studio", "maplinks", ".json") { }

    public override string ModifyTargetFile(Map map, string folder, string file) { return Path.Combine(folder, "maplink_" + map.Id + this.FileExtendsion); }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      asset = asset.Replace("{{lid}}", string.Empty + map.Id);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{neid}}", string.Empty + map.IdNorthEast);
      asset = asset.Replace("{{nwid}}", string.Empty + map.IdNorthWest);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{swid}}", string.Empty + map.IdSouthWest);
      asset = asset.Replace("{{seid}}", string.Empty + map.IdSouthEast);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      return asset;
    }
  }

  public class ZoneExportFormat : SingleAssetMapExportFormat
  {
    /*
export const ZONE_DESCRIPTION_TEXT_ID = 100064;
export const ZONE_NAME_TEXT_ID = 100010;
     */
    protected TxtMapExportFormat Description { get; set; } = new TxtMapExportFormat("100064", m => m.Name);
    protected TxtMapExportFormat Name { get; set; } = new TxtMapExportFormat("100010", m => m.ContigousName);

    protected class TxtMapExportFormat : SingleAssetMapExportFormat
    {
      private string fileId = string.Empty;
      private Func<Map, string> toString = m => m.Name;
      public TxtMapExportFormat(string id, Func<Map, string> toString) : base("Data", "Text", "Dialogs", ".csv")
      {
        this.fileId = id;
        this.toString = toString ?? this.toString;
      }

      public override string ModifyTargetFile(Map map, string folder, string file)
      {
        string re = Path.Combine(folder, this.fileId + this.FileExtendsion);
        string[] txtContent = File.ReadAllLines(re);
        this._lid = txtContent.Length
          - Map._1 // 0 based index
          + Map._1 // header, languages
          ;
        return re;
      }

      public override string Export(Map map, string folder, string file, string asset, string config)
      {
        List<string> txtContent = new List<string>(File.ReadAllLines(file));
        char c = config[Map._0];
        string plus = string.Empty + c;
        string plus_ = string.Empty + c;
        foreach (char cc in txtContent[0]) if (cc == c)
          {
            plus += (this.toString(map) + c);
            plus_ += (c);
          }
        plus = plus.Substring(Map._1);
        plus_ = plus_.Substring(Map._1);
        while (txtContent.Count <= this._lid) txtContent.Add(string.Empty);
        txtContent[this._lid] = plus;
        File.WriteAllLines(file, txtContent);
        return string.Empty;
      }

      public int _lid = Map._0;
    }

    private static string[] _pathAndExtension = ["Data", "Studio", "zones", ".json"];
    public ZoneExportFormat() : base(ZoneExportFormat._pathAndExtension) { }

    public override string ModifyTargetFile(Map map, string folder, string file)
    {
      return Path.Combine(folder, "zone_" + map.Id + this.FileExtendsion);
    }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      string folder2 = file;
      for (int i = Map._0; i < ZoneExportFormat._pathAndExtension.Length; i++) folder2 = Path.GetDirectoryName(folder2);
      string n_d = this.Name.ModifyTargetFolder(map, folder2);
      string n_f = this.Name.ModifyTargetFile(map, n_d, file);
      string d_d = this.Description.ModifyTargetFolder(map, folder2);
      string d_f = this.Description.ModifyTargetFile(map, d_d, file);
      this.Description._lid = this.Name._lid = Math.Max(this.Name._lid, this.Description._lid);
      this.Name.Export(map, n_d, n_f, asset, config);
      this.Description.Export(map, d_d, d_f, asset, config);

      asset = asset.Replace("{{lid}}", string.Empty + this.Name._lid);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      asset = asset.Replace("{{panel}}", map.DefinitivColor.Panel ?? "0");
      return asset;
    }
  }

  public class BitMapExportFormat : MapExportFormat
  {
    public BitMapExportFormat() : base(".bmp") { this.StaticFilter = "_bmp"; }

    public override string ModifyTargetFolder(Map map, string folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder), Path.GetFileName(folder) + this.StaticFilter); }

    public override string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      string file_ = Path.Combine(folder, this.StaticFilter + this.FileExtendsion);
      string file__ = Path.Combine(folder, this.StaticFilter + Map._0 + this.FileExtendsion);
      if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
      if (this.FullImage != null && !File.Exists(file_)) this.FullImage.Save(file_);
      if (this.TinnyImage != null && !File.Exists(file__)) this.TinnyImage.Save(file__);
      if (File.Exists(file)) File.Delete(file);
      this.Export2(map).Save(file);
      return string.Empty;
    }

    public virtual Bitmap Export2(Map map)
    {
      Size s = this.Export_Size(map);
      return ResizeImage1(this.FullImage, s.Width, s.Height, s.Width, s.Height
      , map.WorldMapCoordinates.X * s.Width, map.WorldMapCoordinates.Y * s.Height, s.Width, s.Height
      );
    }

    protected virtual Size Export_Size(Map map)
    {
      return map.World.Size;
    }

    public Bitmap? TinnyImage { get; set; }
    public Bitmap? FullImage { get; set; }
    public Bitmap? OriginalImage { get; set; }

    public virtual Bitmap ResizeImage1(Image image, int width, int height, float HorizontalResolution, float VerticalResolution
    , int? srcX = null
      , int? srcY = null
      , int? srcW = null
      , int? srcH = null
      )
    {
      Func<int?, int, int> alt = (x, y) => { if (x != null && x.HasValue) return x.Value; return y; };
      Rectangle destRect = new Rectangle(0, 0, width, height);
      Bitmap destImage = new Bitmap(width, height);
      destImage.SetResolution(HorizontalResolution, VerticalResolution);
      using (var graphics = Graphics.FromImage(destImage))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        using (var wrapMode = new ImageAttributes())
        {
          wrapMode.SetWrapMode(WrapMode.TileFlipXY);
          graphics.DrawImage(image, destRect, alt(srcX, 0), alt(srcY, 0), alt(srcW, image.Width), alt(srcH, image.Height), GraphicsUnit.Pixel, wrapMode);
        }
      }
      return destImage;
    }
  }

  public class MusicMapExportFormat : MapExportFormat
  {
    public MusicMapExportFormat() : base("Data", "Studio", "maps", ".json") { }

    public override string ModifyTargetFile(Map map, string folder, string file)
    {
      if (string.IsNullOrEmpty(map?.DefinitivColor?.MusicName)) return null;
      foreach (string file2 in Directory.GetFiles(folder))
      {
        string content = File.ReadAllText(file2);
        if (true
          && content.Contains("\"tiledFilename\"")
          && content.Contains("\"" + map.Name + "\"")
        )
        {
          return file2;
        }
      }
      return null;
    }

    public override string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      if (string.IsNullOrEmpty(file)) return string.Empty;
      string music = map.DefinitivColor.MusicName;
      string content = File.ReadAllText(file);
      // I do not want to read the JSON ... but perhaps I should.
      const string s1 = "\"bgm\"";
      const string s2 = "}";
      const string s3 = "\"name\": \"";
      const string s4 = "\"";
      int index1 = content.IndexOf(s1);
      if (index1 < 0) return string.Empty;
      int index2 = content.Substring(index1).IndexOf(s2);
      if (index2 < 0) return string.Empty;
      index2 += index1;
      int index3 = content.Substring(index1, index2 - index1).IndexOf(s3);
      if (index3 < 0) return string.Empty;
      index3 += (index1 + s3.Length);
      int index4 = content.Substring(index3).IndexOf(s4);
      if (index4 < 0) return string.Empty;
      index4 += index3;
      content = string.Empty
        + content.Substring(0, index3)
        + music
        + content.Substring(index4, content.Length - index4)
        ;
      File.WriteAllText(file, content);
      return string.Empty;
    }
  }
}
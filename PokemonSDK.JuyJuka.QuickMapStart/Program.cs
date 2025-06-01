namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System;

  using static System.Windows.Forms.DataFormats;

  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      /*
      foreach (var x in DefinitivMapColor.DefinitivMapColors) Console.WriteLine(x.Name + "=" + x.Color.R + "x" + x.Color.G + "x" + x.Color.B);
      Console.In.ReadLine();
// Grassland    =000x128x000
// Forest       =000x100x000
// Sea          =000x000x255
// Mountain     =169x169x169
// RoughTerrain =255x255x000
// Urban        =211x211x211
      */

      string folder = @"C:\Users\nicolasb\Downloads\PSDK\T2\T3";
      List<Map> maps = new List<Map>();
      Bitmap world = new Bitmap(@"C:\Users\nicolasb\Downloads\PSDK\PokemonSDK.JuyJuka.QuickMapStart\world.bmp");
      Map.Max = new Point(world.Width, world.Height);
      foreach (Point p in Map.ForEach(world.Size))
      {
        maps.Add(new Map()
        {
          Color = world.GetPixel(p.X, p.Y),
          WorldMapCoordinates = new Point(p.X, p.Y),
        });
      }
      /*
      PrintList(maps);
      Console.In.ReadLine();
      PrintSquare(maps[17]);
      PrintSquare(maps[16]);
      PrintSquare(maps[00]);
      Console.In.ReadLine();
      */
      foreach (Map map in maps) map.Export(folder);

      //Console.In.ReadLine();
      return;

      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      Application.Run(new Form1());
    }

    private static void PrintList(IEnumerable<Map> maps)
    {
      foreach (var m in maps) System.Console.WriteLine(m.Name + "\t" + m.Id);
    }

    private static void PrintSquare(Map map)
    {
      string blank = new string(' ', map.Name.Length);
      string format = new string('0', map.Name.Length);
      System.Console.Write(blank + "|" + map.NameNorth + "|" + blank);
      System.Console.Write(blank);
      System.Console.Write(blank + "|" + map.IdNorth.ToString(format) + "|" + blank);
      System.Console.WriteLine();
      System.Console.Write(map.NameWest + "|" + map.Name + "|" + map.NameEast);
      System.Console.Write(blank);
      System.Console.Write(map.IdWest.ToString(format) + "|" + map.Id.ToString(format) + "|" + map.IdEast.ToString(format));
      System.Console.WriteLine();
      System.Console.Write(blank + "|" + map.NameSouth + "|" + blank);
      System.Console.Write(blank);
      System.Console.Write(blank + "|" + map.IdSouth.ToString(format) + "|" + blank);
      System.Console.WriteLine();
      System.Console.WriteLine();
    }
  }

  public class Knowen
  {
    public static readonly object Nothing = 0;
    public static readonly object Grass = 1;
    public static readonly object Water = 151;
    public static readonly object Mointain = 690;
    public static readonly object RoughTerrain = 93;
    public static readonly object Uraban = 239;
  }

  public class DefinitivMapColor
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
    public static DefinitivMapColor DefinitivMapColors_Grassland = new DefinitivMapColor("Grassland", Color.Green, Knowen.Grass);
    public static DefinitivMapColor DefinitivMapColors_Forest = new DefinitivMapColor("Forest", Color.DarkGreen, Knowen.Grass);
    // public static DefinitivMapColor DefinitivMapColors_WatersEdge = new DefinitivMapColor("WatersEdge",Color.Blue);
    public static DefinitivMapColor DefinitivMapColors_Sea = new DefinitivMapColor("Sea", Color.Blue, Knowen.Water);
    // public static DefinitivMapColor DefinitivMapColors_Cave = new DefinitivMapColor("Cave",Color.Black);
    public static DefinitivMapColor DefinitivMapColors_Mountain = new DefinitivMapColor("Mountain", Color.DarkGray, Knowen.Mointain);
    public static DefinitivMapColor DefinitivMapColors_RoughTerrain = new DefinitivMapColor("RoughTerrain", Color.Yellow, Knowen.RoughTerrain);
    public static DefinitivMapColor DefinitivMapColors_Urban = new DefinitivMapColor("Urban", Color.LightGray, Knowen.Uraban);
    // public static DefinitivMapColor DefinitivMapColors_Rare = new DefinitivMapColor(Color.Red);

    private static List<DefinitivMapColor>? _DefinitivMapColors = null;
    public static List<DefinitivMapColor> DefinitivMapColors
    {
      get
      {
        return (DefinitivMapColor._DefinitivMapColors = (DefinitivMapColor._DefinitivMapColors ??
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

    public virtual string Name { get; set; } = "";
    public virtual Color Color { get; set; } = Color.Transparent;
    public virtual object Background { get; set; }

    public DefinitivMapColor(string name, Color color, object background)
    {
      this.Name = name;
      this.Color = color;
      this.Background = background;
    }

    public virtual string ToLayer(Point p, string v)
    {
      return string.Empty + Knowen.Nothing;
    }
  }

  public class Map
  {
    #region Static
    public static readonly int _0 = (int)decimal.Zero;
    public static readonly int _1 = (int)decimal.One;
    public static Point Max { get; set; } = new Point(16, 16);
    public static Size Size { get; protected set; } = new Size(40, 30);
    public static int IdOffset { get; set; } = 22;

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

    private static int OverflowX(int x)
    {
      if (x < Map._0) return Map.Max.X - Map._1;
      if (x >= Map.Max.X) return Map._0;
      return x;
    }

    private static int OverflowY(int y)
    {
      if (y < Map._0) return Map.Max.Y - Map._1;
      if (y >= Map.Max.Y) return Map._0;
      return y;
    }
    #endregion Statis

    #region Id
    private int MakeId(int x, int y)
    {
      return Map.IdOffset
        + (Map.OverflowX(x) * Map.Max.X/*does it need to be Max.Y ?*/)
        + Map.OverflowY(y)
        ;
    }

    public virtual int Id { get { return this.MakeId(this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual int IdNorth { get { return this.MakeId(this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdSouth { get { return this.MakeId(this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdWest { get { return this.MakeId(this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual int IdEast { get { return this.MakeId(this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    #endregion Id

    #region Name
    private static string MakeLength(int max, int value)
    {
      max = (string.Empty + max).Length;
      string re = value.ToString();
      while (max > re.Length) re = "0" + re;
      return re;
    }
    private static string MakeName(bool exterior, int x, int y)
    {
      return string.Empty
        + "" + (exterior ? "E_" : "I_")
        + "X" + Map.MakeLength(Map.Max.X, Map.OverflowX(x))
        + "Y" + Map.MakeLength(Map.Max.Y, Map.OverflowY(y))
        ;
    }

    public virtual string Name { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual string NameNorth { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameSouth { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameWest { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual string NameEast { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    #endregion Name

    public virtual string Description { get; set; } = "";
    public virtual bool IsExterior { get; protected set; } = true;
    public virtual Point WorldMapCoordinates { get; set; } = new Point();

    public Color? Color { get; set; } = null;
    private Color? _color = null;
    private DefinitivMapColor _DefinitivColor = null;
    public virtual DefinitivMapColor DefinitivColor
    {
      get
      {
        if (this._color != this.Color) this._DefinitivColor = this.EstimateColor(this.Color, DefinitivMapColor.DefinitivMapColors_Grassland);
        return this._DefinitivColor;
      }
    }

    private DefinitivMapColor EstimateColor(Color? color, DefinitivMapColor fallback)
    {
      if (color != null && color.HasValue)
        foreach (DefinitivMapColor dColor in DefinitivMapColor.DefinitivMapColors)
          if (dColor?.Color != null && dColor.Color.R == color.Value.R && dColor.Color.G == color.Value.G && dColor.Color.B == color.Value.B)
            return dColor;
      // how to estimate a color?
      return fallback;
    }

    public virtual void Export(string folder)
    {
      string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
      foreach (IMapExportFormat format in new IMapExportFormat[] {
        new TmxMapExportFormat(),
        new MapLinksExportFormat(),
        new ZoneExportFormat(),
      })
      {
        string content = format.Export(this, s => this.ExportStaticsReadAsset(myFolder, s));
        if (string.IsNullOrEmpty(content)) continue;
        string f2 = format.ModifyTargetFolder(this, folder);
        if (!Directory.Exists(f2)) Directory.CreateDirectory(f2);
        if (!string.IsNullOrEmpty(format.StaticFilter)) this.ExportStatics(myFolder, format.StaticFilter, f2);
        f2 = format.ModifyTargetFile(this, f2, Path.Combine(f2, this.Name + format.FileExtendsion));
        if (File.Exists(f2)) File.Delete(f2);
        File.WriteAllText(f2, content);
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
    string Export(Map map, Func<string, Tuple<string, string>> readAsset);
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

    public abstract string Export(Map map, Func<string, Tuple<string, string>> readAsset);
  }

  public abstract class SingleAssetMapExportFormat : MapExportFormat
  {
    public SingleAssetMapExportFormat() : base() { }
    public SingleAssetMapExportFormat(params string[] pathAndExtension) : base(pathAndExtension) { }

    public override string Export(Map map, Func<string, Tuple<string, string>> readAsset)
    {
      Tuple<string, string> a = readAsset(this.GetType().Name);
      return this.Export(map, a.Item2, a.Item1);
    }

    public abstract string Export(Map map, string asset, string config);
  }

  public class TmxMapExportFormat : SingleAssetMapExportFormat
  {
    public TmxMapExportFormat() : base(".tmx") { this.StaticFilter = "_tiled"; }

    public override string ModifyTargetFolder(Map map, string folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder), Path.GetFileName(folder) + this.StaticFilter); }

    public override string Export(Map map, string asset, string config)
    {
      string re = asset;
      string[] layers = [
        "{{p}}",
        "{{s}}",
        "{{tt}}",
        "{{3_d_3}}",
        "{{3_3}}",
        "{{2_d_2}}",
        "{{2_2}}",
        "{{1_d_1}}",
        "{{1_1}}",
      ];
      string[] csvs = new string[layers.Length];
      foreach (Point p in Map.ForEach(Map.Size))
      {
        for (int i = 0; i < layers.Length - Map._1; i++)
        {
          csvs[i] += (config + map.DefinitivColor.ToLayer(p, layers[i]));
          if (p.Y == Map.Size.Width - Map._1) csvs[i] += System.Environment.NewLine;
        }
        csvs[layers.Length - Map._1] += (config + map.DefinitivColor.Background);
        if (p.Y == Map.Size.Width - Map._1) csvs[layers.Length - Map._1] += System.Environment.NewLine;
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

    public override string Export(Map map, string asset, string config)
    {
      asset = asset.Replace("{{lid}}", string.Empty + map.Id);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      return asset;
    }
  }

  public class ZoneExportFormat : SingleAssetMapExportFormat
  {
    public ZoneExportFormat() : base("Data", "Studio", "zones", ".json") { }

    public override string ModifyTargetFile(Map map, string folder, string file) { return Path.Combine(folder, "zone_" + map.Id + this.FileExtendsion); }

    public override string Export(Map map, string asset, string config)
    {
      asset = asset.Replace("{{lid}}", string.Empty + map.Id);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      return asset;
    }
  }
}
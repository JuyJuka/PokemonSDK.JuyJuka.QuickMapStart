namespace PokemonSDK.JuyJuka.QuickMapStart
{
  using System;

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
      string empty = @"C:\Users\nicolasb\Downloads\PSDK\T2\PokemonSDK.JuyJuka.QuickMapStart\empty";
      string world_ = @"C:\Users\nicolasb\Downloads\PSDK\T2\PokemonSDK.JuyJuka.QuickMapStart\world.bmp";
      if (!string.IsNullOrEmpty(empty) && Directory.Exists(empty))
      {
        if (!Directory.Exists(folder))
        {
          Program.CopyFilesRecursively(empty, folder);
        }
        else
        {
          System.Console.Out.WriteLine("Use silly JJ empty-project?");
          if (System.Console.In.ReadLine()?.ToLower()?.StartsWith("j") ?? false)
          {
            if (Directory.Exists(folder)) Directory.Delete(folder, true);
            Program.CopyFilesRecursively(empty, folder);
          }
        }
      }
      List<Map> maps = new List<Map>();
      Bitmap world = new Bitmap(world_);
      Map.Max = new Point(world.Width, world.Height);
      foreach (Point p in Map.ForEach(world.Size))
      {
        var m = new Map()
        {
          Color = world.GetPixel(p.X, p.Y),
          WorldMapCoordinates = new Point(p.X, p.Y),
        };
        maps.Add(m);
      }
      // PrintList2(maps);
      /*
      PrintList1(maps);
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

    private static void PrintList1(IEnumerable<Map> maps)
    {
      foreach (var m in maps) System.Console.WriteLine(m.Name + "\t" + m.Id);
    }

    private static void PrintList2(IEnumerable<Map> maps)
    {
      foreach (var m in maps) System.Console.WriteLine(m.Name + "\t" + m.Color.Value.R + "x" + m.Color.Value.G + "x" + m.Color.Value.B + "\t" + m.DefinitivColor.Name);
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

    private static void CopyFilesRecursively(string sourcePath, string targetPath)
    {
      //Now Create all of the directories
      foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
      {
        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
      }

      //Copy all the files & Replaces any files with the same name
      foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
      {
        File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
      }
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

    public static readonly object SystemTagSea = 4257 + 21;
    public static readonly object SystemTagGrass = 4257 + 5;
    public static readonly object SystemTagSand = 4257 + 14;
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
    public static DefinitivMapColor DefinitivMapColors_Sea = new DefinitivMapColor("Sea", Color.Blue, Knowen.Water, Knowen.SystemTagSea);
    // public static DefinitivMapColor DefinitivMapColors_Cave = new DefinitivMapColor("Cave",Color.Black);
    public static DefinitivMapColor DefinitivMapColors_Mountain = new DefinitivMapColor("Mountain", Color.DarkGray, Knowen.Mointain);
    public static DefinitivMapColor DefinitivMapColors_RoughTerrain = new DefinitivMapColor("RoughTerrain", Color.Yellow, Knowen.RoughTerrain, Knowen.SystemTagSand);
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

    protected Dictionary<string, string> _Defaults = new Dictionary<string, string>();
    public virtual string ToLayer(Point p, int layerIndex, string layerName, Point point)
    {
      string re = string.Empty;
      if (this._Defaults.ContainsKey(layerName)) re = this._Defaults[layerName];
      return string.IsNullOrEmpty(re) ? (string.Empty + Knowen.Nothing) : re;
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
    public virtual int IdNorthEast { get { return this.MakeId(this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdNorthWest { get { return this.MakeId(this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual int IdSouth { get { return this.MakeId(this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdSouthEast { get { return this.MakeId(this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual int IdSouthWest { get { return this.MakeId(this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._1); } }
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
    public virtual string NameNorthEast { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameNorthWest { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y - Map._1); } }
    public virtual string NameSouth { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._0, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameSouthEast { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameSouthWest { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._1); } }
    public virtual string NameWest { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X - Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    public virtual string NameEast { get { return Map.MakeName(this.IsExterior, this.WorldMapCoordinates.X + Map._1, this.WorldMapCoordinates.Y + Map._0); } }
    #endregion Name

    public virtual string Description { get; set; } = "{{}}";
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
      DefinitivMapColor? re = null;
      if (color != null && color.HasValue && re == null)
        foreach (DefinitivMapColor dColor in DefinitivMapColor.DefinitivMapColors)
          if (dColor?.Color != null && dColor.Color.R == color.Value.R && dColor.Color.G == color.Value.G && dColor.Color.B == color.Value.B)
            re = dColor;
      if (color != null && color.HasValue && re == null)
      {
        float hueDiff = float.MaxValue;
        DefinitivMapColor? reTollerenace = null;
        foreach (DefinitivMapColor dColor in DefinitivMapColor.DefinitivMapColors)
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
      System.Console.WriteLine(this.Name + "...");
      string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
      foreach (IMapExportFormat format in new IMapExportFormat[] {
        new TmxMapExportFormat(),
        new MapLinksExportFormat(),
        new ZoneExportFormat(),
      })
      {
        System.Console.WriteLine(".." + format.FileExtendsion);
        // keep order ModifyTargetFolder -> ModifyTargetFile -> Export
        string f2 = format.ModifyTargetFolder(this, folder);
        string f3 = format.ModifyTargetFile(this, f2, Path.Combine(f2, this.Name + format.FileExtendsion));
        string content = format.Export(this, s => this.ExportStaticsReadAsset(myFolder, s));
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
    private static Tuple<string, string> X(string a, string b) { return new Tuple<string, string>(a, b); }
    public static readonly string LayerP = "{{p}}";
    public static readonly string LayerS = "{{s}}";
    public static readonly string LayerTT = "{{tt}}";
    public static readonly Tuple<string, string> Layer3 = TmxMapExportFormat.X("{{3_3}}", "{{3_d_3}}");
    public static readonly Tuple<string, string> Layer2 = TmxMapExportFormat.X("{{2_2}}", "{{2_d_2}}");
    public static readonly Tuple<string, string> Layer1 = TmxMapExportFormat.X("{{1_1}}", "{{1_d_1}}");

    public TmxMapExportFormat() : base(".tmx") { this.StaticFilter = "_tiled"; }

    public override string ModifyTargetFolder(Map map, string folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder), Path.GetFileName(folder) + this.StaticFilter); }

    public override string Export(Map map, string asset, string config)
    {
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
      foreach (Point p in Map.ForEach(Map.Size))
      {
        for (int i = 0; i < layers.Length; i++)
        {
          csvs[i] += (config + map.DefinitivColor.ToLayer(p, i, layers[i], p));
          if (p.Y == Map.Size.Width - Map._1) csvs[i] += System.Environment.NewLine;
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

    public override string Export(Map map, string asset, string config)
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
    protected TxtMapExportFormat Description { get; set; } = new TxtMapExportFormat("100064");
    protected TxtMapExportFormat Name { get; set; } = new TxtMapExportFormat("100010");

    protected class TxtMapExportFormat : SingleAssetMapExportFormat
    {
      private string fileId = string.Empty;
      public TxtMapExportFormat(string id) : base("Data", "Text", "Dialogs", ".csv") { this.fileId = id; }

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

      public override string Export(Map map, string asset, string config)
      {
        List<string> txtContent = new List<string>(File.ReadAllLines(this._txt));
        char c = config[Map._0];
        string plus = string.Empty + c;
        string plus_ = string.Empty + c;
        foreach (char cc in txtContent[0]) if (cc == c)
          {
            plus += (map.Name + c);
            plus_ += (c);
          }
        while (txtContent.Count <= this._lid) txtContent.Add(string.Empty);
        txtContent[this._lid] = plus;
        File.WriteAllLines(this._txt, txtContent);
        return string.Empty;
      }

      public string _txt = string.Empty;
      public int _lid = Map._0;
    }

    public ZoneExportFormat() : base("Data", "Studio", "zones", ".json") { }


    public override string ModifyTargetFolder(Map map, string folder)
    {
      this.Name._txt = this.Name.ModifyTargetFile(map, this.Name.ModifyTargetFolder(map, folder), string.Empty);
      this.Description._txt = this.Description.ModifyTargetFile(map, this.Description.ModifyTargetFolder(map, folder), string.Empty);
      this.Description._lid = this.Name._lid = Math.Max(this.Name._lid, this.Description._lid);
      return base.ModifyTargetFolder(map, folder);
    }

    public override string ModifyTargetFile(Map map, string folder, string file)
    {
      return Path.Combine(folder, "zone_" + map.Id + this.FileExtendsion);
    }

    public override string Export(Map map, string asset, string config)
    {
      this.Name.Export(map, asset, config);
      this.Description.Export(map, asset, config);
      asset = asset.Replace("{{lid}}", string.Empty + this.Name._lid);
      asset = asset.Replace("{{mid}}", string.Empty + map.Id);
      asset = asset.Replace("{{nid}}", string.Empty + map.IdNorth);
      asset = asset.Replace("{{eid}}", string.Empty + map.IdEast);
      asset = asset.Replace("{{sid}}", string.Empty + map.IdSouth);
      asset = asset.Replace("{{wid}}", string.Empty + map.IdWest);
      return asset;
    }
  }
}
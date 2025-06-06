namespace PokemonSDK.JuyJuka.QuickMapStart.Api
{
  using System;
  using System.ComponentModel;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

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
      int hadBeen = 22;
      hadBeen = StaticId.MapName.GuessFor(this.World.Project, this.World, false);
      int re = hadBeen
        + (Map.OverflowX(w, x) * w.Max.X/*does it need to be Max.Y ?*/)
        + Map.OverflowY(w, y)
        ;
      if (re < decimal.Zero) throw new SystemException();
      return re;
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
        map = this;
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
    public virtual IDictionary<Habitats.Habitat, string[]>? Specis { get; set; } = null;

    private Dictionary<string, string> _TestOverlapMe = new Dictionary<string, string>();
    public virtual bool TestOverlap(string me, Point[] points)
    {
      if (points == null || points.Length <= Map._0) return true;

      HashSet<string> x = new HashSet<string>();
      List<string> y = new List<string>();
      string taken;

      lock (this._TestOverlapMe)
      {
        foreach (Point p in points)
        {
          string pp = p.X + "x" + p.Y;
          if (!x.Add(pp)) continue;
          if (!this._TestOverlapMe.TryGetValue(pp, out taken)) y.Add(pp);
          else if (taken != me) return false;
        }
        foreach (string pp in y) this._TestOverlapMe.Add(pp, me);
        return true;
      }
    }

    public Color? Color { get; set; } = null;
    public string ColorHue
    {
      get
      {
        if (this.Color == null) return string.Empty;
        if (!this.Color.HasValue) return string.Empty;
        return string.Empty + this.Color.Value.GetHue();
      }
    }

    private Color? _color = null;
    private IDefinitivMapColor _DefinitivColor = null;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public virtual IDefinitivMapColor DefinitivColor
    {
      get
      {
        if (this._color != this.Color)
        {
          var c = this.Color;
          this._DefinitivColor = this.EstimateColor(c);
          this._color = c;
        }
        return this._DefinitivColor;
      }
      set
      {
        this._DefinitivColor = value;
        if (value == null) this._color = null;
      }
    }

    private IDefinitivMapColor EstimateColor(Color? color)
    {
      IColorEstimation? e = this.World?.ColorEstimation;
      if (e == null) throw new SystemException();

      IDefinitivMapColor? fallback = null;
      IDefinitivMapColor? re = null;
      foreach (DefinitivMapColor dColor in this.World?.DefinitivMapColors ?? new List<IDefinitivMapColor>()) fallback = fallback ?? dColor;

      float hueDiff = float.MaxValue;
      if (color != null && color.HasValue)
        foreach (var tuple in e.Estimate(this, color.Value, this?.World?.DefinitivMapColors))
          if (hueDiff > tuple.Item2)
          {
            hueDiff = tuple.Item2;
            re = tuple.Item1;
          }

      return re ?? fallback ?? new DefinitivMapColor(string.Empty, System.Drawing.Color.Transparent, 0, 0);
    }

    public virtual void Export()
    {
      this._TestOverlapMe.Clear();
      string myFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
      foreach (IMapExportFormat format in this.World.Formats)
      {
        if (format == null || !format.IsEnabled) continue;
        this.World.Logger.Write(this.Name + "..." + format.Name);
        // keep order ModifyTargetFolder -> ModifyTargetFile -> Export
        string f2 = format.ModifyTargetFolder(this, this.World.Project);
        string f3 = format.ModifyTargetFile(this, this.World.Project, f2, Path.Combine(f2, this.Name + format.FileExtendsion));
        string content = format.Export(this, this.World.Project, f2, f3, s => this.ExportStaticsReadAsset(myFolder, s));
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
        try
        {
          if (File.Exists(name)) File.Delete(name);
          File.Copy(file, name);
        }
        catch (Exception ex)
        {
          this.World.Logger.Write("Statics might not be up to date.");
          this.World.Logger.Write(string.Empty + ex);
        }
      }
    }
  }
}
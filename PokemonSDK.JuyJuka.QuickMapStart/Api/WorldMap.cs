namespace PokemonSDK.JuyJuka.QuickMapStart.Api
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.BitMap;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Group;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.MapLinks;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Music;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Zone;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Logging;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Wait;

  public class WorldMap
  {
    public WorldMap()
    {
      this.Formats = new IMapExportFormat[] {
          new TmxMapExportFormat(),
          new MapLinksExportFormat(),
          this.BitMapExportFormat,
          new MusicMapExportFormat(),
          new Day_Grass_GroupExport(),
          new Night_Grass_GroupExport(),
          new Day_Surfing_Ocean_GroupExport(),
          new Night_Surfing_Ocean_GroupExport(),
          new Day_OldRod_Ocean_GroupExport(),
          new Night_OldRod_Ocean_GroupExport(),
          new Day_GoodRod_Ocean_GroupExport(),
          new Night_GoodRod_Ocean_GroupExport(),
          new Day_SuperRod_Ocean_GroupExport(),
          new Night_SuperRod_Ocean_GroupExport(),
          new ZoneExportFormat(),
        };
    }
    public virtual IWaiter? Waiter { get; set; } = null;
    public virtual List<Tuple<string, string>> ContigousNames { get; protected set; } = new List<Tuple<string, string>>();
    public virtual ILogger Logger { get; set; } = new Logger();
    public virtual BitMapExportFormat BitMapExportFormat { get; set; } = new BitMapExportFormat();
    public virtual Point Max { get; protected set; } = new Point(16, 16);
    public virtual Size Size { get; protected set; } = new Size(40, 30);
    public virtual List<Map> Maps { get; protected set; } = new List<Map>();
    public virtual IMapExportFormat[] Formats { get; set; } = [];
    public virtual IPokemonStudioFolder Project { get; set; } = new PokemonStudioFolder();

    private IColorEstimation? _ColorEstimation1 = null;
    private IColorEstimation? _ColorEstimation2 = null;
    public virtual IColorEstimation ColorEstimation
    {
      get
      {
        return this._ColorEstimation1 ?? this._ColorEstimation2 ?? (this._ColorEstimation2 = new ColorEstimation());
      }
      set
      {
        this._ColorEstimation1 = value;
      }
    }

    private List<IDefinitivMapColor>? _DefinitivMapColors = null;
    public List<IDefinitivMapColor> DefinitivMapColors
    {
      get
      {
        return (this._DefinitivMapColors = (this._DefinitivMapColors ??
          [
            StaticColors.Grassland,
            StaticColors.Forest,
            StaticColors.Forest_Sothern,
            StaticColors.Sea,
            StaticColors.Mountain,
            StaticColors.Mountain_Snowy,
            StaticColors.RoughTerrain,
            StaticColors.RoughTerrain_Sandy,
            //StaticColors.DefinitivMapColors_Urban_East,
            StaticColors.Urban,
          ]
        ));
      }
    }

    public virtual IAssignment Assignment { get; set; } = new Assignment();

    public virtual void SkaleImage(string mapFileName, string dexFileName, Point? max = null, Size? size = null)
    {
      this.Logger.Write("Reading world...");
      this.Maps.Clear();
      this.Logger.Write("Sizing map...");
      if (max != null && max.HasValue) this.Max = new Point(Math.Abs(max.Value.X), Math.Abs(max.Value.Y));
      if (size != null && size.HasValue) this.Size = new Size(Math.Abs(size.Value.Width), Math.Abs(size.Value.Height));
      Bitmap world = new Bitmap(Image.FromFile(mapFileName));
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
      foreach (var re in this.Assignment.Read(this.Maps, dexFileName))
      {
        if (re?.Item1 == null || re.Item2 == null) continue;
        Dictionary<Habitats.Habitat, string[]> s = new Dictionary<Habitat, string[]>();
        foreach (var kvp in re.Item2) if (kvp.Value != null) s.Add(kvp.Key, new List<string>(kvp.Value).ToArray());
        re.Item1.Specis = s;
      }
    }

    public virtual void Expor()
    {
      this.Expor(null);
    }

    public virtual void Expor(Predicate<Map>? filter)
    {
      List<IMapExportFormat> all = new List<IMapExportFormat>(this.Formats);
      if (filter == null) filter = f => true;
      this.Formats = all.FindAll(x => !x.IsPostPokemonStudioImport).ToArray();
      foreach (Map map in this.Maps.FindAll(filter))
      {
        map.World = this;
        map.Export();
      }
      this.Formats = all.FindAll(x => x.IsPostPokemonStudioImport).ToArray();
      if (this.Waiter != null) this.Waiter.Wait(Wait.WaitFor.PokemonStudioMapImport);
      foreach (Map map in this.Maps.FindAll(filter))
      {
        map.World = this;
        map.Export();
      }
      this.Formats = all.ToArray();
    }
  }
}
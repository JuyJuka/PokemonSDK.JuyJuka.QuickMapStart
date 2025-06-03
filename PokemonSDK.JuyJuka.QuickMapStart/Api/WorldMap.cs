namespace PokemonSDK.JuyJuka.QuickMapStart.Api
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.BitMap;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.MapLinks;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Music;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Zone;
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
          new ZoneExportFormat(),
          this.BitMapExportFormat,
          new MusicMapExportFormat(),
        };
    }
    public virtual IWaiter? Waiter { get; set; } = null;
    public virtual List<Tuple<string, string>> ContigousNames { get; protected set; } = new List<Tuple<string, string>>();
    public virtual ILogger Logger { get; set; } = new Logger();
    public virtual BitMapExportFormat BitMapExportFormat { get; set; } = new BitMapExportFormat();
    public virtual Point Max { get; protected set; } = new Point(8, 8);
    public virtual Size Size { get; protected set; } = new Size(8, 6);
    public virtual List<Map> Maps { get; protected set; } = new List<Map>();
    public virtual IMapExportFormat[] Formats { get; set; } = [];
    public virtual IPokemonStudioFolder Folder { get; set; } = new PokemonStudioFolder();

    private List<DefinitivMapColor>? _DefinitivMapColors = null;
    public List<DefinitivMapColor> DefinitivMapColors
    {
      get
      {
        return (this._DefinitivMapColors = (this._DefinitivMapColors ??
          [
            StaticColors.DefinitivMapColors_Grassland,
            StaticColors.DefinitivMapColors_Forest,
            StaticColors.DefinitivMapColors_Sea,
            StaticColors.DefinitivMapColors_Mountain,
            StaticColors.DefinitivMapColors_RoughTerrain,
            StaticColors.DefinitivMapColors_Urban,
          ]
        ));
      }
    }

    public virtual void SkaleImage(string fileName, Point? max = null, Size? size = null)
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

    public virtual void Expor()
    {
      List<IMapExportFormat> all = new List<IMapExportFormat>(this.Formats);
      this.Formats = all.FindAll(x => !x.IsPostPokemonStudioImport).ToArray();
      foreach (Map map in this.Maps)
      {
        map.World = this;
        map.Export();
      }
      this.Formats = all.FindAll(x => x.IsPostPokemonStudioImport).ToArray();
      if (this.Waiter != null) this.Waiter.Wait(Wait.WaitFor.PokemonStudioMapImport);
      foreach (Map map in this.Maps)
      {
        map.World = this;
        map.Export();
      }
      this.Formats = all.ToArray();
    }
  }
}
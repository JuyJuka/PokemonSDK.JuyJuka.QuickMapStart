namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.BitMap
{
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public class BitMapExportFormat : MapExportFormat
  {
    public BitMapExportFormat() : base(".bmp") { this.StaticFilter = "_bmp"; }

    public override string ModifyTargetFolder(Map map, IPokemonStudioFolder folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder?.Folder), Path.GetFileName(folder?.Folder) + this.StaticFilter); }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset)
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

      if (true
        && alt(srcX, 0) == 0
        && alt(srcY, 0) == 0
        && alt(srcW, width) == image.Width
        && alt(srcH, height) == image.Height
        )
        if (image is Bitmap)
          return (Bitmap)image;
        else
          return new Bitmap(image);

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
}
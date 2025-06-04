namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.Decorator
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public class WorldMapCoordinatsPercentageColorEstimation : IColorEstimation
  {
    public virtual IColorEstimation? Base { get; set; }
    public virtual bool AllowUndefined { get; set; } = false;

    public IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors)
    {
      if (map?.World != null)
      {
        var xPercantage = (map.WorldMapCoordinates.X / (float)map.World.Max.X) * 100;
        var yPercantage = (map.WorldMapCoordinates.Y / (float)map.World.Max.Y) * 100;
        List<IDefinitivMapColor> colors2 = new List<IDefinitivMapColor>();
        foreach (IDefinitivMapColor dColor in colors ?? new IDefinitivMapColor[] { })
        {
          if (dColor == null) continue;
          var min = new NullablePointF(dColor?.WorldMapCoordinatsPercentageMin, p => p.X <= xPercantage && p.Y <= yPercantage);
          var max = new NullablePointF(dColor?.WorldMapCoordinatsPercentageMax, p => p.X >= xPercantage && p.Y >= yPercantage);
          if (!min.HasValue && !max.HasValue) { if(this.AllowUndefined) colors2.Add(dColor); }
          else if (!min.HasValue && max.HasValue && max.IsInRange) colors2.Add(dColor);
          else if (min.HasValue && !max.HasValue && min.IsInRange) colors2.Add(dColor);
          else if (min.HasValue && max.HasValue && min.IsInRange && max.IsInRange) colors2.Add(dColor);
        }
        if (colors2.Count > Map._0) colors = colors2;
      }
      IColorEstimation? b = this.Base;
      return (b == null) ? [] : b.Estimate(map, color, colors);
    }

    private struct NullablePointF
    {
      public NullablePointF(PointF? point, Func<PointF, bool> test)
      {
        this.HasValue = point != null && point.HasValue;
        this.IsInRange = this.HasValue && (test == null || test(point ?? new PointF()));
      }

      public bool HasValue { get; private set; }
      public bool IsInRange { get; private set; }
    }
  }

  public class WorldMapCoordinatsPercentageColorEstimation<T> : WorldMapCoordinatsPercentageColorEstimation
    where T : IColorEstimation, new()
  {
    public WorldMapCoordinatsPercentageColorEstimation() { this.Base = new T(); }
  }
}

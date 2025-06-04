namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api;

  public sealed partial class DefinitivMapColorFluent
  {
    public DefinitivMapColorFluent(FluentNew newColor, DefinitivMapColor color)
    {
      if (newColor == null) throw new ArgumentNullException(nameof(newColor));
      if (color == null) throw new ArgumentNullException(nameof(color));
      this.DefinitivMapColor = color;
    }
    private DefinitivMapColor DefinitivMapColor { get; set; }

    private void DoActions<T>(T obj, params Action<T>[] action)
    {
      if (action != null) foreach (Action<T> action_ in action) if (action_ != null) action_(obj);
    }

    public DefinitivMapColorFluent With(System.Action<DefinitivMapColor> action)
    {
      if (action != null) action(this.DefinitivMapColor);
      return this;
    }

    public DefinitivMapColorFluent WithMapNamePanel(object panel)
    {
      string? tag = string.Empty + panel;
      if (string.IsNullOrWhiteSpace(tag)) tag = null;
      this.DefinitivMapColor.Panel = tag ?? string.Empty + Knowen.Nothing;
      return this;
    }

    public DefinitivMapColorFluent WithMusic(string name)
    {
      this.DefinitivMapColor.MusicName = name;
      return this;
    }

    public DefinitivMapColor AllAroundTheWorld()
    {
      return this.DefinitivMapColor;
    }

    public LimitedToDefinitivMapColorFluent1 LimitedTo()
    {
      // Northern 10% = WorldMapCoordinatsPercentageMax = new PointF(101, 10%);
      // Southern 20% = WorldMapCoordinatsPercentageMin = new PointF(0, 100-20%);
      // Western 40% = WorldMapCoordinatsPercentageMax = new PointF(40%, 101);
      // Eastern 50% = WorldMapCoordinatsPercentageMin = new PointF(100-50%, 0);
      return new LimitedToDefinitivMapColorFluent1(this, this.DefinitivMapColor);
    }

    public sealed class LimitedToDefinitivMapColorFluent1
    {
      private DefinitivMapColorFluent _d;
      private DefinitivMapColor _c;
      public LimitedToDefinitivMapColorFluent1(DefinitivMapColorFluent d, DefinitivMapColor c)
      {
        if (d == null) throw new ArgumentNullException(nameof(d));
        if (c == null) throw new ArgumentNullException(nameof(c));
        this._d = d;
        this._c = c;
      }

      public LimitedToDefinitivMapColorFluent2 Northern(float percent)
      {
        this._c.WorldMapCoordinatsPercentageMax = new PointF(101, percent);
        return new LimitedToDefinitivMapColorFluent2(this._d, this._c);
      }

      public LimitedToDefinitivMapColorFluent2 Southern(float percent)
      {
        this._c.WorldMapCoordinatsPercentageMin = new PointF(0, 100 - percent);
        return new LimitedToDefinitivMapColorFluent2(this._d, this._c);
      }

      public LimitedToDefinitivMapColorFluent3 Eastern(float percent)
      {
        this._c.WorldMapCoordinatsPercentageMax = new PointF(100 - percent, 101);
        return new LimitedToDefinitivMapColorFluent3(this._d, this._c);
      }

      public LimitedToDefinitivMapColorFluent3 Western(float percent)
      {
        this._c.WorldMapCoordinatsPercentageMax = new PointF(percent, 101);
        return new LimitedToDefinitivMapColorFluent3(this._d, this._c);
      }

      public LimitedToDefinitivMapColorFluent3 Central_NS_EW(float northSouth, float eastWest)
      {
        // central 30% = Min=30% && Max<100-30
        this._c.WorldMapCoordinatsPercentageMin = new PointF(eastWest, northSouth);
        this._c.WorldMapCoordinatsPercentageMax = new PointF(100 - eastWest, 100 - northSouth);
        return new LimitedToDefinitivMapColorFluent3(this._d, this._c);
      }
    }

    public sealed class LimitedToDefinitivMapColorFluent2
    {
      private DefinitivMapColorFluent _d;
      private DefinitivMapColor _c;
      public LimitedToDefinitivMapColorFluent2(DefinitivMapColorFluent d, DefinitivMapColor c)
      {
        if (d == null) throw new ArgumentNullException(nameof(d));
        if (c == null) throw new ArgumentNullException(nameof(c));
        this._d = d;
        this._c = c;
      }

      public LimitedToDefinitivMapColorFluent3 AndWestern(float percent)
      {
        float y = 101;
        if (this._c.WorldMapCoordinatsPercentageMax != null && this._c.WorldMapCoordinatsPercentageMax.HasValue)
          y = this._c.WorldMapCoordinatsPercentageMax.Value.Y;
        this._c.WorldMapCoordinatsPercentageMax = new PointF(percent, y);
        return new LimitedToDefinitivMapColorFluent3(this._d, this._c);
      }

      public LimitedToDefinitivMapColorFluent3 AndEastern(float percent)
      {
        float y = 0;
        if (this._c.WorldMapCoordinatsPercentageMin != null && this._c.WorldMapCoordinatsPercentageMin.HasValue)
          y = this._c.WorldMapCoordinatsPercentageMin.Value.Y;
        this._c.WorldMapCoordinatsPercentageMin = new PointF(100 - percent, y);
        return new LimitedToDefinitivMapColorFluent3(this._d, this._c);
      }

      public DefinitivMapColor Percent()
      {
        return this._c;
      }
    }

    public sealed class LimitedToDefinitivMapColorFluent3
    {
      private DefinitivMapColorFluent _d;
      private DefinitivMapColor _c;
      public LimitedToDefinitivMapColorFluent3(DefinitivMapColorFluent d, DefinitivMapColor c)
      {
        if (d == null) throw new ArgumentNullException(nameof(d));
        if (c == null) throw new ArgumentNullException(nameof(c));
        this._d = d;
        this._c = c;
      }

      public DefinitivMapColor Percent()
      {
        return this._c;
      }
    }
  }
}
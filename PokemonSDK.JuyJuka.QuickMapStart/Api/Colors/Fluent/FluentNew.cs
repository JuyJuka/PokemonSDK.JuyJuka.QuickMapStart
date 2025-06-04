namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using System.Drawing;

  public sealed class FluentNew
  {
    public static FluentNewFrom Color(string name)
    {
      return new FluentNewFrom(new FluentNew(), name, System.Drawing.Color.Transparent, null, Map._1, Map._0);
    }
    // public DefinitivMapColor DefinitivMapColor { get; private set; } = new DefinitivMapColor(string.Empty, System.Drawing.Color.Transparent);

    private FluentNew() { }

    public sealed class FluentNewFrom
    {
      private FluentNew _new;
      private string _name;
      private Color _color;
      private object _bg;
      private float _minHue;
      private float _maxHue;
      public FluentNewFrom(FluentNew new_, string name, Color color, object bg, float minHue, float maxHue)
      {
        if (new_ == null) throw new ArgumentNullException(nameof(new_));
        this._new = new_;
        this._name = name;
        this._bg = bg;
        this._color = color;
        this._minHue = minHue;
        this._maxHue = maxHue;
      }

      public FluentNewTo From(float minHue, float maxHue)
      {
        if (this._color == System.Drawing.Color.Transparent) this._color = new XTEst.HSLColor(minHue + ((maxHue - minHue) / 2), 127, 127).ToRGB();
        return new FluentNewTo(this._new, this._name, this._color, this._bg, minHue, maxHue);
      }

      public FluentNewFrom WithUserColor(Color color)
      {
        this._color = color;
        return this;
      }

      public FluentNewFrom WithUserColor(int red, int green, int blue)
      {
        this._color = System.Drawing.Color.FromArgb(red, green, blue);
        return this;
      }

      private FluentNewTo From(Color color)
      {
        return new FluentNewTo(this._new, this._name, color, this._bg, this._minHue, this._maxHue);
      }

      private FluentNewTo FromRgb(int red, int green, int blue)
      {
        return new FluentNewTo(this._new, this._name, System.Drawing.Color.FromArgb(red, green, blue), this._bg, this._minHue, this._maxHue);
      }

    }

    public sealed class FluentNewTo
    {
      private FluentNew _new;
      private string _name;
      private Color _color;
      private object _bg;
      private float _minHue;
      private float _maxHue;
      public FluentNewTo(FluentNew new_, string name, Color color, object bg, float minHue, float maxHue)
      {
        if (new_ == null) throw new ArgumentNullException(nameof(new_));
        this._new = new_;
        this._name = name;
        this._bg = bg;
        this._color = color;
        this._minHue = minHue;
        this._maxHue = maxHue;
      }

      public DefinitivMapColorFluent ToBackground(object background)
      {
        return new DefinitivMapColorFluent(this._new, new DefinitivMapColor(this._name, this._color, this._minHue, this._maxHue, background));
      }
    }
  }

}

namespace XTEst
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;
  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;

  internal class Test
  {
    public Test()
    {
      IDefinitivMapColor forest =

        FluentNew.Color("Forest")
        .WithUserColor(Color.Green).From(0,0)
        .ToBackground(Knowen.Grass)

        .WithBorder(Knowen.BorderSea)

        .WithShape<TreeShape>()
        .At(0, 0)
        .With(t => t.Name = "Hello")
        .Added()

        .WithShape<TreeShape>()
        .SpreadRandomly(0, 0, 1, 1)
        .Added()

        .AllAroundTheWorld()
        ;

    }
  }
}
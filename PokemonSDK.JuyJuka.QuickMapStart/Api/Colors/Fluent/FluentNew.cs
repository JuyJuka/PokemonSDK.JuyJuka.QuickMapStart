namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using System.Drawing;
  using System.Windows.Forms;

  public sealed class FluentNew
  {
    public static FluentNewFrom Color(string name)
    {
      return new FluentNewFrom(new FluentNew(), name, System.Drawing.Color.Transparent, null);
    }
    // public DefinitivMapColor DefinitivMapColor { get; private set; } = new DefinitivMapColor(string.Empty, System.Drawing.Color.Transparent);

    private FluentNew() { }

    public sealed class FluentNewFrom
    {
      private FluentNew _new;
      private string _name;
      private Color _color;
      private object _bg;
      public FluentNewFrom(FluentNew new_, string name, Color color, object bg)
      {
        if (new_ == null) throw new ArgumentNullException(nameof(new_));
        this._new = new_;
        this._name = name;
        this._bg = bg;
        this._color = color;
      }

      public FluentNewTo From(Color color)
      {
        return new FluentNewTo(this._new, this._name, color, this._bg);
      }
      public FluentNewTo FromRgb(int red, int green, int blue)
      {
        // FromArgb
        return new FluentNewTo(this._new, this._name, System.Drawing.Color.FromArgb(red,green,blue), this._bg);
      }

    }
    public sealed class FluentNewTo
    {
      private FluentNew _new;
      private string _name;
      private Color _color;
      private object _bg;
      public FluentNewTo(FluentNew new_, string name, Color color, object bg)
      {
        if (new_ == null) throw new ArgumentNullException(nameof(new_));
        this._new = new_;
        this._name = name;
        this._bg = bg;
        this._color = color;
      }

      public DefinitivMapColorFluent ToBackground(object background)
      {
        return new DefinitivMapColorFluent(this._new, new DefinitivMapColor(this._name, this._color, background));
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
        .From(Color.Green)
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
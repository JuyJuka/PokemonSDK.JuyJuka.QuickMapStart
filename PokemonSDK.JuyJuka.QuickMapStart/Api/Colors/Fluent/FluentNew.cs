namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using System.Drawing;

  using XTEst;

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

  public class HSLColor
  {
    public float Hue;
    public float Saturation;
    public float Luminosity;

    public HSLColor(float H, float S, float L)
    {
      Hue = H;
      Saturation = S;
      Luminosity = L;
    }

    public static HSLColor FromRGB(Color Clr)
    {
      return FromRGB(Clr.R, Clr.G, Clr.B);
    }

    public static HSLColor FromRGB(Byte R, Byte G, Byte B)
    {
      float _R = (R / 255f);
      float _G = (G / 255f);
      float _B = (B / 255f);

      float _Min = Math.Min(Math.Min(_R, _G), _B);
      float _Max = Math.Max(Math.Max(_R, _G), _B);
      float _Delta = _Max - _Min;

      float H = 0;
      float S = 0;
      float L = (float)((_Max + _Min) / 2.0f);

      if (_Delta != 0)
      {
        if (L < 0.5f)
        {
          S = (float)(_Delta / (_Max + _Min));
        }
        else
        {
          S = (float)(_Delta / (2.0f - _Max - _Min));
        }


        if (_R == _Max)
        {
          H = (_G - _B) / _Delta;
        }
        else if (_G == _Max)
        {
          H = 2f + (_B - _R) / _Delta;
        }
        else if (_B == _Max)
        {
          H = 4f + (_R - _G) / _Delta;
        }
      }

      return new HSLColor(H, S, L);
    }

    private float Hue_2_RGB(float v1, float v2, float vH)
    {
      if (vH < 0) vH += 1;
      if (vH > 1) vH -= 1;
      if ((6 * vH) < 1) return (v1 + (v2 - v1) * 6 * vH);
      if ((2 * vH) < 1) return (v2);
      if ((3 * vH) < 2) return (v1 + (v2 - v1) * ((2 / 3) - vH) * 6);
      return (v1);
    }

    /*public static void X()
    {
      Tuple<int, byte> red = new Tuple<int, byte>(255, 255);
      Tuple<int, byte> green = new Tuple<int, byte>(80, 80);
      Tuple<int, byte> blue = new Tuple<int, byte>(0, 0);
      Action<string, Tuple<int, byte>> print = (s, tpl) => {
        System.Console.Write(s);
        System.Console.Write(" = ");
        System.Console.Write(tpl.Item1);
        System.Console.Write(" | ");
        System.Console.Write(tpl.Item2);
        System.Console.Write(" | ");
        System.Console.Write((int)tpl.Item2);
        System.Console.WriteLine();
      };
      print("red", red);
      print("green", red);
      print("blue", red);
      System.Console.WriteLine(Color.FromArgb((int)0, (int)0, (int)0));
      System.Console.WriteLine(Color.FromArgb((int)red.Item1, (int)green.Item1, (int)blue.Item1));
      System.Console.WriteLine(Color.FromArgb((int)red.Item2, (int)green.Item2, (int)blue.Item2));
    }*/
    public Color ToRGB()
    {
      byte r, g, b;
      if (Saturation == 0)
      {
        r = (byte)Math.Round(Luminosity * 255d);
        g = (byte)Math.Round(Luminosity * 255d);
        b = (byte)Math.Round(Luminosity * 255d);
      }
      else
      {
        double t1, t2;
        double th = Hue / 6.0d;

        if (Luminosity < 0.5d)
        {
          t2 = Luminosity * (1d + Saturation);
        }
        else
        {
          t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
        }
        t1 = 2d * Luminosity - t2;

        double tr, tg, tb;
        tr = th + (1.0d / 3.0d);
        tg = th;
        tb = th - (1.0d / 3.0d);

        tr = ColorCalc(tr, t1, t2);
        tg = ColorCalc(tg, t1, t2);
        tb = ColorCalc(tb, t1, t2);
        r = (byte)Math.Round(tr * 255d);
        g = (byte)Math.Round(tg * 255d);
        b = (byte)Math.Round(tb * 255d);
      }
      return Color.FromArgb(r, g, b);
    }
    private static double ColorCalc(double c, double t1, double t2)
    {

      if (c < 0) c += 1d;
      if (c > 1) c -= 1d;
      if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
      if (2.0d * c < 1.0d) return t2;
      if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
      return t1;
    }
  }

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
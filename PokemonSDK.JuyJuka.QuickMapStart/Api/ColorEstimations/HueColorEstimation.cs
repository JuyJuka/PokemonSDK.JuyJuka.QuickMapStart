
namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using System.Collections.Generic;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public class HueColorEstimation : IColorEstimation
  {
    public virtual float Tollerance { get; set; } = 100;

    public virtual IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors)
    {
      List<IDefinitivMapColor> d1 = new List<IDefinitivMapColor>();
      List<IDefinitivMapColor> d2 = new List<IDefinitivMapColor>();
      float colorHue = color.GetHue();
      float tollerance = this.Tollerance;
      if (colors != null)
        foreach (IDefinitivMapColor c in colors)
          if (c != null)
            d1.Add(c);
      foreach (DefinitivMapColor dColor in d1)
        if (dColor?.Color != null && dColor.Color.R == color.R && dColor.Color.G == color.G && dColor.Color.B == color.B)
          yield return new Tuple<IDefinitivMapColor, float>(dColor, -1);
        else
          d2.Add(dColor);
      foreach (DefinitivMapColor dColor in d2)
      {
        float min = dColor.MinHue;
        float max = dColor.MaxHue;
        if (min < colorHue && max > colorHue)
        {
          yield return new Tuple<IDefinitivMapColor, float>(dColor, Math.Max(Math.Abs(dColor.MinHue - colorHue), Math.Abs(dColor.MaxHue - colorHue)));
        }
        else
        {
          min -= this.Tollerance / 2;
          max += this.Tollerance / 2;
          if (min < colorHue && max > colorHue)
          {
            yield return new Tuple<IDefinitivMapColor, float>(dColor, Math.Max(Math.Abs(dColor.MinHue - colorHue), Math.Abs(dColor.MaxHue - colorHue)));
          }
        }
        /*
        float hueDiff = Math.Abs(dColor.Color.GetHue() - colorHue);
        if (hueDiff <= tollerance) yield return new Tuple<IDefinitivMapColor, float>(dColor, Math.Abs(dColor.Color.GetHue() - colorHue));
        */
      }
    }
  }
}

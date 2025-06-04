namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using System.Collections.Generic;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public class ListColorEstimation : IColorEstimation
  {
    public class Or<T> : IColorEstimation
      where T : ListColorEstimation, new ()
    {
      public Or() { this.Instance.IsOr = true; }

      public T Instance { get; private set; } = new T();

      public IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors)
      {
        return this.Instance.Estimate(map, color, colors);
      }
    }

    public virtual IEnumerable<IColorEstimation>? List { get; set; } = null;
    public virtual bool IsOr { get; set; } = false;

    protected virtual IEnumerable<IColorEstimation> ToList()
    {
      foreach (var item in this.List ?? new IColorEstimation[] { })
        yield return item;
    }

    public virtual IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors)
    {
      foreach (IColorEstimation c in this.ToList() ?? new IColorEstimation[] { })
        if (c != null)
        {
          bool returnedSomething = false;
          foreach (Tuple<IDefinitivMapColor, float> tuple in c.Estimate(map, color, colors) ?? new Tuple<IDefinitivMapColor, float>[] { })
          {
            returnedSomething = true;
            yield return tuple;
          }
          if (returnedSomething && this.IsOr) break;
        }
    }
  }
}

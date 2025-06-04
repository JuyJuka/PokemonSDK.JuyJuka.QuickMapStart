namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.MinMax
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public class MinMaxColorEstimation : IColorEstimation
  {
    public static readonly float Default = 100;

    public MinMaxColorEstimation(float min, IColorEstimation t1Instance)
    {
      if (t1Instance == null) throw new ArgumentNullException(nameof(t1Instance));
      this.T1Instance = t1Instance;
      this.Tolerance = min;
      this.IsMax = false;
    }

    public MinMaxColorEstimation(IColorEstimation t1Instance, float max)
    {
      if (t1Instance == null) throw new ArgumentNullException(nameof(t1Instance));
      this.T1Instance = t1Instance;
      this.Tolerance = max;
      this.IsMax = true;
    }

    public virtual IColorEstimation? T1Instance { get; set; } = null;
    public virtual float Tolerance { get; set; } = Default;
    public virtual bool IsMax { get; set; } = false;

    public virtual IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors)
    {
      IColorEstimation? t1 = this.T1Instance;
      float max = this.Tolerance;
      bool isMax = this.IsMax; if (t1 != null)
        foreach (Tuple<IDefinitivMapColor, float> tuple in t1.Estimate(map, color, colors) ?? new Tuple<IDefinitivMapColor, float>[] { })
        {
          if (isMax && tuple.Item2 <= this.Tolerance)
            yield return tuple;
          else if (!isMax && tuple.Item2 >= this.Tolerance)
            yield return tuple;

          System.Console.WriteLine("tolleranz?" + tuple.Item1.Name + "?" + tuple.Item2);
          System.Console.WriteLine("Look R={0:000} G={1:000} B={2:000}", color.R, color.G, color.B);
          System.Console.WriteLine("Igno R={0:000} G={1:000} B={2:000}", tuple.Item1.Color.R, tuple.Item1.Color.G, tuple.Item1.Color.B);
        }
    }
  }
}

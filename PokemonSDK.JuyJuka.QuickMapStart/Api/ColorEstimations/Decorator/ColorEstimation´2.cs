namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using System.Collections.Generic;

  public class ColorEstimation<T1, T2> : ListColorEstimation
      where T1 : IColorEstimation, new()
      where T2 : IColorEstimation, new()
  {
    public virtual T1 T1Instance { get; set; } = new T1();
    public virtual T2 T2Instance { get; set; } = new T2();

    protected override IEnumerable<IColorEstimation> ToList()
    {
      yield return this.T1Instance;
      yield return this.T2Instance;
      foreach (IColorEstimation c in base.ToList())
        yield return c;
    }
  }
}

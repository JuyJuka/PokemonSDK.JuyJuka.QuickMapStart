namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.MinMax
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations;

  public class MaxColorEstimation<T1> : MinMaxColorEstimation
    where T1 : IColorEstimation, new()
  {
    public MaxColorEstimation() : this(Default) { }
    public MaxColorEstimation(float max) : base(new T1(), max) { }
  }
}

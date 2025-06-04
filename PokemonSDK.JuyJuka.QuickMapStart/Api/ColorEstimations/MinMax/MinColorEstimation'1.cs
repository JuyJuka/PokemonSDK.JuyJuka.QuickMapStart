namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.MinMax
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations;

  public class MinColorEstimation<T1> : MinMaxColorEstimation 
    where T1 : IColorEstimation, new()
  {
    public MinColorEstimation() : this(Default) { }
    public MinColorEstimation(float min) : base(min, new T1()) { }
  }
}

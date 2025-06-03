namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  using X = PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.DefinitivMapColor.FunctionParameters;

  public interface IShape
  {
    IShapePositon? Position { get; set; }
    string ToLayer(X parameters);
  }
}

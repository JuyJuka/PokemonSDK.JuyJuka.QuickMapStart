namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  public class TreeShape : Shape
  {
    public TreeShape() : base(string.Empty
      , new object[,] { }
      , new object[,]{
        { _, X, X, _ },
        { _, X, X, _ },
        { _, _, _, _ },
      }
      , new object[,] { }
      , new object[,] {
        { 1 + 400,1 + 401,1 + 402,1 + 403 },
        { 1 + 408,1 + 409,1 + 410,1 + 411 },
        { _, _, _, _ },
      }
      , new object[,] {
        { _, _, _, _ },
        { _, _, _, _ },
        { _,1+433 ,1+434,_ },
      }
    )
    { }
  }
}

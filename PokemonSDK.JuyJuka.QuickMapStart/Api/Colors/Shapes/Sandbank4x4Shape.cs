namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  public class Sandbank4x4Shape : Shape
  {
    public Sandbank4x4Shape() : base(string.Empty
      , new object[,]{
        { _, _, _, _, _, _, },
        { _, Knowen.SystemTagSea, Knowen.SystemTagSea, Knowen.SystemTagSea, Knowen.SystemTagSea, _ },
        { _, Knowen.SystemTagSea, Knowen.SystemTagSand, Knowen.SystemTagSand, Knowen.SystemTagSea, _ },
        { _, Knowen.SystemTagSea, Knowen.SystemTagSand, Knowen.SystemTagSand, Knowen.SystemTagSea, _ },
        { _, Knowen.SystemTagSea, Knowen.SystemTagSea, Knowen.SystemTagSea, Knowen.SystemTagSea, _  },
        { _, _, _, _, _, _, },
      }
      , new object[,] { }
      , new object[,] { }
      , new object[,] { }
      , new object[,]{
        { _, _, _, _, _, _, },
        { _, 1+131, 1+132, 1+132, 1+133, _ },
        { _, 1+139, 1+140, 1+140, 1+141, _ },
        { _, 1+139, 1+140, 1+140, 1+141, _ },
        { _, 1+147, 1+148, 1+148, 1+149, _ },
        { _, _, _, _, _, _,  },
      }
    )
    { }
  }
}

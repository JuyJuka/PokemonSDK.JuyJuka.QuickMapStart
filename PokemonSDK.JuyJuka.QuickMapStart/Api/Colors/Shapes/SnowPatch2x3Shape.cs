namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  public class SnowPatch2x3Shape : Shape
  {
    public SnowPatch2x3Shape() : this(true) { }
    public SnowPatch2x3Shape(bool canEncounter) : base(string.Empty
      , (!canEncounter) ? (new object[,] { }) : new object[,]{
        { _, _, _, _ },
        { _, Knowen.SystemTagGrass, Knowen.SystemTagGrass, _  },
        { _, Knowen.SystemTagGrass, Knowen.SystemTagGrass, _  },
        { _, Knowen.SystemTagGrass, Knowen.SystemTagGrass, _  },
        { _, _, _, _ },
      }
      , new object[,] { }
      , new object[,] { }
      , new object[,] { }
      , new object[,]{
        { _, _, _, _ },
        { _, 1+4228, 1+4230, _ },
        { _, 1+4236, 1+4238, _ },
        { _, 1+4244, 1+4246, _ },
        { _, _, _, _ },
      }
    )
    { }
  }
}

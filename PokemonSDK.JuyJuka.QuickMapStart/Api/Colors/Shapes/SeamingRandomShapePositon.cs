namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  using System.Drawing;

  using X = PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.DefinitivMapColor.FunctionParameters;

  public class SeamingRandomShapePositon : IShapePositon
  {
    public virtual int WorldMapCoordinatesXModulo { get; set; } = Map._1;
    public virtual int WorldMapCoordinatesYModulo { get; set; } = Map._1;
    public virtual int FrequencyX { get; set; } = Map._0;
    public virtual int FrequencyY { get; set; } = Map._0;
    public virtual bool IsStart(X parameters, Point start)
    {
      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      for(int x=worldX;x<parameters.Map.World.Size.Width;x+=this.FrequencyX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += this.FrequencyY)
          if(start.X == x && start.Y == y)
            return true;
      return false;
    }
  }
}

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
      const int _33 = 33;
      const int _66 = 66;
      decimal percentageX = (start.X / (decimal)parameters.Map.World.Size.Width) * 100;
      decimal percentageY = (start.Y / (decimal)parameters.Map.World.Size.Height) * 100;

      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;



      if (percentageX < _33)
      {
        if (percentageY < _33)
          return this.A(parameters, start);
        else if (percentageY < _66)
          return this.B(parameters, start);
        else
          return this.C(parameters, start);
      }
      else if (percentageX < _66)
      {
        if (percentageY < _33)
          return this.D(parameters, start);
        else if (percentageY < _66)
          return this.E(parameters, start);
        else
          return this.F(parameters, start);
      }
      else
      {
        if (percentageY < _33)
          return this.G(parameters, start);
        else if (percentageY < _66)
          return this.H(parameters, start);
        else
          return this.I(parameters, start);
      }
    }

    private bool A(X parameters, Point start)
    {
      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool B(X parameters, Point start)
    {
      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldY = parameters.Map.WorldMapCoordinates.Y;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool C(X parameters, Point start)
    {
      int worldX = parameters.Map.WorldMapCoordinates.X;
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool D(X parameters, Point start)
    {
      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX;
      int fY = this.FrequencyY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool E(X parameters, Point start)
    {
      int worldY = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldX = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool F(X parameters, Point start)
    {
      int worldY = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldX = parameters.Map.WorldMapCoordinates.Y;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool G(X parameters, Point start)
    {
      int worldY = parameters.Map.WorldMapCoordinates.X;
      int worldX = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool H(X parameters, Point start)
    {
      int worldY = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesXModulo;
      int worldX = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX;
      int fY = this.FrequencyY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

    private bool I(X parameters, Point start)
    {
      int worldY = parameters.Map.WorldMapCoordinates.Y % this.WorldMapCoordinatesXModulo;
      int worldX = parameters.Map.WorldMapCoordinates.X % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX;
      int fY = this.FrequencyY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }

  }
}

﻿namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
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
      int worldX = (parameters.Map.WorldMapCoordinates.X + Map._1) % this.WorldMapCoordinatesXModulo;
      int worldY = (parameters.Map.WorldMapCoordinates.Y + Map._1) % this.WorldMapCoordinatesYModulo;
      int fX = this.FrequencyX + worldX;
      int fY = this.FrequencyY + worldY;
      for (int x = worldX; x < parameters.Map.World.Size.Width; x += fX)
        for (int y = worldY; y < parameters.Map.World.Size.Height; y += fY)
          if (start.X == x && start.Y == y)
            return true;
      return false;
    }
  }
}
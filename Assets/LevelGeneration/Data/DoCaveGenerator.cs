using UnityEngine;
using System.Collections;

public class DoCaveGenerator 
    : DoAbstractWorldGenerator 
{
    [Range(0.0f, 1.0f)]
    public float obstacleChance;
    [Range(0, 9)]
    public int birthLimit;
    [Range(0, 9)]
    public int deathLimit;





    protected override void MyInit()
    {
        CreateNoise(obstacleChance);
    }


    public override void DoStep()
    {

        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                int blockCount = CurWorld.CountNeighbours(i, j, DoTile.TileType.Obstacle);

                if (CurWorld.GetTileAt(i, j).Type == DoTile.TileType.Empty)
                {
                    if (blockCount > birthLimit)
                        NextWorld.GetTileAt(i, j).Type = DoTile.TileType.Obstacle;
                   
                    else
                        NextWorld.GetTileAt(i, j).Type = DoTile.TileType.Empty;
                }


                else
                {
                    if (blockCount < deathLimit)
                        NextWorld.GetTileAt(i, j).Type = DoTile.TileType.Empty;

                    else 
                        NextWorld.GetTileAt(i, j).Type = DoTile.TileType.Obstacle;
                }
            }
        }

        DoWorld oldWorld = CurWorld;
        CurWorld = NextWorld;
        NextWorld = oldWorld;

    }




    public override void DoSteps(int numSteps)
    {
        for (int i = 0; i < numSteps; i++)
            DoStep();
    }


}

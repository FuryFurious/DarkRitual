using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoCaveGenerator 
    : DoAbstractWorldGenerator 
{
    [Range(0.0f, 1.0f)]
    public float obstacleChance;
    [Range(0, 9)]
    public int birthLimit;
    [Range(0, 9)]
    public int deathLimit;
    [Range(0.0f, 1.0f)]
    public float grasSpawnChance;
    [Range(0.0f, 1.0f)]
    public float poolSpawnChance;
    [Range(0, 100)]
    public int borderWidth;
    [Range(0.0f, 1.0f)]
    public float singleObstacleChance;

    private Vec2i pentagramPos = new Vec2i(-1, -1);

    private List<DoTile> singleFieldObstacles = new List<DoTile>();


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

                if (i <= borderWidth || j <= borderWidth || i >= width - borderWidth || j >= height - borderWidth)
                    NextWorld.GetTileAt(i, j).Type = DoTile.TileType.Obstacle;
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


    public override void CreateGameObjects()
    {
        singleFieldObstacles.Clear();
        DeleteChildren();

        Debug.Assert(CurWorld != null);

        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                Debug.Assert(CurWorld.GetTileAt(i, j) != null);

                //create ground:
                InstantiateObject(groundTilesPrefabs[random.Next(0, groundTilesPrefabs.Length)], i, j);

                //create solid walls:
                if (CurWorld.GetTileAt(i, j).Type == DoTile.TileType.Obstacle)
                {
                    int id = CurWorld.GetNeighbourInfo(i, j);
                    InstantiateObject(tilabeObstaclesPrefabs[id], i, j);
                }


                //create decorations or single field blockers:
                else
                {
                    //create gras:
                    if (RandFloat() < grasSpawnChance)
                    {
                        GameObject gras = InstantiateObject(grasDecoPrefabs[random.Next(0, grasDecoPrefabs.Length)], i, j);

                        if (RandFloat() < 0.5f)
                            FlipX(gras);
                    }

                    //create pools
                    else if (RandFloat() < poolSpawnChance)
                    {
                        GameObject pool = InstantiateObject(swampPoolPrefabs, i, j);

                        if (RandFloat() < 0.5f)
                            FlipX(pool);
                    }

                    //create stones:
                    else if(RandFloat() < singleObstacleChance && CurWorld.CountNeighbours(i, j, DoTile.TileType.Empty) == 8)
                    {

                        InstantiateObject(singleTileBlockersPrefabs[random.Next(0, singleTileBlockersPrefabs.Length)], i, j);

                        CurWorld.GetTileAt(i, j).Type = DoTile.TileType.Obstacle;

                        singleFieldObstacles.Add(CurWorld.GetTileAt(i, j));
                    }
                }
            }
        }


        Debug.Assert(singleFieldObstacles.Count >= 1);
        
      DoTile pentagramTile = singleFieldObstacles[random.Next(0, singleFieldObstacles.Count)];
      CreatePentagram(pentagramTile.X, pentagramTile.Y);
    }

    void CreatePentagram(int i, int j)
    {
        RemoveTilesAround(i, j, 2);
        InstantiateObject(pentagramPrefab, i, j);
        pentagramPos = new Vec2i(i, j);
    }

    void FlipX(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if(spriteRenderer != null)
            spriteRenderer.flipX = true;
    }

    public override Vec2i GetPentagramPos()
    {
        return pentagramPos;
    }

    public void RemoveTilesAround(int x, int y, int radius)
    {
        int half = radius / 2;

        for (int i = -half; i <= half; i++)
        {
            for (int j = -half; j <= half; j++)
            {
                int idX = x + i;
                int idY = y + j;

                if (idX >= 0 && idY >= 0 && idX < CurWorld.WorldWidth && idY < CurWorld.WorldHeight)
                {

                    if (CurWorld.GetTileAt(idX, idY).Type == DoTile.TileType.Empty)
                    {
                        for (int k = spawnedGameObjects[idX, idY].Count - 1; k >= 0; k--)
                        {
                            GameObject.Destroy(spawnedGameObjects[idX, idY][k]);
                        }
                        spawnedGameObjects[idX, idY].Clear();


                        //recreate the ground tiles:
                        InstantiateObject(groundTilesPrefabs[random.Next(0, groundTilesPrefabs.Length)], idX, idY);
                    }
                }
            }
        }

    }
}

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
    public float grassOnGroundChance;
     [Range(0.0f, 1.0f)]
    public float grassOnObstacleChance;

    [Range(0.0f, 1.0f)]
    public float poolSpawnChance;
    [Range(0, 100)]
    public int borderWidth;

    [Range(0.0f, 1.0f)]
    public float singleObstacleChanceOnGround;
    [Range(0.0f, 1.0f)]
    public float singleObstaclesChanceOnObstacles;

    [Range(1, 50)]
    public int numSteps;


    protected override void MyInit()
    {
        CreateNoise(obstacleChance);

        DoSteps();
    }


    public override void DoSteps()
    {
        DoWorld oldWorld = null;

        for (int i = 0; i < numSteps - 1; i++)
        {
            CarveTheCave();

            oldWorld = CurWorld;
            CurWorld = NextWorld;
            NextWorld = oldWorld;
        }

        CarveTheCave();

        List<DoRegion> regions = CalculateRegions();

        Debug.Assert(regions.Count >= 1);

        for (int i = 1; i < regions.Count; i++)
        {
            List<DoTile> tiles = regions[i].GetTiles();

            for (int j = 0; j < tiles.Count; j++)
                tiles[j].Type = DoTile.TileType.Obstacle;
        }

        oldWorld = CurWorld;
        CurWorld = NextWorld;
        NextWorld = oldWorld;

        DoRegion biggestRegion = regions[0];

        PlaceAllTheStuff(biggestRegion);
        
      
    }

    private void PlaceAllTheStuff(DoRegion biggestRegion)
    {

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                DoTile curTile = CurWorld.GetTileAt(i, j);

                //you are in the ground:
                if (curTile.Type == DoTile.TileType.Empty)
                {
                    //create grass
                    if (RandFloat() < grassOnGroundChance)
                        curTile.TopObject = DoTile.ObjectOnTop.Grass;

                    //create obstacle
                    else if (CurWorld.CountNeighbours(i, j, (x => x.Type == DoTile.TileType.Obstacle)) == 0)
                    {
                        if (RandFloat() < singleObstacleChanceOnGround)
                            curTile.TopObject = DoTile.ObjectOnTop.SingleBlocker;
                    }

                    //create pool:
                    else if (RandFloat() < poolSpawnChance)
                        curTile.TopObject = DoTile.ObjectOnTop.Pool;
                }


                //you are on a obstacle:
                else if (curTile.Type == DoTile.TileType.Obstacle)
                {

                    //create grass:
                    if (CurWorld.HasLowerNeighbour(i, j, DoTile.TileType.Obstacle))
                    {
                        if (RandFloat() < grassOnObstacleChance)
                            curTile.TopObject = DoTile.ObjectOnTop.Grass;
                    }

                    //create obstacle:
                    else if (CurWorld.HasLowerNeighbour(i, j, DoTile.TileType.Obstacle))
                    {
                        if (RandFloat() < singleObstaclesChanceOnObstacles)
                            curTile.TopObject = DoTile.ObjectOnTop.SingleBlocker;
                    }

                }

            }
        }


       
        DoTile bigPortalTile = biggestRegion.GetTile( random.Next(biggestRegion.RegionSize()));
        RemoveTilesAround(CurWorld, biggestRegion, bigPortalTile.X, bigPortalTile.Y, 4);
        bigPortalTile.TopObject = DoTile.ObjectOnTop.BigPortal;



        for (int i = 0; i < PortalStoneTarget.NUM_COLORS; i++)
        {
            DoTile smallPortalTile = biggestRegion.GetTile(random.Next(biggestRegion.RegionSize()));
            RemoveTilesAround(CurWorld, biggestRegion, smallPortalTile.X, smallPortalTile.Y, 2);
            smallPortalTile.TopObject = DoTile.ObjectOnTop.SmallPortal;
        }
   



    }

 

   




    private List<DoRegion> CalculateRegions()
    {
        List<DoRegion> regions = new List<DoRegion>();
        int regionCount = 0;
  
        int[,] markedTiles = new int[width, height];


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (markedTiles[i, j] != 0 || NextWorld.GetTileAt(i, j).Type == DoTile.TileType.Obstacle)
                    continue;

                else
                {
                    regionCount++;
                    regions.Add(new DoRegion(regionCount));

                    FillRegion(regions[regionCount - 1], NextWorld, markedTiles, i, j);
                }
            }
        }

        regions.Sort();

        return regions;
    }

    private void CarveTheCave()
    {
        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                int blockCount = CurWorld.CountNeighbours(i, j, (x => x.Type == DoTile.TileType.Obstacle));

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
    }

    private void FillRegion(DoRegion region, DoWorld world, int[,] markedTiles, int x, int y)
    {

        //is inside the level:
        if (x >= 0 && y >= 0 && y < height && x < width)
        {
            //label is not 0 -> its marked by a region
            if (markedTiles[x, y] != 0 || world.GetTileAt(x, y).Type == DoTile.TileType.Obstacle)
                return;

            else
            {
                markedTiles[x, y] = region.RegionId;
                region.AddTile(world.GetTileAt(x, y));

                FillRegion(region, world, markedTiles, x - 1, y);
                FillRegion(region, world, markedTiles, x + 1, y);
                FillRegion(region, world, markedTiles, x, y - 1);
                FillRegion(region, world, markedTiles, x, y + 1);
            }


          
        }
    }



    public void RemoveTilesAround(DoWorld world, DoRegion biggestRegion, int x, int y, int radius)
    {
        int half = radius / 2;
        int secHalf = radius - half;

        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int idX = x + i;
                int idY = y + j;

                if (idX >= borderWidth && idY >= borderWidth && idX < world.WorldWidth - borderWidth && idY < world.WorldHeight - borderWidth)
                {

                    int dist = world.GetDistance8Neigh(x, y, idX, idY);
                    if (dist <= radius)
                    {
                        world.GetTileAt(idX, idY).Type = DoTile.TileType.Empty;
                        world.GetTileAt(idX, idY).TopObject = DoTile.ObjectOnTop.None;

                        if(biggestRegion != null)
                            biggestRegion.Remove(world.GetTileAt(idX, idY));
                    }
                }
            }
        }
    }


    

}
